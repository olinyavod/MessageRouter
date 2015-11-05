using MessageRouter.Network;

namespace MessageRouter.Simple.Network
{
    class NetworkClientFactory:INetworkClientFactory
    {
        private readonly NetworkSettings _networkSettings;

        public NetworkClientFactory(NetworkSettings networkSettings)
        {
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
            return new SystemTcpClient();
        }
    }
}
