namespace MessageRouter.Network
{
	public interface INetworkClientFactory
	{
		IMulticastClient CreateMulticastClient();

		ITcpListener CreateListener();

		ITcpClient CreateTcpClient();
	}
}
