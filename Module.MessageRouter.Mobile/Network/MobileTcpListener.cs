using System;
using System.Threading.Tasks;
using Hubl.Mobile;
using MessageRouter;
using MessageRouter.Network;
using Sockets.Plugin;

namespace Module.MessageRouter.Mobile.Network
{
	public class MobileTcpListener : ITcpListener
	{
		private readonly MobileNetworkSettings _settings;
		private readonly TcpSocketListener _listener;
		private readonly UsersService _userService;


		public MobileTcpListener (MobileNetworkSettings settings, UsersService userService)
		{
			_settings = settings;
			_listener = new TcpSocketListener ();
			_userService = userService;
		}
		#region ITcpListener
		public event EventHandler<ListenerConnectEventArgs> ConnectionReceived;

		public Task StartListeningAsync ()
		{
			return _listener.StartListeningAsync (_settings.ListenPort);
		}

		public Task StopListeningAsync ()
		{
			return _listener.StopListeningAsync ();
		}
		#endregion

		#region IDisposable implementation
		public void Dispose ()
		{
			_listener.Dispose ();
		}
		#endregion
	}
}

