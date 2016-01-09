using System;
using System.Threading.Tasks;
using Module.MessageRouter.Abstractions.Network;
using Sockets.Plugin;

namespace Module.MessageRouter.Mobile.Network
{
    public class MobileTcpListener : ITcpListener
    {
        private readonly TcpSocketListener _listener;
        private readonly MobileNetworkSettings _settings;


        public MobileTcpListener(MobileNetworkSettings settings)
        {
            _settings = settings;
            _listener = new TcpSocketListener();
        }

        #region IDisposable implementation

        public void Dispose()
        {
            _listener.Dispose();
        }

        #endregion

        #region ITcpListener

        public event EventHandler<ListenerConnectEventArgs> ConnectionReceived;

        public Task StartListeningAsync()
        {
            return _listener.StartListeningAsync(_settings.ListenPort);
        }

        public Task StopListeningAsync()
        {
            return _listener.StopListeningAsync();
        }

        #endregion
    }
}