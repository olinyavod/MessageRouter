using System.Runtime.Serialization;
using MessageRouter.Models;
using MessageRouter.Network;

namespace MessageRouter.Message
{
	[DataContract]
	[Message(AccessGroups.System)]
	public class HelloMessage : IMessage
	{
		[DataMember]
		public Meloman Meloman { get; set; }
	}
}
