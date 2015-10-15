using System;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using Melomans.Core.Models;
using Melomans.Core.Message;
using Sockets.Plugin.Abstractions;

namespace Melomans.Core.Network
{
	class UdpMulticastSenderTask<TMessage> : INetworkTask<TMessage>
		where TMessage:class, IMessage
	{
		private readonly TMessage _message;
		private readonly IMessageSerializer _serializer;
		private readonly IUdpSocketMulticastClient _client;
		private Action<TMessage> _onComplite;
		private Action<Exception> _onCatch;

		public UdpMulticastSenderTask(TMessage message, IMessageSerializer serializer, IUdpSocketMulticastClient client)
		{
			_message = message;
			_serializer = serializer;
			_client = client;
		}

		public Meloman For
		{
			get { return null; }
		}

		public void Cancel()
		{
			
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
			return this;
		}



		public async void Run()
		{
			MemoryStream stream = null;
			try
			{

				stream = new MemoryStream();
				await _serializer.WriteMessage(_message, stream);
				await _client.SendMulticastAsync(stream.ToArray());
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
				if (stream != null)
					stream.Dispose();
			}
			
		}
	}
}
