using Hubl.Core.Service;
using MessageRouter.Network;

namespace Hubl.Daemon.Network
{
	class NetworkClientFactory:INetworkClientFactory
	{
		private readonly UsersService _userService;
		private readonly NetworkSettings _networkSettings;

		public NetworkClientFactory(
			UsersService userService,
			NetworkSettings networkSettings)
		{
			_userService = userService;
			_networkSettings = networkSettings;
		}

		public IMulticastClient CreateMulticastClient()
		{
			return new SystemMulticastClient(_networkSettings);
		}

		public ITcpListener CreateListener()
		{
			return new SystemTcpListener(_networkSettings);
		}

		public ITcpClient CreateTcpClient()
		{
			return new SystemTcpClient(_userService);
		}
	}
}
