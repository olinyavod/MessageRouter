using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using MessageRouter.Network;

namespace MessageRouter.Simple.Network
{
    class SystemTcpClient:ITcpClient
    {
        private readonly TcpClient _client;

        public SystemTcpClient():this(new TcpClient())
        {
        }

        public SystemTcpClient(TcpClient client)
        {
            _client = client;
        }


        public void Dispose()
        {
            _client.Close();
        }

        public Stream ReadStream { get; private set; }

        public Stream WriteStream { get; private set; }

        public string RemoteAddress { get; private set; }

        public int RemotePort { get; private set; }

        public async Task ConnectAsync(string address, int port)
        {
            RemoteAddress = address;
            RemotePort = port;
            await _client.ConnectAsync(address, port);
            ReadStream = _client.GetStream();
            WriteStream = ReadStream;
        }

        public Task DisconnectAsync()
        {
            return Task.Run(() => _client.Close());
        }
    }
}
