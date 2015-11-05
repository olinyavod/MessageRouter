using System;
using System.IO;
using System.Threading.Tasks;

namespace MessageRouter.Network
{
	public interface IRemoteClient:IDisposable
	{
		Stream ReadStream { get; }

		Stream WriteStream { get; }

		Task DisconnectAsync();
	}
}
