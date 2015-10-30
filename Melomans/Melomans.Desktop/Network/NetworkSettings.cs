using Sockets.Plugin.Abstractions;

namespace Melomans.Desktop.Network
{
    class NetworkSettings
    {
        public int TTL { get; set; }

        public ICommsInterface Adaptes { get; set; }

        public int MulticastPort { get; set; }

        public int ListenPort { get; set; }
    }
}
