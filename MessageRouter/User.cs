namespace Module.MessageRouter.Abstractions
{
    public class User
    {
        public User(string ipAddress, string id)
        {
            IpAddress = ipAddress;
            Id = id;
        }

        public string IpAddress { get; set; }
        public string Id { get; set; }
    }
}