using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using Melomans.Core.Models;
using Sockets.Plugin;
using Sockets.Plugin.Abstractions;

namespace Melomans.Core.Network
{
	public class UdpMelomanScanner : IMelomansScanner
	{
		readonly static Guid ScanMessage = Guid.Parse("56B87012-CD02-4F57-9FC9-7907B75D4D47");

		private static readonly Guid UserInfoMessage = Guid.Parse("A551F1DB-F5C6-4C3E-97E7-89E333EFED20");

		private readonly INetworkSettngs _settngs;
		private readonly ISession _session;
		private UdpSocketMulticastClient _client;
		private readonly DataContractJsonSerializer _serializer;
		private readonly IList<Token> _tokens;

		class Token : IDisposable
		{
			private Action<Meloman> _send;
			private readonly IList<Token> _tokens;

			public Token(Action<Meloman> send, IList<Token> tokens)
			{
				_send = send;
				_tokens = tokens;
			}

			public void Send(Meloman meloman)
			{
				Send(meloman);
			}

			public void Dispose()
			{
				_send = null;
				_tokens.Remove(this);
			}
		}

		public UdpMelomanScanner(INetworkSettngs settngs, ISession session)
		{
			_settngs = settngs;
			_session = session;
			_serializer = new DataContractJsonSerializer(typeof(Meloman));
			_tokens = new List<Token>();
		
		}

		public void Dispose()
		{
			if (_client != null)
			{
				_client.DisconnectAsync();
				_client.Dispose();
			}
		}



		public IDisposable FindMeloman(Action<Meloman> meloman)
		{
			var token = new Token(meloman, _tokens);
			_tokens.Add(token);
			return token;
		}

		public async void Scan()
		{
			if (_client == null)
			{
				_client = new UdpSocketMulticastClient();
				_client.TTL = 10;
				_client.MessageReceived += MessageReceived;
				await _client.JoinMulticastGroupAsync(_settngs.MulticastAddress, _settngs.MulticastPort);
			}
			await _client.SendMulticastAsync(ScanMessage.ToByteArray());
		}

		private void MessageReceived(object sender, UdpSocketMessageReceivedEventArgs e)
		{
			var memoryStream = new MemoryStream(e.ByteData);
			var buffer = new byte[16];
			memoryStream.Read(buffer, 0, 16);
			var guid = new Guid(buffer);
			if (UserInfoMessage == guid)
			{
				var meloman = ((Meloman) _serializer.ReadObject(memoryStream));
				foreach (var token in _tokens)
				{
					token.Send(meloman);
				}
			}else if (guid == ScanMessage)
			{

			}
		}
	}
}
