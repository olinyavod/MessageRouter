using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Module.MessageRouter.Abstractions.Message;

namespace Module.MessageRouter.Abstractions.Network
{
	public abstract class NetworkTaskBase<TMessage> : INetworkTask<TMessage>
		where TMessage : class, IMessage
	{
		private Action<TMessage> _onFinally;
		private Action<Exception> _onCatch;
		private Action<TMessage> _onSuccess;
		private Action<TMessage> _onStart;
		private Action<TMessage> _onCancelled;
		private Action<ProgressInfo<TMessage>> _onReport;
		private CancellationTokenSource _cancellationTokenSource;
		private Func<TMessage, Stream> _getStream;

        protected async Task<MemoryStream> ToMemoryStream(CancellationToken cancellationToken, Stream stream)
        {
            var result = new MemoryStream();
            try
            { 
                var buffer = new byte[4];
                await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
                var len = BitConverter.ToInt32(buffer, 0);
                buffer = new byte[len];
                await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
                await result.WriteAsync(buffer, 0, buffer.Length, cancellationToken);
                result.Flush();
                result.Seek(0, SeekOrigin.Begin);
                return result;
            }
            catch (Exception)
            {
                result.Dispose();
                return null;
            }
           
        }

        protected async Task Send(CancellationToken cancellationToken, Stream stream, byte[] buffer)
        {
            await stream.WriteAsync(BitConverter.GetBytes(buffer.Length), 0, 4, cancellationToken);
            await stream.WriteAsync(buffer, 0, buffer.Length, cancellationToken);
            await stream.FlushAsync(cancellationToken);
        }

		protected bool IsCancellationRequested {
			get { 
				return _cancellationTokenSource != null &&
				_cancellationTokenSource.IsCancellationRequested;
			}
		}
	    public NetworkTaskBase()
		{
			_cancellationTokenSource = new CancellationTokenSource();
		}

		protected void RaiseFinally(TMessage message)
		{
			if(_onFinally != null)
		    	_onFinally.Invoke(message);
		}

	    protected void RaiseCatch(Exception ex)
	    {
			if(_onCatch != null)
		        _onCatch.Invoke(ex);
	    }

	    protected void RaiseSuccess(TMessage message)
	    {
			if( _onSuccess != null)
	        _onSuccess.Invoke(message);
	    }

	    protected void RaiseStart(TMessage message)
	    {
			if( _onStart != null)
		        _onStart.Invoke(message);
	    }

	    protected void RaiseCancelled(TMessage message)
	    {
			if( _onCancelled != null)
				_onCancelled.Invoke(message);
	    }

	    protected void RaiseReport(ProgressInfo<TMessage> info)
	    {
			if( _onReport != null)
		        _onReport.Invoke(info);
	    }

	    protected Stream RaiseGetStream(TMessage message)
		{
			if( _getStream != null)
		        return _getStream.Invoke(message);
			return null;
		}

		public virtual void Cancel()
		{
			if (_cancellationTokenSource != null)
			    _cancellationTokenSource.Cancel();
		}

	    public virtual INetworkTask<TMessage> OnFinally(Action<TMessage> onFinally)
		{
			_onFinally = onFinally;
			return this;
		}

		public virtual INetworkTask<TMessage> OnException(Action<Exception> onCatch)
		{
			_onCatch = onCatch;
			return this;
		}

		public virtual INetworkTask<TMessage> OnReport(Action<ProgressInfo<TMessage>> onReport)
		{
			_onReport = onReport;
			return this;
		}

		public virtual INetworkTask<TMessage> OnSuccess(Action<TMessage> onSuccess)
		{
			_onSuccess = onSuccess;
			return this;
		}

		public virtual INetworkTask<TMessage> GetStream(Func<TMessage, Stream> getStream)
		{
			_getStream = getStream;
			return this;
		}

		public async void Run()
		{
			try
			{
				RaiseStart(Message);
				await Run(_cancellationTokenSource.Token);
			}
			catch (OperationCanceledException)
			{
				RaiseCancelled(Message);
			}
			catch (Exception ex)
			{
				RaiseCatch(ex);
			}
			finally
			{
				if( _cancellationTokenSource != null)
				    _cancellationTokenSource.Dispose();
			    RaiseFinally(Message);
				_onCatch = null;
				_onCancelled = null;
				_onFinally = null;
				_onReport = null;
				_onStart = null;
				_onSuccess = null;
				_getStream = null;
			}
		}

		protected abstract TMessage Message { get; }

		protected abstract Task Run(CancellationToken cancellationToken);

		public virtual INetworkTask<TMessage> OnStart(Action<TMessage> onStart)
		{
			_onStart = onStart;
			return this;
		}

		public virtual INetworkTask<TMessage> OnCancelled(Action<TMessage> onCancelled)
		{
			_onCancelled = onCancelled;
			return this;
		}
	}
}