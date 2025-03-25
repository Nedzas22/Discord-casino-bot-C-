using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmiralCapital.Balansas
{
    public class BalansoEngine
    {
        private readonly IUserStorage _userStorage;

        public BalansoEngine(IUserStorage userStorage)
        {
            _userStorage = userStorage;
        }

        public bool StoreUserDetails(DUser user)
        {
            return _userStorage.StoreUserDetails(user);
        }

        public bool CheckUserExists(string username, ulong guildID)
        {
            return _userStorage.CheckUserExists(username, guildID);
        }

        public DUser GetUser(string username, ulong guildID)
        {
            return _userStorage.GetUser(username, guildID);
        }

        public bool Nobalance(string username, ulong guildID)
        {
            return _userStorage.UpdateBalance(username, guildID, 500);
        }
    }
}
