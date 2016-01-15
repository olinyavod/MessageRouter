using Sockets.Plugin;
using Sockets.Plugin.Abstractions;
using Module.MessageRouter.Abstractions.Network;

namespace Module.MessageRouter.Mobile.Network
{
	public class MobileNetworkSettings: INetworkSettings
    {
        public MobileNetworkSettings(int listenPort)
        {
            TTL = 5;
            MulticastPort = 30307;
            MulticastAddress = "224.0.0.1";
            Adapters = new CommsInterface();
        }

        public int TTL { get; set; }

        public int ListenPort { get; set; }

        public string MulticastAddress { get; set; }
        public ICommsInterface Adapters { get; set; }
        public int MulticastPort { get; set; }
    }
}