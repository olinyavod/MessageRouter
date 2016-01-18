using System;
using System.IO;
using Module.MessageRouter.Abstractions.Message;

namespace Module.MessageRouter.Abstractions.Network.Interfaces
{
	public interface INetworkTask<TMessage>
		where TMessage: class, IMessage
	{
		void Run();

		INetworkTask<TMessage> OnStart(Action<TMessage> onStart);

		INetworkTask<TMessage> OnCancelled(Action<TMessage> onCancelled);
			
		INetworkTask<TMessage> OnException(Action<Exception> onCatch);

		INetworkTask<TMessage> OnSuccess(Action<TMessage> onSuccess);

		INetworkTask<TMessage> GetStream(Func<TMessage, Stream> getStream);
			
		INetworkTask<TMessage> OnFinally(Action<TMessage> onFinally);

		INetworkTask<TMessage> OnReport(Action<ProgressInfo<TMessage>> onReport);

		void Cancel();


	}
}
