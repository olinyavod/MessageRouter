using System.IO;
using System.Runtime.Serialization;

namespace MessageRouter.Message
{
	public interface IStreamingMessage
	{
		/// <summary>Длина стрима</summary>
		[DataMember]
		ulong StreamLength { get; set; }

		/// <summary>Стрим который нужно передать</summary>
		[IgnoreDataMember]
		Stream Stream { get; set; }
	}
}
