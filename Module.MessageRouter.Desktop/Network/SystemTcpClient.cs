using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using Module.MessageRouter.Abstractions;
using Module.MessageRouter.Abstractions.Network;

namespace Module.MessageRouter.Desktop.Network
{
    internal class SystemTcpClient : ITcpClient
    {
        private readonly TcpClient _client;
        private readonly IUsersService _usersService;

        public SystemTcpClient(IUsersService usersService) : this(usersService, new TcpClient())
        {
        }

        public SystemTcpClient(IUsersService usersService, TcpClient client)
        {
            _usersService = usersService;
            _client = client;
        }


        public void Dispose()
        {
            _client.Close();
        }

        public Stream ReadStream { get; private set; }

        public Stream WriteStream { get; private set; }

        public async Task ConnectAsync(string userId)
        {
            var user = _usersService.Get(userId);
            if (user == null)
                return;
            await _client.ConnectAsync(user.IpAddress, user.Port);
            ReadStream = _client.GetStream();
            WriteStream = ReadStream;
        }

        public Task DisconnectAsync()
        {
            return Task.Run(() => _client.Close());
        }
    }
}