namespace Module.MessageRouter.Abstractions
{
    public class User
    {
        public string IpAddress { get; set; }
        public string Id { get; set; }
        public int Port { get; set; }

        public User(string ipAddress, string id, int port)
        {
            IpAddress = ipAddress;
            Id = id;
            Port = port;
        }
    }
}