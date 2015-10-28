namespace Melomans.Core.Network
{
	public class UdpSocketMessageReceivedEventArgs
	{
		public UdpSocketMessageReceivedEventArgs(string remoteAddress, int port, byte[] data)
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