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

namespace Melomans.Core.Network
{
	public class TcpAddressTask<TMessage> : NetworkTaskBase<TMessage>, INetworkTask<TMessage>
		where TMessage:class, IMessage
	{
		private readonly Meloman _meloman;
		private readonly INetworkClientFactory _clientFactory;
		private readonly TMessage _message;
		private readonly IMessageSerializer _serializer;
		private readonly IMessageService _messageService;

		public TcpAddressTask(Meloman meloman, 
			INetworkClientFactory clientFactory,
			TMessage message,
			IMessageSerializer serializer,
			IMessageService messageService)
		{
			_meloman = meloman;
			_clientFactory = clientFactory;
			_message = message;
			_serializer = serializer;
			_messageService = messageService;
			
		}

		public override Meloman For
		{
			get { return _meloman; }
		}

		private async Task<bool> GetResponse(Stream readStream, CancellationToken cancellationToken)
		{
			MemoryStream memoryBuffer = null;
			try
			{
				var buffer = new byte[1024];
				memoryBuffer = new MemoryStream();
				int readedCount = 0;
				do
				{
					readedCount = await readStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
					await memoryBuffer.WriteAsync(buffer, 0, readedCount);
				} while (readedCount > 0);
				if (IsCancellationRequested)
					throw new OperationCanceledException();

				NetworkState responseCode;
				if (!Enum.TryParse(Encoding.UTF8.GetString(memoryBuffer.ToArray(), 0, (int) memoryBuffer.Length), out responseCode))
					throw new InvalidDataException();

				switch (responseCode)
				{
					case NetworkState.AccessDenied:
						throw new SecurityAccessDeniedException(string.Format("Access denied for type message {0}", typeof (TMessage)));
					case NetworkState.Error:
						throw new IOException();
				}
				return true;
			}
			finally
			{
				if(memoryBuffer != null)
					memoryBuffer.Dispose();
			}
		}

		protected async override Task Run(CancellationToken cancellationToken)
		{
			ITcpClient client = null;
			try
			{
				if (!_messageService.CanSend(For.Id, typeof (TMessage)))
					throw new SecurityAccessDeniedException(string.Format("Access denied for type message {0}", typeof (TMessage)));
                client = _clientFactory.CreateTcpClient();
				await client.ConnectAsync(_meloman.IpAddress, _meloman.Port);
				var definition = _messageService.GetDefinition<TMessage>();
				var buffer = BitConverter.GetBytes(_messageService.CreateMessageHash(definition));
				await client.WriteStream.WriteAsync(buffer, 0, buffer.Length);
				await client.WriteStream.FlushAsync(cancellationToken);
				if(!await GetResponse(client.ReadStream, cancellationToken))
					return;
				if (IsCancellationRequested)
					throw new OperationCanceledException();
				
				await _serializer.WriteMessage(_message, client.WriteStream);
			    await client.WriteStream.FlushAsync(cancellationToken);
				if (IsCancellationRequested)
					throw new OperationCanceledException();
				
				await client.WriteStream.FlushAsync(cancellationToken);
				if(!await GetResponse(client.ReadStream, cancellationToken))
					return;
				var streaming = Message as IStreamingMessage;
				if (streaming != null)
				{
					var readedCount = 0;
					ulong readed = 0;
					RaiseReport(new ProgressInfo<TMessage>(_message, streaming.StreamLength, readed));
					buffer = new byte[2048];
					do
					{
						readedCount = await streaming.Stream.ReadAsync(buffer, 0, buffer.Length);
						await client.WriteStream.WriteAsync(buffer, 0, readedCount, cancellationToken);
						await client.WriteStream.FlushAsync(cancellationToken);
						if(IsCancellationRequested)
							throw new OperationCanceledException();
						readed += (ulong) readedCount;
						RaiseReport(new ProgressInfo<TMessage>(_message, streaming.StreamLength, readed));
					} while (readedCount > 0);
				}
				RaiseSuccess(Message);
				await client.DisconnectAsync();
			}
			finally
			{
				
				if(client != null)
					client.Dispose();
			}
		}

		protected override TMessage Message
		{
			get { return _message; }
		}
	}
}
