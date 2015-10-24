using System;
using Melomans.Core.Message;
using Melomans.Core.Models;
using Sockets.Plugin;

namespace Melomans.Core.Network
{
	public class NetworkTaskFactory : INetworkTaskFactory
	{
		private readonly IMessageService _messageService;
		private readonly IMessageSerializer _messageSerializer;
		private readonly INetworkSettngs _networkSettngs;

		public NetworkTaskFactory(
			IMessageService messageService,
			IMessageSerializer messageSerializer,
			INetworkSettngs networkSettngs)
		{
			_messageService = messageService;
			_messageSerializer = messageSerializer;
			_networkSettngs = networkSettngs;
		}

		public INetworkTask<TMessage> CreateAddressTask<TMessage>(Meloman meloman, TMessage message)
			where TMessage : class, IMessage
		{
			return new TcpAddressTask<TMessage>(meloman, message, _messageSerializer, _messageService, _networkSettngs);
		}

		public INetworkTask<TMessage> CreateMulticastTask<TMessage>(TMessage message, UdpSocketMulticastClient client) where TMessage : class, IMessage
		{
			return new UdpMulticastSenderTask<TMessage>(message, _messageSerializer, _messageService, client);
		}

		public INetworkTask<TMessage> CreateReceivedTask<TMessage>(Meloman meloman, IRemoteClient client) where TMessage : class, IMessage
		{
			return new MessageReceiveTask<TMessage>(meloman, client, _messageSerializer);
		}
	}
}
