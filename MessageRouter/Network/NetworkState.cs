using System.Runtime.Serialization;

namespace MessageRouter.Network
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
