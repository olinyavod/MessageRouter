using System;
using System.Threading.Tasks;
using Module.MessageRouter.Abstractions.Network;

namespace MessageRouter.Network
{
	public interface ITcpListener:IDisposable
	{
	    event EventHandler<ListenerConnectEventArgs> ConnectionReceived;
		
		Task StartListeningAsync();

		Task StopListeningAsync();

	}
}
