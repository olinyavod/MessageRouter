using System;
using System.Threading.Tasks;

namespace Melomans.Core.Network
{
	public interface ITcpListener:IDisposable
	{
	    event EventHandler<ListenerConnectEventArgs> ConnectionReceived;
		
		Task StartListeningAsync();

		Task StopListeningAsync();

	}
}
