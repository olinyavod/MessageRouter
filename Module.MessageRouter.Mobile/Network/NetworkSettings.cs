using Sockets.Plugin;
using Sockets.Plugin.Abstractions;
using Module.MessageRouter.Abstractions.Network;

namespace Module.MessageRouter.Mobile.Network
{
	public class NetworkSettings: INetworkSettings
	{
		public int TTL {get;set;}

		public int ListenPort  { get; set;}

		public string MulticastAddress { get; set;}
		public ICommsInterface Adapters { get; set; }
		public int MulticastPort { get; set;}


		public NetworkSettings ()
		{
			TTL = 10;
			MulticastPort = 30307;
			ListenPort = 30303;
			MulticastAddress = "239.0.0.222";
			Adapters = null;
			var interfaces = Sockets.Plugin.CommsInterface.GetAllInterfacesAsync ().Result;
			foreach (var i in interfaces) {
				if (i.IsUsable && !i.IsLoopback) {
					//Adapters = i;
					break;
				}			
			}
		}
}
}