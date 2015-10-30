namespace Melomans.Core.Network
{
	public class DatagramReceivedEventArgs
	{
		public DatagramReceivedEventArgs(string remoteAddress, int port, byte[] data)
		{
			RemoteAddress = remoteAddress;
			Port = port;
			Data = data;
		}

		public string RemoteAddress { get; private set; }

		public int Port { get; private set; }

		public byte[] Data { get; private set; }
	}
}