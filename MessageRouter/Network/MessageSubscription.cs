using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MessageRouter.Message;
using MessageRouter.Models;

namespace MessageRouter.Network
{
	class MessageSubscription<TMessage> : IMessageSubscription
		where TMessage : class, IMessage
	{
		private readonly IList<MessageReceiveConfig> _receivers;
		private readonly long _id;
		private readonly INetworkTaskFactory _taskFactory;
		private readonly IDictionary<long, IMessageSubscription> _subscriptions;

		public MessageSubscription(
			long id,
			MessageDefinition messageDefinitione,
			INetworkTaskFactory taskFactory,
			IDictionary<long, IMessageSubscription> subscriptions)
		{
			_id = id;
			_taskFactory = taskFactory;
			_subscriptions = subscriptions;
			Definition = messageDefinitione;
			_receivers = new List<MessageReceiveConfig>();
		}

		public MessageDefinition Definition { get; private set; }

		public void ReceivedMessage(Meloman meloman, IRemoteClient client)
		{
			var receivedTask = _taskFactory.CreateReceivedTask<TMessage>(meloman, client);
			receivedTask
				.OnStart(delegate
				{
				    foreach (var r in _receivers)
				    {
				        r.CurrentTask = receivedTask;
				        r.RaiseOnStart(meloman);
				    }
				})
				.OnFinally(delegate(TMessage m)
				{
				    foreach (var r in _receivers)
				    {
				        r.CurrentTask = null;
				        r.RaiseOnFinally(meloman, m);
				    }
				})
				.OnException(ex =>
				{
					foreach (var r in _receivers)
						r.RaiseOnException(meloman, ex);
				}).GetStream(
				    delegate(TMessage m)
				    {
				        return _receivers.Select(i => i.OnGetWriter(meloman, m)).FirstOrDefault(i => i != null);
				    })
				.OnCancelled(delegate(TMessage m)
				{
				    foreach (var r in _receivers)
				        r.RaiseOnCancelled(meloman, m);
				})
				.OnSuccess(delegate(TMessage m)
				{
				    foreach (var r in _receivers)
				        r.RaiseSuccess(meloman, m);
				})
				.OnReport(delegate(ProgressInfo<TMessage> m)
				{
				    foreach (var r in _receivers)
				        r.RaisenReport(meloman, m);
				}).Run();
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
					_receivers.Clear();
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


		public IMessageReceiverConfig<TMessage> New()
		{
			var receiver = new MessageReceiveConfig(this, _receivers);
			_receivers.Add(receiver);
			return receiver;
		}

		private class MessageReceiveConfig : IMessageReceiverConfig<TMessage>
		{
			private Action<Meloman, TMessage> _onCancelled;

			private Action<Meloman, Exception> _onException;

			private Action<Meloman, TMessage> _onFinally;

			private Action<Meloman, TMessage> _onSuccess;

			private Func<Meloman, TMessage, Stream> _onGetWriter;

			private Action<Meloman> _onStart;

			private Action<Meloman, ProgressInfo<TMessage>> _onReport;

			private readonly IDisposable _parent;
			private readonly IList<MessageReceiveConfig> _receivers;

			public bool CanCancel
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			public MessageReceiveConfig(IDisposable parent,
				IList<MessageReceiveConfig> receivers)
			{
				_parent = parent;
				_receivers = receivers;
			}

			public INetworkTask<TMessage> CurrentTask { get; set; }

			public void Dispose()
			{
				_receivers.Remove(this);
				if(_receivers.Count == 0)
					_parent.Dispose();
				_onException = null;
				_onFinally = null;
				_onReport = null;
				_onGetWriter = null;
				_onSuccess = null;
				_onStart = null;
				_onCancelled = null;
			}

			public void RaiseOnCancelled(Meloman meloman, TMessage message)
			{
				if (_onCancelled != null)
					_onCancelled(meloman, message);
			}

			public void RaiseOnException(Meloman meloman, Exception ex)
			{
				if (_onException != null)
					_onException(meloman, ex);
			}

			public void RaiseOnFinally(Meloman meloman, TMessage message)
			{
				if (_onFinally != null)
					_onFinally(meloman, message);
			}

			public void RaiseOnStart(Meloman meloman)
			{
				if (_onStart != null)
					_onStart(meloman);
			}

			public void RaiseSuccess(Meloman meloman, TMessage message)
			{
				if (_onSuccess != null)
					_onSuccess(meloman, message);
			}

			public Stream OnGetWriter(Meloman meloman, TMessage message)
			{
				if (_onGetWriter != null)
					return _onGetWriter(meloman, message);
				return null;
			}

			public void RaisenReport(Meloman meloman, ProgressInfo<TMessage> info)
			{
				if (_onReport != null)
					_onReport(meloman, info);
			}

			public IMessageReceiverConfig<TMessage> OnException(Action<Exception> onException)
			{
				_onException = (meloman, exception) => onException(exception);
				return this;
			}

			public IMessageReceiverConfig<TMessage> OnException(Action<Meloman, Exception> onException)
			{
				_onException = onException;
				return this;
			}

			public IMessageReceiverConfig<TMessage> OnFinally(Action<TMessage> onFinnally)
			{
				_onFinally = (meloman, message) => onFinnally(message);
				return this;
			}

			public IMessageReceiverConfig<TMessage> OnFinally(Action<Meloman, TMessage> onFinally)
			{
				_onFinally = onFinally;
				return this;
			}

			public IMessageReceiverConfig<TMessage> OnGetWriter(Func<TMessage, Stream> onGetWriter)
			{
				_onGetWriter = (meloman, message) => onGetWriter(message);
				return this;
			}

			public IMessageReceiverConfig<TMessage> OnGetWriter(Func<Meloman, TMessage, Stream> onGetWriter)
			{
				_onGetWriter = onGetWriter;
				return this;
			}

			public IMessageReceiverConfig<TMessage> OnReport(Action<ProgressInfo<TMessage>> onReport)
			{
				_onReport = (meloman, info) => onReport(info);
				return this;
			}

			public IMessageReceiverConfig<TMessage> OnReport(Action<Meloman, ProgressInfo<TMessage>> onReport)
			{
				_onReport = onReport;
				return this;
			}

			public IMessageReceiverConfig<TMessage> OnStart(Action onStart)
			{
				_onStart = meloman => onStart();
				return this;
			}

			public IMessageReceiverConfig<TMessage> OnStart(Action<Meloman> onStart)
			{
				_onStart = onStart;
				return this;
			}

			public IMessageReceiverConfig<TMessage> OnSuccess(Action<TMessage> onSuccess)
			{
				_onSuccess = (meloman, message) => onSuccess(message);
				return this;
			}

			public IMessageReceiverConfig<TMessage> OnSuccess(Action<Meloman, TMessage> onSuccess)
			{
				_onSuccess = onSuccess;
				return this;
			}

			public IMessageReceiverConfig<TMessage> OnCancelled(Action<TMessage> onCancelled)
			{
				_onCancelled = (meloman, message) => onCancelled(message);
				return this;
			}

			public IMessageReceiverConfig<TMessage> OnCancelled(Action<Meloman, TMessage> onCancelled)
			{
				_onCancelled = onCancelled;
				return this;
			}

			public void Cancel()
			{
				CurrentTask.Cancel();
			}
		}


	}
}
