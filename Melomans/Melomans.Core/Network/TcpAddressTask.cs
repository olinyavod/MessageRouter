using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Melomans.Core.Models;
using Melomans.Core.Message;
using Sockets.Plugin;
using Sockets.Plugin.Abstractions;

namespace Melomans.Core.Network
{
	public class TcpAddressTask<TMessage> : INetworkTask<TMessage>
		where TMessage:class, IMessage
	{
		private readonly Meloman _meloman;
		private readonly TMessage _message;
		private readonly IMessageSerializer _serializer;
		private readonly INetworkSettngs _settngs;
		private Action<TMessage> _onComplite;
		private Action<Exception> _onCatch;
		private Action<int> _onReport;
		private readonly CancellationTokenSource _cancellationTokenSource;

		public TcpAddressTask(Meloman meloman, 
			TMessage message,
			IMessageSerializer serializer,
			INetworkSettngs settngs)
		{
			_meloman = meloman;
			_message = message;
			_serializer = serializer;
			_settngs = settngs;
			_cancellationTokenSource = new CancellationTokenSource();
		}

		public Meloman For
		{
			get { return _meloman; }
		}

		public void Cancel()
		{
			_cancellationTokenSource.Cancel();
		}

		public INetworkTask<TMessage> OnComplite(Action<TMessage> onComplite)
		{
			_onComplite = onComplite;
			return this;
		}

		public INetworkTask<TMessage> OnException(Action<Exception> onCatch)
		{
			_onCatch = onCatch;
			return this;
		}

		public INetworkTask<TMessage> OnReport(Action<int> onReport)
		{
			_onReport = onReport;
			return this;
		}

		private async Task<bool> GetResponse(Stream readStream)
		{
			var buffer = new byte[1024];
			var readedCount = await readStream.ReadAsync(buffer, 0, buffer.Length, _cancellationTokenSource.Token);
			if (_cancellationTokenSource.IsCancellationRequested)
			{
				_onComplite(_message);
				return false;
			}

			NetworkState responseCode;
			if (!Enum.TryParse(Encoding.UTF8.GetString(buffer, 0, readedCount), out responseCode))
				throw new InvalidDataException();

			switch (responseCode)
			{
				case NetworkState.AccessDenied:
					throw new SecurityAccessDeniedException(string.Format("Access denied for type message {0}", typeof(TMessage)));
				case NetworkState.Error:
					throw new IOException();
			}
			return true;
		}

		public async void Run()
		{
			TcpSocketClient client = null;
			try
			{
				client = new TcpSocketClient();
				await client.ConnectAsync(_meloman.IpAddress, _meloman.Port);
				var buffer = typeof (TMessage).GetTypeInfo().GUID.ToByteArray();
				client.WriteStream.Write(buffer, 0, buffer.Length);
				await client.WriteStream.FlushAsync(_cancellationTokenSource.Token);
				if(!await GetResponse(client.ReadStream))
					return;
				if (_cancellationTokenSource.IsCancellationRequested)
				{
					_onComplite(_message);
					return;
				}
				
				await _serializer.WriteMessage(_message, client.WriteStream);
				if (_cancellationTokenSource.IsCancellationRequested)
				{
					_onComplite(_message);
					return;
				}
				await client.WriteStream.FlushAsync(_cancellationTokenSource.Token);
				if(!await GetResponse(client.ReadStream))
					return;
				var streaming = _message as IStreamingMessage;
				if (streaming != null)
				{
					
				}
				if (_onComplite != null)
					_onComplite(_message);


			}
			catch (Exception ex)
			{
				if (_onCatch != null)
					_onCatch(ex);
			}
			finally
			{
				await client.DisconnectAsync();
				if(client != null)
					client.Dispose();
			}
		}
	}
}
