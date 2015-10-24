using System;
using System.IO;
using System.Threading.Tasks;

namespace Melomans.Core.Network
{
	public interface IRemoteClient:IDisposable
	{
		Stream ReadStream { get; }

		Stream WriteStream { get; }

		Task DisconnectAsync();
	}
}
