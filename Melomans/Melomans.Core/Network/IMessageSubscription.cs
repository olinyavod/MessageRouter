using System;
using MessageRouter.Message;
using MessageRouter.Models;

namespace MessageRouter.Network
{
	public interface IMessageSubscription:IDisposable
	{
		MessageDefinition Definition { get; }

		void ReceivedMessage(Meloman meloman, IRemoteClient client);
	}
}