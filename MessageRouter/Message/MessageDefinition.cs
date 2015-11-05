using MessageRouter.Network;

namespace MessageRouter.Message
{
	public class MessageDefinition
	{
		public string MessageId { get; set; }

		public AccessGroups AccessGroup { get; set; }
	}
}
