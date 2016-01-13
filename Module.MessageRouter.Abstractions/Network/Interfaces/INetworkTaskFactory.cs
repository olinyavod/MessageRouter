using Module.MessageRouter.Abstractions.Message;

namespace Module.MessageRouter.Abstractions.Network.Interfaces
{
	public interface INetworkTaskFactory
	{
		INetworkTask<TMessage> CreateMulticastTask<TMessage>(TMessage message, IMulticastClient client)
			where TMessage: class, IMessage;

		INetworkTask<TMessage> CreateAddressTask<TMessage>(string address, TMessage message)
			where TMessage : class, IMessage;

		INetworkTask<TMessage> CreateReceivedTask<TMessage>(IRemoteClient client)
			where TMessage: class, IMessage;
	}
}
