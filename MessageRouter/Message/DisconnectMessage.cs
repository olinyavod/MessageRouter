using System.Runtime.Serialization;
using MessageRouter.Network;

namespace MessageRouter.Message
{
	[DataContract]
	[Message(AccessGroups.System)]
	public class DisconnectMessage
	{
		[DataMember]
		public string MelomandId { get; set; }
	}
}
