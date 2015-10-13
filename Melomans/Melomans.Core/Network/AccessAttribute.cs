using System;

namespace Melomans.Core.Network
{
	[AttributeUsage(AttributeTargets.Class)]
	public class AccessAttribute:Attribute
	{
		public AccessCroups Group { get; private set; }

		public AccessAttribute(AccessCroups group)
		{
			Group = @group;
		}
	}
}
