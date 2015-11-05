using MessageRouter.Message;
using MessageRouter.Models;

namespace MessageRouter.Network
{
	public interface INetworkTaskFactory
	{
		INetworkTask<TMessage> CreateMulticastTask<TMessage>(TMessage message, IMulticastClient client)
			where TMessage: class, IMessage;

		INetworkTask<TMessage> CreateAddressTask<TMessage>(Meloman meloman, TMessage message)
			where TMessage : class, IMessage;

		INetworkTask<TMessage> CreateReceivedTask<TMessage>(Meloman meloman, IRemoteClient client)
			where TMessage: class, IMessage;
	}
}
