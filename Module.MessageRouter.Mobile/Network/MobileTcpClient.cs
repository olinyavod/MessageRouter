using System;
using MessageRouter.Network;
using Sockets.Plugin.Abstractions;
using Sockets.Plugin;
using System.IO;
using System.Threading.Tasks;
using Hubl.Core.Service;

namespace Hubl.Mobile
{
	public class MobileTcpClient : ITcpClient
	{

		private readonly UsersService _usersService;
		private readonly TcpSocketClient _client;

		public MobileTcpClient(UsersService usersService) : this(usersService, new TcpSocketClient())
		{
		}

		public MobileTcpClient(UsersService usersService, TcpSocketClient client)
		{
			_usersService = usersService;
			_client = client;
		}

		#region ITcpClient implementation

		public Task ConnectAsync (string userId)
		{
			var user = _usersService.Get (userId);
			return _client.ConnectAsync(user.IpAddress, user.Port, false);
		}

		public Task DisconnectAsync ()
		{
			return _client.DisconnectAsync ();
		}

		public Stream ReadStream {
			get {	
				return _client.ReadStream;
			}
		}

		public Stream WriteStream {
			get {
				return _client.WriteStream;
			}
		}

		#endregion

		#region IDisposable implementation

		public void Dispose ()
		{
			_client.Dispose ();
		}

		#endregion

	}
}

