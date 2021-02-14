using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rent.DomainModels.Models.HubModels
{
    public class UserInfoInMemory
    {
        private ConcurrentDictionary<string, UserInfo> _onlineUsers { get; set; } = new ConcurrentDictionary<string, UserInfo>();
        public bool AddUpdate(string name, string connectionId)
        {
            var userAlreadyExists = _onlineUsers.ContainsKey(name);

            var userInfo = new UserInfo
            {
                UserName = name,
                ConnectionId = connectionId
            };

            _onlineUsers.AddOrUpdate(name, userInfo, (key, value) => userInfo);

            return userAlreadyExists;
        }

        public void Remove(string name)
        {
            UserInfo userInfo;
            _onlineUsers.TryRemove(name, out userInfo);
        }

        public IEnumerable<UserInfo> GetAllUsersExceptThis(string username)
        {
            return _onlineUsers.Values.Where(item => item.UserName != username);
        }

        public UserInfo GetUserInfo(string username)
        {
            UserInfo user;
            _onlineUsers.TryGetValue(username, out user);
            return user;
        }
    }
}
