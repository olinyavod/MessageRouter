namespace Module.MessageRouter.Abstractions.Network
{
	public interface INetworkClientFactory
	{
		IMulticastClient CreateMulticastClient();

		ITcpListener CreateListener();

		ITcpClient CreateTcpClient();
	}
}
