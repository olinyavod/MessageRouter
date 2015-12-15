using System;
using System.Threading.Tasks;
using Module.MessageRouter.Abstractions.Network;
using Sockets.Plugin;
using Sockets.Plugin.Abstractions;

namespace Module.MessageRouter.Mobile.Network
{
    public class MobileMulticastClient : IMulticastClient
    {
        private readonly MobileNetworkSettings _settings;


        private readonly IUdpSocketMulticastClient _udpClient;


        public MobileMulticastClient(MobileNetworkSettings settings)

        {
            _settings = settings;


            _udpClient = new UdpSocketMulticastClient();
            _udpClient.TTL = settings.TTL;
        }

        #region IDisposable implementation

        public void Dispose()

        {
            _udpClient.Dispose();
        }

        #endregion

        #region IMulticastClient implementation

        public event EventHandler<DatagramReceivedEventArgs> MessageReceived;

        public Task JoinMulticastGroupAsync()

        {
            return _udpClient.JoinMulticastGroupAsync(_settings.MulticastAddress, _settings.MulticastPort,
                _settings.Adapters);
        }


        public Task DisconnectAsync()

        {
            return _udpClient.DisconnectAsync();
        }


        public Task SendMulticastAsync(byte[] data)

        {
            return _udpClient.SendMulticastAsync(data);
        }

        #endregion
    }
}