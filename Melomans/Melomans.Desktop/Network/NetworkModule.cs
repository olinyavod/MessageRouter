using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Melomans.Core.Message;
using Melomans.Core.Network;

namespace Melomans.Desktop.Network
{
    class NetworkModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<NetworkSettings>()
                .SingleInstance();

            builder.RegisterType<NetworkClientFactory>()
                .As<INetworkClientFactory>()
                .SingleInstance();

            builder.RegisterType<JsonMessageSerializer>()
                .As<IMessageSerializer>()
                .SingleInstance();

            builder.RegisterType<MessageService>()
                .As<IMessageService>()
                .SingleInstance();

            builder.RegisterType<NetworkTaskFactory>()
                .As<INetworkTaskFactory>()
                .SingleInstance();

            builder.RegisterType<NetworkMessagesRouter>()
                .As<INetworkMessageRouter>()
                .SingleInstance();


        }
    }
}
