using System.IO;
using System.Threading.Tasks;
using Module.MessageRouter.Abstractions;
using Module.MessageRouter.Abstractions.Network;
using Sockets.Plugin;

namespace Module.MessageRouter.Mobile.Network
{
    public class MobileTcpClient : ITcpClient
    {
        private readonly TcpSocketClient _client;
        private readonly IUsersService _usersService;

        public MobileTcpClient(IUsersService usersService) : this(usersService, new TcpSocketClient())
        {
        }

        private MobileTcpClient(IUsersService usersService, TcpSocketClient client)
        {
            _usersService = usersService;
            _client = client;
        }

        #region IDisposable implementation

        public void Dispose()
        {
            _client.Dispose();
        }

        #endregion

        #region ITcpClient implementation

        public Task ConnectAsync(string userId)
        {
            var user = _usersService.Get(userId);
            return _client.ConnectAsync(user.IpAddress, user.Port);
        }

        public Task DisconnectAsync()
        {
            return _client.DisconnectAsync();
        }

        public Stream ReadStream => _client.ReadStream;

        public Stream WriteStream => _client.WriteStream;

        #endregion
    }
}