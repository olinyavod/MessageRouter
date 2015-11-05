using System;
using System.Collections.Generic;
using MessageRouter.Message;
using MessageRouter.Models;

namespace MessageRouter.Network
{
	public interface INetworkMessageRouter : IDisposable
	{
		void Start();

		void Stop();

		INetworkTask<TMessage> Publish<TMessage>(TMessage message)
			where TMessage : class, IMessage;
		
		IEnumerable<INetworkTask<TMessage>> PublishFor<TMessage>(IEnumerable<Meloman> melomans, TMessage message)
			where TMessage: class, IMessage;

		IMessageReceiverConfig<TMessage> Subscribe<TMessage>()
			where TMessage : class, IMessage;
	}
}
