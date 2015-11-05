using System;

namespace MessageRouter.Message
{
	public interface IMessageService
	{
		MessageDefinition GetDefinition(Type type);

		MessageDefinition GetDefinition<TMessage>()
			where TMessage : IMessage;

		bool CanSend(string userId, Type type);

		bool CanReceive(string userId, Type type);

		long CreateMessageHash(MessageDefinition messageDefinition);

	}
}