using Sockets.Plugin;
using Sockets.Plugin.Abstractions;

namespace Module.MessageRouter.Mobile.Network
{
	public class MobileNetworkSettings
	{
		public int TTL {get; private set;}

		public int ListenPort { get; set;}

		public string MulticastAddress { get; set;}
		public ICommsInterface Adapters { get; set; }
		public int MulticastPort { get; set;}


		public MobileNetworkSettings (int listenPort)
		{
		    TTL = 5;
			MulticastPort = 30307;
			MulticastAddress = "224.0.0.1";
			Adapters = new CommsInterface ();
		}
	}
}

