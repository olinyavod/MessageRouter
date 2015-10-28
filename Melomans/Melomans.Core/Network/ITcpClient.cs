using System;
using System.IO;
using System.Threading.Tasks;

namespace Melomans.Core.Network
{
	public interface ITcpClient : IDisposable
	{
		Stream ReadStream { get; }

		Stream WriteStream { get; }

		string RemoteAddress { get; }

		int RemotePort { get; }

		Task ConnectAsync(string address, int port);

		Task DisconnectAsync();
	}
}
