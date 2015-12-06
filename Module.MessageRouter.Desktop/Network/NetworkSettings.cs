using System.Net;

namespace Hubl.Daemon.Network
{
    public class NetworkSettings
    {
        public NetworkSettings()
        {
            MulticastPort = 30307;
            ListenPort = 30303;
			MulticastAddress = "224.0.0.1";
        }
        public int TTL { get; set; }

        // public ICommsInterface Adaptes { get; set; }

        public int MulticastPort { get; private set; }

        public int ListenPort { get; set; }
        public string MulticastAddress { get; set; }
    }
}
