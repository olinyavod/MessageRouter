using System;
using System.IO;
using System.Threading.Tasks;

namespace Module.MessageRouter.Abstractions.Network
{
    public interface IRemoteClient : IDisposable
    {
        Stream ReadStream { get; }

        Stream WriteStream { get; }

        RemotePoint RemotePoint { get; }

        Task DisconnectAsync();
    }
}
