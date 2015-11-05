using System;

namespace MessageRouter.Network
{
	[AttributeUsage(AttributeTargets.Class)]
	public class MessageAttribute:Attribute
	{
		public AccessGroups Group { get; private set; }

		public string MessageId { get; set; }

		public MessageAttribute(AccessGroups group)
		{
			Group = @group;
		}
	}
}
