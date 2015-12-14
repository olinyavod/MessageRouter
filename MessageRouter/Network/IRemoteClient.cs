using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.ServiceModel;
using System.Threading.Tasks;
using Module.MessageRouter.Abstractions.Network;

namespace MessageRouter.Network
{
	public interface IRemoteClient:IDisposable
	{
		Stream ReadStream { get; }

		Stream WriteStream { get; }

        RemotePoint RemotePoint { get; }

		Task DisconnectAsync();
	}
}
