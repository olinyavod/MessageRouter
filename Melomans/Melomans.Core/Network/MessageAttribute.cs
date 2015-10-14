using System;

namespace Melomans.Core.Network
{
	[AttributeUsage(AttributeTargets.Class)]
	public class MessageAttribute:Attribute
	{
		public AccessGroups Group { get; private set; }

		public MessageAttribute(AccessGroups group)
		{
			Group = @group;
		}
	}
}
