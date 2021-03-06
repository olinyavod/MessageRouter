﻿using System;
using System.Collections.Generic;
using Module.MessageRouter.Abstractions.Message;

namespace Module.MessageRouter.Abstractions.Network.Interfaces
{
	public interface INetworkMessageRouter : IDisposable
	{
		void Start();

		void Stop();

		INetworkTask<TMessage> Publish<TMessage>(TMessage message)
			where TMessage : class, IMessage;
		
		IEnumerable<INetworkTask<TMessage>> PublishFor<TMessage>(IEnumerable<string> userIds, TMessage message)
			where TMessage: class, IMessage;

		IMessageReceiverConfig<TMessage> Subscribe<TMessage>()
			where TMessage : class, IMessage;
	}
}
