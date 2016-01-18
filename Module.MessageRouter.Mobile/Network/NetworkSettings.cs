using Sockets.Plugin;
using Sockets.Plugin.Abstractions;
using Module.MessageRouter.Abstractions.Network;

namespace Module.MessageRouter.Mobile.Network
{
	public class NetworkSettings: INetworkSettings
	{
		public int TTL { get; private set; }

		public int ListenPort { get; private set; }

		public string MulticastAddress { get; private set; }
		public ICommsInterface Adapters { get; private set; }
		public int MulticastPort { get; private set; }


		public NetworkSettings()
		{
			TTL = 10;
			MulticastPort = 30307;
			ListenPort = 30303;
			MulticastAddress = "239.0.0.222";
			Adapters = null;
			var interfaces = CommsInterface.GetAllInterfacesAsync().Result;
			foreach (var i in interfaces)
			{
				if (i.IsUsable && !i.IsLoopback)
				{
					//Adapters = i;
					break;
				}
			}
		}
	}
}