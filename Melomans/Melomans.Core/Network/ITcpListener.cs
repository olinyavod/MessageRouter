using System;
using System.Threading.Tasks;

namespace MessageRouter.Network
{
	public interface ITcpListener:IDisposable
	{
	    event EventHandler<ListenerConnectEventArgs> ConnectionReceived;
		
		Task StartListeningAsync();

		Task StopListeningAsync();

	}
}
