using System.Collections.Generic;
using MessageRouter.Simple.Model;

namespace MessageRouter.Simple.Service
{
    public class UsersService
    {
        readonly Dictionary<string, User> _users = new Dictionary<string, User>();

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
    }
}
