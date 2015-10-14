using System.IO;
using System.Runtime.Serialization;

namespace Melomans.Core.Message
{
	public interface IStreamingMessage
	{
		[IgnoreDataMember]
		Stream Stream { get; set; }
	}
}
