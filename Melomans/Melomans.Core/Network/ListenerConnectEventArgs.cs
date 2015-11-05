using System;

namespace MessageRouter.Network
{
	public class ListenerConnectEventArgs:EventArgs
	{
		public ListenerConnectEventArgs(string remoteAddress, int remotePort, IRemoteClient remoteClient)
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