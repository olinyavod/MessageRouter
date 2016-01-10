using System;
using Module.MessageRouter.Abstractions.Message;

namespace Module.MessageRouter.Abstractions.Network
{
	public interface IMessageSubscription:IDisposable
	{
		MessageDefinition Definition { get; }

		void ReceivedMessage(IRemoteClient client);
	}
}