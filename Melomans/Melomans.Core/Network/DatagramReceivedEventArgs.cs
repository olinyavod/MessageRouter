using System;

namespace MessageRouter.Network
{
	public class DatagramReceivedEventArgs:EventArgs
	{
		public DatagramReceivedEventArgs(string remoteAddress, string remotePort, byte[] data)
		{
			RemoteAddress = remoteAddress;
			RemotePort = remotePort;
			Data = data;
		}

		public string RemoteAddress { get; private set; }

		public string RemotePort { get; private set; }

		public byte[] Data { get; private set; }
	}
}