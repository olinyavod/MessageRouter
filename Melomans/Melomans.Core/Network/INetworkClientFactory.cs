namespace Melomans.Core.Network
{
	public interface INetworkClientFactory
	{
		IMulticastClient CreateMulticastClient();

		ITcpListener CreateListener();

		ITcpClient CreateTcpClient();
	}
}
