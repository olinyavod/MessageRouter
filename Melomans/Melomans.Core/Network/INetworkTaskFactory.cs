using System;
using Melomans.Core.Message;

namespace Melomans.Core.Network
{
	public interface INetworkTaskFactory
	{
		INetworkTask<TMessage> CreateMulticastTask<TMessage>(TMessage message)
			where TMessage: class, IMessage;

		INetworkTask<TMessage> CreateAddressTask<TMessage>(TMessage message)
			where TMessage : class, IMessage;

		INetworkTask<TMessage> CreateMulticastReaderTask<TMessage>() 
			where TMessage : class, IMessage;

		INetworkTask<TMessage> CreateAddressReaderTask<TMessage>()
			where TMessage : class, IMessage;
	}
}
