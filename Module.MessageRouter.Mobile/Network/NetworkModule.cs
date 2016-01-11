using Autofac;
using Module.MessageRouter.Abstractions.Message;
using Module.MessageRouter.Abstractions.Network;

namespace Module.MessageRouter.Mobile.Network
{
    public class NetworkModule : Autofac.Module
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