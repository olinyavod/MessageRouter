using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Melomans.Core.Message;
using Melomans.Core.Models;
using System.Threading.Tasks;

namespace Melomans.Core.Network
{
	public class NetworkMessagesRouter : INetworkMessageRouter
	{
		private readonly IMessageService _messageService;
		private readonly INetworkTaskFactory _taskFactory;
		private readonly ConcurrentDictionary<long, IMessageSubscription> _messageSubscrubtions;
		private readonly IMulticastClient _multicastClient;
		private readonly ITcpListener _listener;

		public NetworkMessagesRouter(
			IMessageService messageService,
			INetworkClientFactory clientFactory,
			INetworkTaskFactory taskFactory)
		{
			_messageService = messageService;
			_taskFactory = taskFactory;
			_messageSubscrubtions = new ConcurrentDictionary<long, IMessageSubscription>();
			_multicastClient = clientFactory.CreateMulticastClient ();
			_multicastClient.MessageReceived += MessageReceived;
			_listener = clientFactory.CreateListener();
			_listener.ConnectionReceived += ConnectionReceived;

		}

		private async void MessageReceived(object sender, DatagramReceivedEventArgs e)
		{
			MemoryStream stream = null;
			try
			{
				stream = new MemoryStream(e.Data);
				var value = await GetSubscrubtion(e.RemoteAddress, stream);
				if (value != null)
				{
					value.ReceivedMessage(null, new MulticastRemoteClient(stream));
				}

			}
			finally
			{
				if(stream != null)
					stream.Dispose();
			}
		}

		async Task<IMessageSubscription> GetSubscrubtion(string senderAddress, Stream stream)
		{
			var buffer = new byte[8];
			var readeCount = await stream.ReadAsync(buffer, 0, buffer.Length);
			var key = BitConverter.ToInt64(buffer, 0);
			IMessageSubscription value;
			if (_messageSubscrubtions.TryGetValue(key, out value))
				return value;
			return null;
		}

		private async void ConnectionReceived(object sender, ListenerConnectEventArgs e)
		{
			var value = await GetSubscrubtion(e.RemoteAddress, e.RemoteClient.ReadStream);
			if (value != null)
			{
				var buffer = Encoding.UTF8.GetBytes(NetworkState.Ok.ToString());
				await e.RemoteClient.WriteStream.WriteAsync(buffer, 0, buffer.Length);
				await e.RemoteClient.WriteStream.FlushAsync();
				value.ReceivedMessage(null, e.RemoteClient);
			}
			else
			{
				var buffer = Encoding.UTF8.GetBytes(NetworkState.AccessDenied.ToString());
				await e.RemoteClient.WriteStream.WriteAsync(buffer, 0, buffer.Length);
				await e.RemoteClient.WriteStream.FlushAsync();
				await e.RemoteClient.DisconnectAsync();
				e.RemoteClient.Dispose();
			}
		}

		public async void Stop()
		{
			await _multicastClient.DisconnectAsync();
			await _listener.StopListeningAsync();
		}

		public void Start()
		{
			_listener.StartListeningAsync();
			_multicastClient.JoinMulticastGroupAsync();
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

		public IMessageeceiverConfig<TMessage> Subscribe<TMessage>() 
			where TMessage : class, IMessage
		{
			var definition = _messageService.GetDefinition<TMessage>();
			var id = _messageService.CreateMessageHash(definition);
			var result = (MessageSubscription<TMessage>)_messageSubscrubtions.GetOrAdd(id, key => new MessageSubscription<TMessage>(key, definition, _taskFactory, _messageSubscrubtions));
			return result.New();
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
