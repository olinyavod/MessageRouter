using System;

namespace MessageRouter.Network
{
	public class DatagramReceivedEventArgs:EventArgs
	{
		public DatagramReceivedEventArgs(string remoteAddress, int remotePort, byte[] data)
		{
			RemoteAddress = remoteAddress;
			RemotePort = remotePort;
			Data = data;
		}

		public string RemoteAddress { get; private set; }

		public int RemotePort { get; private set; }

		public byte[] Data { get; private set; }
	}
}