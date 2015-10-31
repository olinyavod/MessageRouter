using System;
using System.Threading.Tasks;
using Melomans.Core.Network;
using Sockets.Plugin;
using Sockets.Plugin.Abstractions;

namespace Melomans.Desktop.Network
{
    class TcpListener : ITcpListener
    {
        private readonly NetworkSettings _settings;
        private TcpSocketListener _listener;

        public TcpListener(NetworkSettings settings)
        {
            _settings = settings;
            _listener = new TcpSocketListener();
            _listener.ConnectionReceived += OnConnectionReceived;
        }

        private void OnConnectionReceived(object sender, TcpSocketListenerConnectEventArgs e)
        {
            if(ConnectionReceived != null)
                ConnectionReceived(sender, new ListenerConnectEventArgs(e.SocketClient.RemoteAddress, e.SocketClient.RemotePort, new TcpRemoteClient(e.SocketClient)));
        }

        public void Dispose()
        {
            _listener.ConnectionReceived -= OnConnectionReceived;
            _listener.Dispose();
        }

        public event EventHandler<ListenerConnectEventArgs> ConnectionReceived;

        public Task StartListeningAsync()
        {
            return _listener.StartListeningAsync(_settings.ListenPort, _settings.Adaptes);
        }

        public Task StopListeningAsync()
        {
            return _listener.StopListeningAsync();
        }
    }
}
