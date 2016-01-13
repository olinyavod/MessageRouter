namespace Module.MessageRouter.Abstractions.Network.Interfaces
{
    public class RemotePoint
    {
        public RemotePoint(int port, string address)
        {
            Port = port;
            Address = address;
        }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        // ReSharper disable once MemberCanBePrivate.Global
       public int Port { get; private set; }

       public string Address { get; private set; }
    }
}
