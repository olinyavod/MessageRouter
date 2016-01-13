using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Module.MessageRouter.Abstractions.Message;
using Module.MessageRouter.Abstractions.Network.Interfaces;

namespace Module.MessageRouter.Abstractions.Network
{
	public class MessageReceiveTask<TMessage> : NetworkTaskBase<TMessage>
		where TMessage: class, IMessage
	{
		
		private readonly IRemoteClient _client;
		private readonly IMessageSerializer _messageSerializer;
		private TMessage _message;

		public MessageReceiveTask(IRemoteClient client, IMessageSerializer messageSerializer)
		{
			_client = client;
			_messageSerializer = messageSerializer;
		}

		protected override TMessage Message { get { return  _message; } }


	    protected override async Task Run(CancellationToken cancellationToken)
	    {
	        Stream stream = null;
	        try
	        {
	            var buffer = Encoding.UTF8.GetBytes(NetworkState.Ok.ToString());
	            await Send(cancellationToken, _client.WriteStream, buffer);
	            using (var bufferStream = await ToMemoryStream(cancellationToken, _client.ReadStream))
	                _message = await _messageSerializer.ReadMessage<TMessage>(bufferStream);

	            if (Message != null && _client.WriteStream.CanWrite)
	            {
	                buffer = Encoding.UTF8.GetBytes(NetworkState.Ok.ToString());
	                await Send(cancellationToken, _client.WriteStream, buffer);
	            }
	            if (IsCancellationRequested)
	                throw new OperationCanceledException();
                //TODO:  Check this for proper work
	            var streaming = _message as IStreamingMessage;
	            if (streaming != null)
	            {
	                ulong allReaded = 0;
	                RaiseReport(new ProgressInfo<TMessage>(Message, streaming.StreamLength, allReaded));
	                buffer = new byte[2048];
	                stream = RaiseGetStream(Message) ?? streaming.Stream;
	                do
	                {
	                    if (IsCancellationRequested)
	                        throw new OperationCanceledException();
	                    var readed = await _client.ReadStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
	                    await stream.WriteAsync(buffer, 0, readed, cancellationToken);
	                    allReaded += (ulong) readed;
	                    await stream.FlushAsync(cancellationToken);
	                    RaiseReport(new ProgressInfo<TMessage>(Message, streaming.StreamLength, allReaded));
	                } while (allReaded < streaming.StreamLength);
	            }
	            RaiseSuccess(Message);

	        }
	        catch (OperationCanceledException)
	        {
	            RaiseCancelled(Message);
	        }
	        catch (Exception ex)
	        {
	            RaiseCatch(ex);
	        }

	        if (stream != null)
	            stream.Dispose();
            await _client.DisconnectAsync();
	        _client.Dispose();


	    }


	}
}
