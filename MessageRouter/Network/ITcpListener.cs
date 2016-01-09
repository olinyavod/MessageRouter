using System;
using System.Threading.Tasks;

namespace Module.MessageRouter.Abstractions.Network
{
	public interface ITcpListener:IDisposable
	{
	    event EventHandler<ListenerConnectEventArgs> ConnectionReceived;
		
		Task StartListeningAsync();

		Task StopListeningAsync();

	}
}
