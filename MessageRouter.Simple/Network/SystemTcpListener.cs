using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Module.MessageRouter.Abstractions.Network;

namespace MessageRouter.Simple.Network
{
	internal class SystemTcpListener : ITcpListener
	{
		private readonly NetworkSettings _settings;
		private readonly TcpListener _listener;

		public SystemTcpListener(NetworkSettings settings)
		{
			_settings = settings;
			_listener = new TcpListener(IPAddress.Any, 0);
		}

		private void OnConnectionReceived(object sender, ListenerConnectEventArgs e)
		{
			ConnectionReceived?.Invoke(sender, e);
		}

		public void Dispose()
		{
			_listener.Stop();
		}

		public event EventHandler<ListenerConnectEventArgs> ConnectionReceived;

		public async Task StartListeningAsync()
		{
			_listener.Start();
			_settings.ListenPort = ((IPEndPoint) _listener.LocalEndpoint).Port;
			while (true)
			{
				var r = await _listener.AcceptTcpClientAsync();
				var remoteEndPoint = (IPEndPoint) r.Client.RemoteEndPoint;
				OnConnectionReceived(_listener,
					new ListenerConnectEventArgs(remoteEndPoint.Address.ToString(), remoteEndPoint.Port, new TcpRemoteClient(r)));
			}
		}

		public Task StopListeningAsync()
		{
			return Task.Run(() => _listener.Stop());
		}
	}
}
