using Module.MessageRouter.Abstractions.Network;

namespace Module.MessageRouter.Desktop.Network
{
    public class NetworkSettings: INetworkSettings
    {
        public NetworkSettings()
        {
            MulticastPort = 30307;
            ListenPort = 30303;
            MulticastAddress = "239.0.0.222";
        }

        public int TTL { get; set; }

        // public ICommsInterface Adaptes { get; set; }

        public int MulticastPort { get; private set; }
        public int ListenPort { get; set; }
        public string MulticastAddress { get; set; }
    }
}