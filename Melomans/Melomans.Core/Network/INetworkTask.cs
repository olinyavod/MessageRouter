using Melomans.Core.Models;
using System;
using Melomans.Core.Message;

namespace Melomans.Core.Network
{
	public interface INetworkTask<TMessage>
		where TMessage: class, IMessage
	{
		void Run();

		Meloman For { get; }

		INetworkTask<TMessage> OnException(Action<Exception> onCatch);

		INetworkTask<TMessage> OnComplite(Action<TMessage> onComplite);

		INetworkTask<TMessage> OnReport(Action<int> onReport);

		void Cancel();


	}
}
