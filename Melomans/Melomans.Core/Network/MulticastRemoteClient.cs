using System.IO;
using System.Threading.Tasks;

namespace MessageRouter.Network
{
	class MulticastRemoteClient : IRemoteClient
	{
		private Stream _stream;

		public MulticastRemoteClient(Stream stream)
		{
			_stream = stream;
			WriteStream = Stream.Null;
		}

		public Stream ReadStream
		{
			get { return _stream; }
		}

		public Stream WriteStream { get; private set; }

		public Task DisconnectAsync()
		{
			return Task.Delay(0);
		}

		public void Dispose()
		{
			_stream.Dispose();
			_stream = null;
			WriteStream.Dispose();
			WriteStream = null;
		}
	}
}
