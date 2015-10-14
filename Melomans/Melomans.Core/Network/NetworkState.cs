using System.Runtime.Serialization;

namespace Melomans.Core.Network
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
		SendingCancelled;
	}
}
