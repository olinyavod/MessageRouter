using System;
using System.Threading.Tasks;

namespace Melomans.Core.Network
{
	public interface IMulticastClient : IDisposable
	{
		event EventHandler<DatagramReceivedEventArgs> MessageReceived { get; set; }

		Task JoinMulticastGroupAsync();

		Task DisconnectAsync();

		Task SendMulticastAsync(byte[] data);

	}
}
