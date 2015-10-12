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
		private readonly INetworkSettngs _settngs;
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

		public UdpMelomanScanner(INetworkSettngs settngs)
		{
			_settngs = settngs;
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
		}

		private void MessageReceived(object sender, UdpSocketMessageReceivedEventArgs e)
		{
			var meloman = ((Meloman) _serializer.ReadObject(new MemoryStream(e.ByteData)));
			foreach (var token in _tokens)
			{
				token.Send(meloman);
			}
		}
	}
}
