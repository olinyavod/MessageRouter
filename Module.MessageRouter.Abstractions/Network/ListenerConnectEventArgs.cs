using System;
using Module.MessageRouter.Abstractions.Network.Interfaces;

namespace Module.MessageRouter.Abstractions.Network
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

	    // ReSharper disable once UnusedAutoPropertyAccessor.Global
	    // ReSharper disable once MemberCanBePrivate.Global
		public int RemotePort { get; private set; }

		public IRemoteClient RemoteClient { get; private set; }
	}
}