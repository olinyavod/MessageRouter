using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using MessageRouter.Network;

namespace MessageRouter.Simple.Network
{
    class SystemMulticastClient:IMulticastClient
    {
        private readonly NetworkSettings _settings;
        private readonly UdpClient _client;

        public SystemMulticastClient(NetworkSettings settings)
        {
            
            _settings = settings;
            _client = new UdpClient(settings.MulticastPort, AddressFamily.InterNetwork) {Ttl = ((short) _settings.TTL)};
            _client.MulticastLoopback = true;
        }

        private void OnMessageReceived(object sender, DatagramReceivedEventArgs e)
        {
            if(MessageReceived != null)
                MessageReceived(sender, e);
        }

        public void Dispose()
        {
            //_client.DropMulticastGroup(IPAddress.Parse(_settings.MulticastAddress));
            _client.Close();
        }


        public event EventHandler<DatagramReceivedEventArgs> MessageReceived;

        public async Task JoinMulticastGroupAsync()
        {
            await Task.Run(() => _client.JoinMulticastGroup(IPAddress.Parse(_settings.MulticastAddress)));
            while (true)
            {
                var result = await _client.ReceiveAsync();
                OnMessageReceived(_client,
                    new DatagramReceivedEventArgs(result.RemoteEndPoint.Address.ToString(),
                        result.RemoteEndPoint.Port.ToString(), result.Buffer));
            }
            
        }

        public Task DisconnectAsync()
        {
            return Task.Run(() => _client.DropMulticastGroup(IPAddress.Parse(_settings.MulticastAddress)));
        }

        public Task SendMulticastAsync(byte[] data)
        {
            return _client.SendAsync(data, data.Length, new IPEndPoint(IPAddress.Parse(_settings.MulticastAddress), _settings.MulticastPort));
        }
    }
}
