using System;
using Melomans.Core.Message;
using Melomans.Core.Models;
using System.IO;

namespace Melomans.Core.Network
{
	public interface IMessageeceiverConfig<TMessage>:IDisposable
		where TMessage:class, IMessage
	{
		IMessageeceiverConfig<TMessage> OnStart(Action<Meloman> onStart);

		IMessageeceiverConfig<TMessage> OnStart(Action onStart);

		IMessageeceiverConfig<TMessage> OnException(Action<Meloman, Exception> onException);

		IMessageeceiverConfig<TMessage> OnException(Action<Exception> onException);

		IMessageeceiverConfig<TMessage> OnFinally(Action<Meloman, TMessage> onFinally);

		IMessageeceiverConfig<TMessage> OnFinally(Action<TMessage> onFinnally);

		IMessageeceiverConfig<TMessage> OnSuccess(Action<Meloman, TMessage> onSuccess);

		IMessageeceiverConfig<TMessage> OnSuccess(Action<TMessage> onSuccess);

		IMessageeceiverConfig<TMessage> OnGetWriter(Func<Meloman, TMessage, Stream> onGetWriter);

		IMessageeceiverConfig<TMessage> OnGetWriter(Func<TMessage, Stream> onGetWriter);

		IMessageeceiverConfig<TMessage> OnReport(Action<Meloman, ProgressInfo<TMessage>> onReport);

		IMessageeceiverConfig<TMessage> OnReport(Action<ProgressInfo<TMessage>> onReport);

		IMessageeceiverConfig<TMessage> OnCancelled(Action<TMessage> onCancelled);

		IMessageeceiverConfig<TMessage> OnCancelled(Action<Meloman, TMessage> onCancelled);

		void Cancel();

		bool CanCancel { get; }


	}
}
