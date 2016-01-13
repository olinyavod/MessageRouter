using System;
using System.Threading.Tasks;

namespace Module.MessageRouter.Abstractions.Network.Interfaces
{
	public interface IMulticastClient : IDisposable
	{
		event EventHandler<DatagramReceivedEventArgs> MessageReceived;

		Task JoinMulticastGroupAsync();

		Task DisconnectAsync();

		Task SendMulticastAsync(byte[] data);

	}
}
