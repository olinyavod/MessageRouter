using System.Runtime.Serialization;
using Melomans.Core.Network;

namespace Melomans.Core.Message
{
	[DataContract]
	[Message(AccessGroups.System)]
	public class DisconnectMessage
	{
		[DataMember]
		public string MelomandId { get; set; }
	}
}
