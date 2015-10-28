using Sockets.Plugin.Abstractions;
using System.IO;
using System.Threading.Tasks;

namespace Melomans.Droid.Networks
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
