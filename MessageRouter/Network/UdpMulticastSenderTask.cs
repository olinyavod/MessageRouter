using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MessageRouter.Network;
using Module.MessageRouter.Abstractions.Message;

namespace Module.MessageRouter.Abstractions.Network
{
	public class UdpMulticastSenderTask<TMessage> : NetworkTaskBase<TMessage>
		where TMessage:class, IMessage
	{
	    private readonly IMessageSerializer _serializer;
		private readonly IMessageService _messageService;
		private readonly IMulticastClient _client;

		public UdpMulticastSenderTask(TMessage message, 
			IMessageSerializer serializer,
			IMessageService messageService,
			IMulticastClient client)
		{
			Message = message;
			_serializer = serializer;
			_messageService = messageService;
			_client = client;
		}

		


		protected override async Task Run(CancellationToken cancellationToken)
		{
			MemoryStream stream = null;
			try
			{
				var definition = _messageService.GetDefinition<TMessage>();
			    var buffer = BitConverter.GetBytes(_messageService.CreateMessageHash(definition));
			    stream = new MemoryStream();
			    await stream.WriteAsync(buffer, 0, buffer.Length, cancellationToken);
			    using (var bufferStream = new MemoryStream())
			    {
			        await _serializer.WriteMessage(Message, bufferStream);
			        await Send(cancellationToken, stream, bufferStream.ToArray());
			    }
			    await _client.SendMulticastAsync(stream.ToArray());
				RaiseSuccess(Message);
			}
			finally
			{
			    stream?.Dispose();
			}
		}

		public override void Cancel()
		{
			throw new NotSupportedException();
		}

		public override INetworkTask<TMessage> GetStream(Func<TMessage, Stream> getStream)
		{
			throw new NotSupportedException();
		}

		public override INetworkTask<TMessage> OnReport(Action<ProgressInfo<TMessage>> onReport)
		{
			throw new NotSupportedException();
		}

		protected override TMessage Message { get; }
	}
}
