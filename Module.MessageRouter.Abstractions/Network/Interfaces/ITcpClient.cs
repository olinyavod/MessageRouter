using System;
using System.IO;
using System.Threading.Tasks;

namespace Module.MessageRouter.Abstractions.Network.Interfaces
{
	public interface ITcpClient : IDisposable
	{
		Stream ReadStream { get; }

		Stream WriteStream { get; }

	    Task ConnectAsync(string userId);

		Task DisconnectAsync();
	}
}
