namespace Melomans.Core.Network
{
	public class TcpSocketListenerConnectEventArgs
	{
		public TcpSocketListenerConnectEventArgs(string remoteAddress, int remotePort, IRemoteClient remoteClient)
		{
			RemoteAddress = remoteAddress;
			RemotePort = remotePort;
			RemoteClient = remoteClient;
		}
	    public string RemoteAddress { get; private set; }

		public int RemotePort { get; private set; }

		public IRemoteClient RemoteClient { get; private set; }
	}
}