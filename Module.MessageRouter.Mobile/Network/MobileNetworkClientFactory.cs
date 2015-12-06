using System;
using MessageRouter.Network;
using Hubl.Core.Service;

namespace Hubl.Mobile
{
	public class MobileNetworkClientFactory: INetworkClientFactory
	{

		private readonly MobileNetworkSettings _networkSettings;
		private readonly UsersService _usersService;

		public MobileNetworkClientFactory(MobileNetworkSettings networkSettings, UsersService usersService)
		{
			_networkSettings = networkSettings;
			_usersService = usersService;
		}

		#region INetworkClientFactory implementation

		public IMulticastClient CreateMulticastClient ()
		{
			return new MobileMulticastClient (_networkSettings);
		}

		public ITcpListener CreateListener ()
		{
			return new MobileTcpListener (_networkSettings, _usersService);
		}

		public ITcpClient CreateTcpClient ()
		{
			return new MobileTcpClient (_usersService);
		}

		#endregion

	}
}

