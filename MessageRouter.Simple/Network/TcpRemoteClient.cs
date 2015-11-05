using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using MessageRouter.Network;

namespace MessageRouter.Simple.Network
{
	class TcpRemoteClient : IRemoteClient
	{
		private readonly TcpClient _client;

		public TcpRemoteClient(TcpClient client)
		{
			_client = client;
		    var stream = _client.GetStream();
		    WriteStream = stream;
		    ReadStream = stream;
		}

		public Stream ReadStream { get; private set; }

		public Stream WriteStream { get; private set; }

		public Task DisconnectAsync()
		{
		    return Task.Run(() => _client.Close());
		}

		public void Dispose()
		{
			
		}
	}
}
