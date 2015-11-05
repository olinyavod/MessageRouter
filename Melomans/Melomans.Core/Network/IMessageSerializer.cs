using System.IO;
using System.Threading.Tasks;

namespace MessageRouter.Network
{
	public interface IMessageSerializer
	{
		Task WriteMessage<TMessage>(TMessage message, Stream stream);

		Task<TMessage> ReadMessage<TMessage>(Stream stream);
	}
}
