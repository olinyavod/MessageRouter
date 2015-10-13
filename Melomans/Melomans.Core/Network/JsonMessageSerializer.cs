using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace Melomans.Core.Network
{
	public class JsonMessageSerializer : IMessageSerializer
	{
		public Task<TMessage> ReadMessage<TMessage>(Stream stream)
		{
			var serializer = new DataContractSerializer(typeof(TMessage));
			return Task.Run(() => ((TMessage) serializer.ReadObject(stream)));
		}

		public Task WriteMessage<TMessage>(TMessage message, Stream stream)
		{
			var serializer = new DataContractJsonSerializer(typeof(TMessage));
			return Task.Run(() => serializer.WriteObject(stream, message));
		}
	}
}
