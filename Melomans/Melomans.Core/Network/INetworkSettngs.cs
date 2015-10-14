namespace Melomans.Core.Network
{
	public interface INetworkSettngs
	{
		string MulticastAddress { get; }

		int MulticastPort { get; }

		int BufferSize { get; }
	}
}
