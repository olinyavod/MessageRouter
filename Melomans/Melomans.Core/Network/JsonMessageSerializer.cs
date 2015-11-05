using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace MessageRouter.Network
{
	public class JsonMessageSerializer : IMessageSerializer
	{
		public async Task<TMessage> ReadMessage<TMessage>(Stream stream)
		{
			var serializer = new DataContractJsonSerializer(typeof(TMessage));
		    return await Task.Run(() => ((TMessage) serializer.ReadObject(stream)));
		}

		public Task WriteMessage<TMessage>(TMessage message, Stream stream)
		{
			var serializer = new DataContractJsonSerializer(typeof(TMessage));
			return Task.Factory.StartNew(() => serializer.WriteObject(stream, message));
		}
	}
}
