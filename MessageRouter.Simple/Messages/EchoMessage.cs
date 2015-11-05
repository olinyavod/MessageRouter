using System.Runtime.Serialization;
using MessageRouter.Message;
using MessageRouter.Network;
using MessageRouter.Simple.Model;

namespace MessageRouter.Simple.Messages
{
	[DataContract]
	[Message(AccessGroups.System)]
	public class EchoMessage:IMessage
	{
		[DataMember]
		public User User { get; set; }
	}
}
