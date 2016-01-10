using System.Runtime.Serialization;

namespace Module.MessageRouter.Abstractions.Network
{
	[DataContract]
	public enum NetworkState
	{
		[EnumMember]
		Ok,

		[EnumMember]
		AccessDenied,

		[EnumMember]
		Error,

		[EnumMember]
		SendingCancelled
	}
}
