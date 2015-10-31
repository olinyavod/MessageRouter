namespace Melomans.Core.Network
{
	public class DatagramReceivedEventArgs
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