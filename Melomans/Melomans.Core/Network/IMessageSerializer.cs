using System.IO;
using System.Threading.Tasks;

namespace Melomans.Core.Network
{
	public interface IMessageSerializer
	{
		Task WriteMessage<TMessage>(TMessage message, Stream stream);

		Task<TMessage> ReadMessage<TMessage>(Stream stream);
	}
}
