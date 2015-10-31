using System.IO;
using System.Threading.Tasks;
using Melomans.Core.Network;
using Sockets.Plugin;

namespace Melomans.Desktop.Network
{
    class TcpClient:ITcpClient
    {
        private readonly TcpSocketClient _client;

        public TcpClient()
        {
            _client = new TcpSocketClient();
        }


        public void Dispose()
        {
            _client.Dispose();
        }

        public Stream ReadStream
        {
            get { return _client.ReadStream; }
        }

        public Stream WriteStream
        {
            get { return _client.WriteStream; }
        }

        public string RemoteAddress
        {
            get { return _client.RemoteAddress; }
        }

        public int RemotePort { get { return _client.RemotePort; } }

        public Task ConnectAsync(string address, int port)
        {
            return _client.ConnectAsync(address, port);
        }

        public Task DisconnectAsync()
        {
            return _client.DisconnectAsync();
        }
    }
}
