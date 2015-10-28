using System;
using Melomans.Core.Message;
using Melomans.Core.Models;

namespace Melomans.Core.Network
{
	public interface INetworkTaskFactory
	{
		INetworkTask<TMessage> CreateMulticastTask<TMessage>(TMessage message, IUdpSocketMulticastClient client)
			where TMessage: class, IMessage;

		INetworkTask<TMessage> CreateAddressTask<TMessage>(Meloman meloman, TMessage message)
			where TMessage : class, IMessage;

		INetworkTask<TMessage> CreateReceivedTask<TMessage>(Meloman meloman, IRemoteClient client)
			where TMessage: class, IMessage;
	}
}
