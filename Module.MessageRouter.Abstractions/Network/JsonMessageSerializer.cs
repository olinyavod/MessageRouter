using System.IO;
using System.Text;
using System.Threading.Tasks;
using Module.MessageRouter.Abstractions.Network.Interfaces;
using Newtonsoft.Json;

namespace Module.MessageRouter.Abstractions.Network
{
	public class JsonMessageSerializer : IMessageSerializer
	{
		public async Task<TMessage> ReadMessage<TMessage>(Stream stream)
		{
			using (var reader = new StreamReader(stream, Encoding.UTF8))
			{
				return await Task.Run(() => JsonConvert.DeserializeObject<TMessage>(reader.ReadToEnd()));
			}
		}

		public Task WriteMessage<TMessage>(TMessage message, Stream stream)
		{
			return Task.Run(() =>
			{
				var result = JsonConvert.SerializeObject(message);
				using (var writer = new StreamWriter(stream, Encoding.UTF8))
				{
					writer.WriteLine(result);
				}
			});
		}
	}
}
