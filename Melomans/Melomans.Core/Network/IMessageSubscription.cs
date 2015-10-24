using System;
using Melomans.Core.Message;
using System.IO;
using Melomans.Core.Models;

namespace Melomans.Core.Network
{
	public interface IMessageSubscription:IDisposable
	{
		MessageDefinition Definition { get; }

		void ReceivedMessage(Meloman meloman, IRemoteClient client);
	}
}