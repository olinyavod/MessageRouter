using Autofac;
using Hubl.Daemon.Network;
using MessageRouter.Message;
using MessageRouter.Network;

namespace Module.MessageRouter.Desktop.Network
{
	class NetworkModule:System.Reflection.Module
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
