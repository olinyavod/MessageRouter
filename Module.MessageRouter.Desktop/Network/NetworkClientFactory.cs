using MessageRouter.Network;
using Module.MessageRouter.Abstractions;

namespace Module.MessageRouter.Desktop.Network
{
    internal class NetworkClientFactory : INetworkClientFactory
    {
        private readonly NetworkSettings _networkSettings;
        private readonly UsersService _userService;

        public NetworkClientFactory(
            UsersService userService,
            NetworkSettings networkSettings)
        {
            _userService = userService;
            _networkSettings = networkSettings;
        }

        public IMulticastClient CreateMulticastClient()
        {
            return new SystemMulticastClient(_networkSettings);
        }

        public ITcpListener CreateListener()
        {
            return new SystemTcpListener(_networkSettings);
        }

        public ITcpClient CreateTcpClient()
        {
            return new SystemTcpClient(_userService);
        }
    }
}