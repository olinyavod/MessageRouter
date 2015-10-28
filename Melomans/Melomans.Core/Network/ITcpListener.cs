using System;
using System.Threading.Tasks;

namespace Melomans.Core.Network
{
	public interface ITcpListener:IDisposable
	{
		EventHandler<TcpSocketListenerConnectEventArgs> ConnectionReceived { get; set; }
		
		Task StartListeningAsync();

		Task StopListeningAsync();

	}
}
