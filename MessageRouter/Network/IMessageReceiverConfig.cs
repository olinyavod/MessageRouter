using System;
using System.IO;
using MessageRouter.Message;

namespace MessageRouter.Network
{
	public interface IMessageReceiverConfig<TMessage>:IDisposable
		where TMessage:class, IMessage
	{
		IMessageReceiverConfig<TMessage> OnStart(Action<RemotePoint> onStart);

		IMessageReceiverConfig<TMessage> OnStart(Action onStart);

		IMessageReceiverConfig<TMessage> OnException(Action<RemotePoint, Exception> onException);

		IMessageReceiverConfig<TMessage> OnException(Action<Exception> onException);

		IMessageReceiverConfig<TMessage> OnFinally(Action<RemotePoint, TMessage> onFinally);

		IMessageReceiverConfig<TMessage> OnFinally(Action<TMessage> onFinnally);

		IMessageReceiverConfig<TMessage> OnSuccess(Action<RemotePoint, TMessage> onSuccess);

		IMessageReceiverConfig<TMessage> OnSuccess(Action<TMessage> onSuccess);

		IMessageReceiverConfig<TMessage> OnGetWriter(Func<RemotePoint, TMessage, Stream> onGetWriter);

		IMessageReceiverConfig<TMessage> OnGetWriter(Func<TMessage, Stream> onGetWriter);

		IMessageReceiverConfig<TMessage> OnReport(Action<RemotePoint, ProgressInfo<TMessage>> onReport);

		IMessageReceiverConfig<TMessage> OnReport(Action<ProgressInfo<TMessage>> onReport);

		IMessageReceiverConfig<TMessage> OnCancelled(Action<TMessage> onCancelled);

		IMessageReceiverConfig<TMessage> OnCancelled(Action<RemotePoint, TMessage> onCancelled);

		void Cancel();

		bool CanCancel { get; }


	}
}
