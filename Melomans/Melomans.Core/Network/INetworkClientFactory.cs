namespace Melomans.Core.Network
{
	public interface INetworkClientFactory
	{
		IUdpSocketMulticastClient CreateMulticastClient();

		ITcpListener CreateListener();

		ITcpClient CreateTcpClient();
	}
}
