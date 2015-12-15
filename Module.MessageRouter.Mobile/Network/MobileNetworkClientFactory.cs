﻿using MessageRouter.Network;
using Module.MessageRouter.Abstractions;
using Module.MessageRouter.Abstractions.Network;

namespace Module.MessageRouter.Mobile.Network
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MobileNetworkClientFactory : INetworkClientFactory
    {
        private readonly MobileNetworkSettings _networkSettings;
        private readonly UsersService _usersService;

        public MobileNetworkClientFactory(MobileNetworkSettings networkSettings, UsersService usersService)
        {
            _networkSettings = networkSettings;
            _usersService = usersService;
        }

        #region INetworkClientFactory implementation

        public IMulticastClient CreateMulticastClient()
        {
            return new MobileMulticastClient(_networkSettings);
        }

        public ITcpListener CreateListener()
        {
            return new MobileTcpListener(_networkSettings);
        }

        public ITcpClient CreateTcpClient()
        {
            return new MobileTcpClient(_usersService);
        }

        #endregion
    }
}