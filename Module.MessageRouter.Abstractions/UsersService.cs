using System.Collections.Generic;
using System.Linq;
using Module.MessageRouter.Abstractions.Network;

namespace Module.MessageRouter.Abstractions
{
	public class UsersService<TUser> :IUsersService where TUser:IUser
	{
		private readonly Dictionary<string, TUser> _users = new Dictionary<string, TUser>();

		public IUser Get(string id)
		{
			return _users.ContainsKey(id) ? _users[id] :  default(TUser);
		}

		public bool Remove(string id)
		{
			return _users.Remove(id);
		}

		public bool Add(TUser user)
		{
			if (!_users.ContainsKey(user.Id))
			{
				_users.Add(user.Id, user);
				return true;
			}
			return false;
		}

		public IEnumerable<TUser> GetList()
		{
			return _users.Values.ToList();
		}

		public IEnumerable<string> GetUserIds()
		{
			return _users.Values.Select(user => user.Id).ToList();
		}

		public TUser Get(RemotePoint remotePoint)
		{
			return _users.Values.FirstOrDefault(m => m.IpAddress == remotePoint.Address);
		}
	}
}
