using Sockets.Plugin.Abstractions;

namespace Melomans.Windows.Network
{
    public class NetworkSettings
    {
        public NetworkSettings()
        {
            MulticastPort = 30303;
            ListenPort = 30303;
        }
        public int TTL { get; set; }

        public ICommsInterface Adaptes { get; set; }

        public int MulticastPort { get; private set; }

        public int ListenPort { get; set; }
        public string MulticastAddress { get; set; }
    }
}
