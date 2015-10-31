using System;
using System.Threading.Tasks;
using Melomans.Core.Network;
using Sockets.Plugin;
using Sockets.Plugin.Abstractions;

namespace Melomans.Windows.Network
{
    class MulticastClient:IMulticastClient
    {
        private readonly NetworkSettings _settings;
        private readonly IUdpSocketMulticastClient _client;

        public MulticastClient(NetworkSettings settings)
        {
            _settings = settings;
            _client = new UdpSocketMulticastClient
            {
                TTL = settings.TTL
            };
            _client.MessageReceived += OnMessageReceived;

        }

        private void OnMessageReceived(object sender, UdpSocketMessageReceivedEventArgs e)
        {
            if(MessageReceived != null)
                MessageReceived(sender, new DatagramReceivedEventArgs(e.RemoteAddress, e.RemotePort, e.ByteData));
        }

        public void Dispose()
        {
            _client.MessageReceived -= OnMessageReceived;
            _client.Dispose();
        }


        public event EventHandler<DatagramReceivedEventArgs> MessageReceived;

        public Task JoinMulticastGroupAsync()
        {
            return _client.JoinMulticastGroupAsync(_settings.MulticastAddress, _settings.MulticastPort,
                _settings.Adaptes);
        }

        public Task DisconnectAsync()
        {
           return _client.DisconnectAsync();
        }

        public Task SendMulticastAsync(byte[] data)
        {
			return _client.SendMulticastAsync (data);
        }
    }
}
