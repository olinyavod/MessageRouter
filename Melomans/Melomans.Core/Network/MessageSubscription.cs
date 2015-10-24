using System;
using System.Collections.Generic;
using Melomans.Core.Message;
using Melomans.Core.Models;

namespace Melomans.Core.Network
{
	class MessageSubscription<TMessage> : IMessageSubscription
		where TMessage : class, IMessage
	{
		private readonly long _id;
		private readonly INetworkTaskFactory _taskFactory;
		private readonly Action<INetworkTask<TMessage>> _onReceived;
		private readonly IDictionary<long, IMessageSubscription> _subscriptions;

		public MessageSubscription(
			long id,
			MessageDefinition messageDefinitione,
			INetworkTaskFactory taskFactory,
			Action<INetworkTask<TMessage>> onReceived,
			IDictionary<long, IMessageSubscription> subscriptions)
		{
			_id = id;
			_taskFactory = taskFactory;
			_onReceived = onReceived;
			_subscriptions = subscriptions;
			Definition = messageDefinitione;
		}

		public MessageDefinition Definition { get; private set; }

		public void ReceivedMessage(Meloman meloman, IRemoteClient client)
		{
			_onReceived(_taskFactory.CreateReceivedTask(meloman, client));
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					_subscriptions.Remove(_id);
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~MessageSubscription() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion
	}
}
