using System;
using Melomans.Core.Models;
using System.Collections.Generic;

namespace Melomans.Core.Network
{
	public interface INetworkEventAgriggator : IDisposable
	{
		void Publish<TMessage>(TMessage message)
			where TMessage : IMessage;

		IEnumerable<INetTask<TMessage>> PublishFor<TMessage>(IEnumerable<Meloman> melomans, TMessage message)
			where TMessage: IMessage;

		IDisposable Subscribe<TMessage>(Action<INetTask<TMessage>> action)
			where TMessage : class, IMessage;
	}
}
