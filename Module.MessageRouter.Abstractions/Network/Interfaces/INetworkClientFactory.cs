namespace Module.MessageRouter.Abstractions.Network.Interfaces
{
	public interface INetworkClientFactory
	{
		IMulticastClient CreateMulticastClient();

		ITcpListener CreateListener();

		ITcpClient CreateTcpClient();
	}
}
