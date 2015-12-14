using System;
using System.Reflection;
using MessageRouter.Message;
using MessageRouter.Network;

namespace Module.MessageRouter.Abstractions.Message
{
	public class MessageService : IMessageService
	{
		private short[] hashTable = new short[] {562, -6578, 334, 367, 990, 776, 6678, 235, 665, -12567, 987, 434, 7783, -7745 };

		public MessageDefinition GetDefinition(Type type)
		{
			var result = new MessageDefinition();
			var messageDefinition = type.GetTypeInfo().GetCustomAttribute<MessageAttribute>();
			result.MessageId = !string.IsNullOrWhiteSpace(messageDefinition?.MessageId) ? messageDefinition.MessageId : type.Name;
		    if (messageDefinition != null) result.AccessGroup = messageDefinition.Group;
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

		public long CreateMessageHash(MessageDefinition messageDefinition)
		{

			var messageId = messageDefinition.MessageId;
			if (string.IsNullOrWhiteSpace(messageId))
				return 0;
			long result = messageId[0];
			for (int i = 1; i < messageId.Length; i++)
			{
				var w = hashTable[i%hashTable.Length];
				result ^= (messageId[i]%w)*(messageId[i - 1]*w);
			}
			return result;
		}
	}
}
