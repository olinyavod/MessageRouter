using System;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using Melomans.Core.Models;
using Melomans.Core.Message;
using Sockets.Plugin.Abstractions;

namespace Melomans.Core.Network
{
	public class UdpMulticastSenderTask<TMessage> : NetworkTaskBase<TMessage>
		where TMessage:class, IMessage
	{
		private readonly TMessage _message;
		private readonly IMessageSerializer _serializer;
		private readonly IMessageService _messageService;
		private readonly IUdpSocketMulticastClient _client;

		public UdpMulticastSenderTask(TMessage message, 
			IMessageSerializer serializer,
			IMessageService messageService,
			IUdpSocketMulticastClient client)
		{
			_message = message;
			_serializer = serializer;
			_messageService = messageService;
			_client = client;
		}

		public override Meloman For { get { throw new NotSupportedException(); } }


		protected async override void Run(CancellationToken cancellationToken)
		{
			MemoryStream stream = null;
			try
			{
				var definition = _messageService.GetDefinition<TMessage>();
				stream = new MemoryStream(BitConverter.GetBytes(_messageService.CreateMessageHash(definition)));
				await _serializer.WriteMessage(Message, stream);
				await _client.SendMulticastAsync(stream.ToArray());
				RaiseSuccess(Message);
			}
			finally
			{
				if (stream != null)
					stream.Dispose();
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

		protected override TMessage Message
		{
			get { return _message; }
		}
	}
}
