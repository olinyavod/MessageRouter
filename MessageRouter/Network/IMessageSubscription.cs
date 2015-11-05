using System;
using MessageRouter.Message;

namespace MessageRouter.Network
{
	public interface IMessageSubscription:IDisposable
	{
		MessageDefinition Definition { get; }

		void ReceivedMessage(IRemoteClient client);
	}
}