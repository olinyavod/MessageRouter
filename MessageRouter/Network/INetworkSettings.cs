namespace Module.MessageRouter.Abstractions.Network
{
	public interface INetworkSettings
	{
		string MulticastAddress { get; }

		int MulticastPort { get; }

		int BufferSize { get; }

		int ListenPort { get; }
		int TTL { get; set; }
	}
}
