using Autofac;
using MessageRouter.Network;
using Module.MessageRouter.Abstractions.Message;
using Module.MessageRouter.Abstractions.Network;

namespace Module.MessageRouter.Mobile.Network
{
    internal class NetworkModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<MobileNetworkSettings>()
                .SingleInstance();

            builder.RegisterType<MobileNetworkClientFactory>()
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