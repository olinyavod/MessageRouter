using System;
using System.Reflection;
using Melomans.Core.Network;

namespace Melomans.Core.Message
{
	public class MessageService : IMessageService
	{
		public MessageDefinition GetDefinition(Type type)
		{
			var result = new MessageDefinition();
			var messageDefinition = type.GetTypeInfo().GetCustomAttribute<MessageAttribute>();
			if (messageDefinition != null && !string.IsNullOrWhiteSpace(messageDefinition.MessageId))
				result.MessageId = messageDefinition.MessageId;
			else result.MessageId = type.Name;
			result.AccessGroup = messageDefinition.Group;
			return result;
		}


		
		public MessageDefinition GetDefinition<TMessage>()
			where TMessage : IMessage
		{
			return GetDefinition(typeof(TMessage));
		}

		public bool CanSend(string userId, Type type)
		{
			return true;
		}

		public bool CanReceive(string userId, Type type)
		{
			return true;
		}
	}
}
