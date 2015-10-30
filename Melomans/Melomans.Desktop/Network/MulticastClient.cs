using System;
using System.Threading.Tasks;
using Melomans.Core.Network;
using Sockets.Plugin;
using Sockets.Plugin.Abstractions;

namespace Melomans.Desktop.Network
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
            _client.MessageReceived += MessageReceived;

        }

        private void MessageReceived(object sender, Sockets.Plugin.Abstractions.UdpSocketMessageReceivedEventArgs udpSocketMessageReceivedEventArgs)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _client.MessageReceived -= MessageReceived;
            _client.Dispose();
        }

       
        public Task JoinMulticastGroupAsync()
        {
            throw new NotImplementedException();
        }

        public Task DisconnectAsync()
        {
            throw new NotImplementedException();
        }

        public Task SendMulticastAsync(byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
