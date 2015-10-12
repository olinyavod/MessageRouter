using System;
using Melomans.Core.Models;
using System.Collections.Generic;

namespace Melomans.Core.Network
{
	public interface INetworkEventAgriggator : IDisposable
	{
		IEnumerable<INetTask<TMessage>> Publish<TMessage>(TMessage message)
			where TMessage : class, IMessage;

		IEnumerable<INetTask<TMessage>> PublishFor<TMessage>(IEnumerable<Meloman> melomans, TMessage message)
			where TMessage : class, IMessage;

		IDisposable Subscribe<TMessage>(Action<INetTask<TMessage>> action)
			where TMessage : class, IMessage;
	}
}
