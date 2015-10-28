using System;
using System.Threading.Tasks;

namespace Melomans.Core.Network
{
	public interface IUdpSocketMulticastClient : IDisposable
	{
		EventHandler<UdpSocketMessageReceivedEventArgs> MessageReceived { get; set; }

		Task JoinMulticastGroupAsync();

		Task DisconnectAsync();

		Task SendMulticastAsync(byte[] data);

	}
}
