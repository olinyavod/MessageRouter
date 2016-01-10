
namespace Module.MessageRouter.Abstractions
{
    public interface IUser
    {
        string IpAddress { get; set; }
        string Id { get; set; }
        int Port { get; set; }
    }
}