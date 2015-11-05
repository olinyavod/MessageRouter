using System;
using System.IO;
using MessageRouter.Message;
using MessageRouter.Models;

namespace MessageRouter.Network
{
	public interface IMessageReceiverConfig<TMessage>:IDisposable
		where TMessage:class, IMessage
	{
		IMessageReceiverConfig<TMessage> OnStart(Action<Meloman> onStart);

		IMessageReceiverConfig<TMessage> OnStart(Action onStart);

		IMessageReceiverConfig<TMessage> OnException(Action<Meloman, Exception> onException);

		IMessageReceiverConfig<TMessage> OnException(Action<Exception> onException);

		IMessageReceiverConfig<TMessage> OnFinally(Action<Meloman, TMessage> onFinally);

		IMessageReceiverConfig<TMessage> OnFinally(Action<TMessage> onFinnally);

		IMessageReceiverConfig<TMessage> OnSuccess(Action<Meloman, TMessage> onSuccess);

		IMessageReceiverConfig<TMessage> OnSuccess(Action<TMessage> onSuccess);

		IMessageReceiverConfig<TMessage> OnGetWriter(Func<Meloman, TMessage, Stream> onGetWriter);

		IMessageReceiverConfig<TMessage> OnGetWriter(Func<TMessage, Stream> onGetWriter);

		IMessageReceiverConfig<TMessage> OnReport(Action<Meloman, ProgressInfo<TMessage>> onReport);

		IMessageReceiverConfig<TMessage> OnReport(Action<ProgressInfo<TMessage>> onReport);

		IMessageReceiverConfig<TMessage> OnCancelled(Action<TMessage> onCancelled);

		IMessageReceiverConfig<TMessage> OnCancelled(Action<Meloman, TMessage> onCancelled);

		void Cancel();

		bool CanCancel { get; }


	}
}
