using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using MessageRouter.Network;
using Module.MessageRouter.Abstractions.Network;

namespace Module.MessageRouter.Desktop.Network
{
    internal class SystemTcpListener : ITcpListener
    {
        private readonly NetworkSettings _settings;
        private readonly TcpListener _listener;

        public SystemTcpListener(NetworkSettings settings)
        {
            _settings = settings;
            _listener = new TcpListener(_settings.ListenPort);
        }

        public void Dispose()
        {
            _listener.Stop();
        }

        public event EventHandler<ListenerConnectEventArgs> ConnectionReceived;

        public async Task StartListeningAsync()
        {
            _listener.Start();
            while (true)
            {
                var r = await _listener.AcceptTcpClientAsync();
                var remoteEndPoint = (IPEndPoint) r.Client.RemoteEndPoint;
                OnConnectionReceived(_listener,
                    new ListenerConnectEventArgs(remoteEndPoint.Address.ToString(), remoteEndPoint.Port,
                        new TcpRemoteClient(r)));
            }
        }

        public Task StopListeningAsync()
        {
            return Task.Run(() => _listener.Stop());
        }

        private void OnConnectionReceived(object sender, ListenerConnectEventArgs e)
        {
            if (ConnectionReceived != null)
                ConnectionReceived(sender, e);
        }
    }
}