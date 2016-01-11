using Module.MessageRouter.Abstractions;
using Module.MessageRouter.Abstractions.Network;

namespace Module.MessageRouter.Mobile.Network
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class NetworkClientFactory : INetworkClientFactory
    {
        private readonly NetworkSettings _networkSettings;
        private readonly IUsersService _usersService;

        public NetworkClientFactory(NetworkSettings networkSettings, IUsersService usersService)
        {
            _networkSettings = networkSettings;
            _usersService = usersService;
        }

        #region INetworkClientFactory implementation

        public IMulticastClient CreateMulticastClient()
        {
            return new MulticastClient(_networkSettings);
        }

        public ITcpListener CreateListener()
        {
            return new TcpListener(_networkSettings);
        }

        public ITcpClient CreateTcpClient()
        {
            return new TcpClient(_usersService);
        }

        #endregion
    }
}