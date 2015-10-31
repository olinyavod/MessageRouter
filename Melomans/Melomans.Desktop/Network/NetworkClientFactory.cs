using Melomans.Core.Network;

namespace Melomans.Desktop.Network
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
            return new MulticastClient(_networkSettings);
        }

        public ITcpListener CreateListener()
        {
            return new TcpListener(_networkSettings);
        }

        public ITcpClient CreateTcpClient()
        {
            return new TcpClient();
        }
    }
}
