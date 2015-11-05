using System.Runtime.Serialization;
using MessageRouter.Network;

namespace MessageRouter.Simple.Messages
{
	[DataContract]
	[Message(AccessGroups.System)]
	public class DisconnectMessage
	{
		[DataMember]
		public string UserId { get; set; }
	}
}
