using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using MessageRouter.Network;

namespace Hubl.Daemon.Network
{
	class TcpRemoteClient : IRemoteClient
	{
		private readonly TcpClient _client;

		public TcpRemoteClient(TcpClient client)
		{
			_client = client;
		    var remoteEndPoint = (IPEndPoint)_client.Client.RemoteEndPoint;
            RemotePoint = new RemotePoint(remoteEndPoint.Port, remoteEndPoint.Address.ToString());
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

		public RemotePoint RemotePoint { get; private set; }
	}
}
