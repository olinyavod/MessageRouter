﻿using System;
using Melomans.Core.Models;
using System.Collections.Generic;
using Melomans.Core.Message;

namespace Melomans.Core.Network
{
	public interface INetworkEventAgriggator : IDisposable
	{
		INetworkTask<TMessage> Publish<TMessage>(TMessage message)
			where TMessage : class, IMessage;
		
		IEnumerable<INetworkTask<TMessage>> PublishFor<TMessage>(IEnumerable<Meloman> melomans, TMessage message)
			where TMessage: class, IMessage;

		IDisposable Subscribe<TMessage>(Action<INetworkTask<TMessage>> action)
			where TMessage : class, IMessage;
	}
}