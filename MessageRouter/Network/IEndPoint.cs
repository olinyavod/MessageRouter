namespace MessageRouter.Network
{
    public class RemotePoint
    {
        public RemotePoint(int port, string address)
        {
            Port = port;
            Address = address;
        }

       public int Port { get; private set; }

       public string Address { get; private set; }
    }
}
