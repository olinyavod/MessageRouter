using System.IO;
using System.Threading.Tasks;

namespace Module.MessageRouter.Abstractions.Network
{
	class MulticastRemoteClient : IRemoteClient
	{
		private Stream _stream;

		public MulticastRemoteClient(RemotePoint point, Stream stream)
		{
			_stream = stream;
			WriteStream = Stream.Null;
            RemotePoint = point;
		}

		public Stream ReadStream => _stream;

	    public Stream WriteStream { get; private set; }
	    public RemotePoint RemotePoint { get; private set; }

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
