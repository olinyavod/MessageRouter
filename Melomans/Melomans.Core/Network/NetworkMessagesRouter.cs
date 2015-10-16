using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Melomans.Core.Message;
using Melomans.Core.Models;
using Sockets.Plugin;
using Sockets.Plugin.Abstractions;

namespace Melomans.Core.Network
{
	public class NetworkMessagesRouter : INetworkEventAgriggator
	{
		private readonly INetworkSettngs _networkSettngs;
		private readonly IMessageService _messageService;
		private readonly INetworkTaskFactory _taskFactory;
		private readonly IDictionary<string, IMessageSubscrubtion> _messageSubscrubtions;
		private readonly UdpSocketMulticastClient _multicastClient;
		private readonly TcpSocketListener _listener;

		public NetworkMessagesRouter(
			INetworkSettngs networkSettngs,
			IMessageService messageService,
			INetworkTaskFactory taskFactory)
		{
			_networkSettngs = networkSettngs;
			_messageService = messageService;
			_taskFactory = taskFactory;
			_messageSubscrubtions = new ConcurrentDictionary<string, IMessageSubscrubtion>();
			_multicastClient = new UdpSocketMulticastClient();
			_multicastClient.MessageReceived += MessageReceived;
			_listener = new TcpSocketListener(networkSettngs.BufferSize);
			_listener.ConnectionReceived += ConnectionReceived;

		}

		private void MessageReceived(object sender, UdpSocketMessageReceivedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void ConnectionReceived(object sender, TcpSocketListenerConnectEventArgs e)
		{
			throw new NotImplementedException();
		}

		public void Initialize()
		{
			_listener.StartListeningAsync(_networkSettngs.ListenPort);
			_multicastClient.TTL = _networkSettngs.TTL;
			_multicastClient.JoinMulticastGroupAsync(_networkSettngs.MulticastAddress, _networkSettngs.MulticastPort);
		}

		public INetworkTask<TMessage> Publish<TMessage>(TMessage message) 
			where TMessage : class, IMessage
		{
			return _taskFactory.CreateMulticastTask(message, _multicastClient);
		}

		public IEnumerable<INetworkTask<TMessage>> PublishFor<TMessage>(IEnumerable<Meloman> melomans, TMessage message)
			where TMessage : class, IMessage
		{
			foreach (var meloman in melomans)
				yield return _taskFactory.CreateAddressTask(meloman, message);
		}

		public IDisposable Subscribe<TMessage>(Action<INetworkTask<TMessage>> action) where TMessage : class, IMessage
		{
			throw new NotImplementedException();
		}

		#region IDisposable Support

		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					_listener.ConnectionReceived -= ConnectionReceived;
					_listener.Dispose();
					_multicastClient.MessageReceived -= MessageReceived;
					_multicastClient.Dispose();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.

		// ~NetworkMessagesRouter() {

		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.

		//   Dispose(false);

		// }

		// This code added to correctly implement the disposable pattern.

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}

		#endregion
	}
}
