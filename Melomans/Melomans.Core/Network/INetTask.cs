using Melomans.Core.Models;
using System;

namespace Melomans.Core.Network
{
	public interface INetTask<TMessage>
		where TMessage: class, IMessage
	{
		void Run();

		Meloman For { get; }

		INetTask<TMessage> OnException(Action<Exception> onCatch);

		INetTask<TMessage> OnComplite(Action<TMessage> onComplite);

		INetTask<TMessage> OnReport(Action<int> onReport);

		void Cancel();


	}
}
