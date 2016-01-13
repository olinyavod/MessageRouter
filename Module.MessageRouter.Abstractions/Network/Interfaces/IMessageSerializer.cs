using System.IO;
using System.Threading.Tasks;

namespace Module.MessageRouter.Abstractions.Network.Interfaces
{
	public interface IMessageSerializer
	{
		Task WriteMessage<TMessage>(TMessage message, Stream stream);

		Task<TMessage> ReadMessage<TMessage>(Stream stream);
	}
}
