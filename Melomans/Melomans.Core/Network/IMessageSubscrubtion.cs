using Melomans.Core.Message;
using System.IO;

namespace Melomans.Core.Network
{
	public interface IMessageSubscrubtion
	{
		MessageDefinition Definition { get; }

		void ReceivedMessage(Stream stream);
	}
}