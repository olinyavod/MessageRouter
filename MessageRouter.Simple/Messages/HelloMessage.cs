using System.Runtime.Serialization;
using MessageRouter.Simple.Model;
using Module.MessageRouter.Abstractions.Message;
using Module.MessageRouter.Abstractions.Network;

namespace MessageRouter.Simple.Messages
{
	[DataContract]
	[Message(AccessGroups.System)]
	public class HelloMessage : IMessage
	{
		[DataMember]
		public User User { get; set; }
	}
}
