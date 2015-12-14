using System.Collections.Generic;
using System.Linq;
using MessageRouter.Network;
using Module.MessageRouter.Abstractions.Network;

namespace Module.MessageRouter.Abstractions
{
    public class UsersService
    {
        private readonly Dictionary<string, User> _users = new Dictionary<string, User>();

        public User Get(string id)
        {
            return _users.ContainsKey(id) ? _users[id] : null;
        }

        public bool Remove(string id)
        {
            return _users.Remove(id);
        }

        public bool Add(User user)
        {
            if (!_users.ContainsKey(user.Id))
            {
                _users.Add(user.Id, user);
                return true;
            }
            return false;
        }

        public IEnumerable<User> GetList()
        {
            return _users.Values.ToList();
        }

        public IEnumerable<string> GetUserIds()
        {
            var list = new List<string>();
            foreach (var user in _users.Values)
                list.Add(user.Id);
            return list;
        }

        public User Get(RemotePoint remotePoint)
        {
            return _users.Values.FirstOrDefault(m => m.IpAddress == remotePoint.Address);
        }
    }
}
