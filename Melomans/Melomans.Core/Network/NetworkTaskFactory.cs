using MessageRouter.Message;
using MessageRouter.Models;

namespace MessageRouter.Network
{
	public class NetworkTaskFactory : INetworkTaskFactory
	{
		private readonly INetworkClientFactory _clientFactory;
		private readonly IMessageService _messageService;
		private readonly IMessageSerializer _messageSerializer;

		public NetworkTaskFactory(
			INetworkClientFactory clientFactory,
			IMessageService messageService,
			IMessageSerializer messageSerializer)
		{
			_clientFactory = clientFactory;
			_messageService = messageService;
			_messageSerializer = messageSerializer;
		}

		public INetworkTask<TMessage> CreateAddressTask<TMessage>(Meloman meloman, TMessage message)
			where TMessage : class, IMessage
		{
			return new TcpAddressTask<TMessage>(meloman, _clientFactory, message, _messageSerializer, _messageService);
		}

		public INetworkTask<TMessage> CreateMulticastTask<TMessage>(TMessage message, IMulticastClient client) where TMessage : class, IMessage
		{
			return new UdpMulticastSenderTask<TMessage>(message, _messageSerializer, _messageService, client);
		}

		public INetworkTask<TMessage> CreateReceivedTask<TMessage>(Meloman meloman, IRemoteClient client) where TMessage : class, IMessage
		{
			return new MessageReceiveTask<TMessage>(meloman, client, _messageSerializer);
		}
	}
}
