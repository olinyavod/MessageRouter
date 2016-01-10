using System;

namespace Module.MessageRouter.Abstractions.Network
{
	[AttributeUsage(AttributeTargets.Class)]
	public class MessageAttribute:Attribute
	{
		public string Group { get; private set; }

		public string MessageId { get; set; }

		public MessageAttribute(string group)
		{
			Group = @group;
		}
	}
}
