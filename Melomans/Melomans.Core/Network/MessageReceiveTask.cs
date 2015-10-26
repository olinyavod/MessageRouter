using System;
using System.IO;
using System.Text;
using System.Threading;
using Melomans.Core.Message;
using Melomans.Core.Models;

namespace Melomans.Core.Network
{
	public class MessageReceiveTask<TMessage> : NetworkTaskBase<TMessage>
		where TMessage: class, IMessage
	{
		private readonly Meloman _meloman;
		private readonly IRemoteClient _client;
		private readonly IMessageSerializer _messageSerializer;
		private TMessage _message;

		public MessageReceiveTask(Meloman meloman, IRemoteClient client, IMessageSerializer messageSerializer)
		{
			_meloman = meloman;
			_client = client;
			_messageSerializer = messageSerializer;
		}

		public override Meloman For { get { return _meloman; } }

		protected override TMessage Message { get { return _message; } }

		protected override async void Run(CancellationToken cancellationToken)
		{
			Stream stream = null;
			try
			{
				_message = await _messageSerializer.ReadMessage<TMessage>(_client.ReadStream);
				byte[] buffer;
				if (Message != null && _client.WriteStream.CanWrite)
				{
					buffer = Encoding.UTF8.GetBytes(NetworkState.Ok.ToString());
					await _client.WriteStream.WriteAsync(buffer, 0, buffer.Length, cancellationToken);
					await _client.WriteStream.FlushAsync(cancellationToken);
				}
				if (IsCancellationRequested)
					throw new OperationCanceledException();
				var streaming = _message as IStreamingMessage;
				if (streaming != null)
				{
					var readedCount = 0;
					ulong allReaded = 0;
					RaiseReport(new ProgressInfo<TMessage>(Message, streaming.StreamLength, allReaded));
					buffer = new byte[2048];
				    stream = RaiseGetStream(Message) ?? streaming.Stream;
					do
					{
						if(IsCancellationRequested)
							throw new OperationCanceledException();
						readedCount = await _client.ReadStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
						await stream.WriteAsync(buffer, 0, readedCount, cancellationToken);
						await stream.FlushAsync(cancellationToken);
						allReaded += (ulong)readedCount;
						RaiseReport(new ProgressInfo<TMessage>(Message, streaming.StreamLength, allReaded));
					} while (readedCount > 0);
				}
				RaiseSuccess(Message);
				await _client.DisconnectAsync();
			}
			finally
			{
				if(stream != null)
					stream.Dispose();
				_client.Dispose();
			}

		}
	}
}
