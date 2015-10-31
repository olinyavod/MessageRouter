using System.IO;
using System.Threading.Tasks;
using Melomans.Core.Network;
using Sockets.Plugin.Abstractions;

namespace Melomans.Windows.Network
{
	class TcpRemoteClient : IRemoteClient
	{
		private readonly ITcpSocketClient _client;

		public TcpRemoteClient(ITcpSocketClient client)
		{
			_client = client;
		}

		public Stream ReadStream
		{
			get { return _client.ReadStream; }
		}

		public Stream WriteStream
		{
			get { return _client.WriteStream; }
		}

		public Task DisconnectAsync()
		{
			return _client.DisconnectAsync();
		}

		public void Dispose()
		{
			_client.Dispose();
		}
	}
}
