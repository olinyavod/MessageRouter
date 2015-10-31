using System;
using Melomans.Core.Models;
using System.Collections.Generic;
using Melomans.Core.Message;

namespace Melomans.Core.Network
{
	public interface INetworkMessageRouter : IDisposable
	{
		void Start();

		void Stop();

		INetworkTask<TMessage> Publish<TMessage>(TMessage message)
			where TMessage : class, IMessage;
		
		IEnumerable<INetworkTask<TMessage>> PublishFor<TMessage>(IEnumerable<Meloman> melomans, TMessage message)
			where TMessage: class, IMessage;

		IMessageeceiverConfig<TMessage> Subscribe<TMessage>()
			where TMessage : class, IMessage;
	}
}
