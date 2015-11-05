namespace MessageRouter.Network
{
	public interface INetworkSettngs
	{
		string MulticastAddress { get; }

		int MulticastPort { get; }

		int BufferSize { get; }

		int ListenPort { get; }
		int TTL { get; set; }
	}
}
