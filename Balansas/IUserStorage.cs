using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmiralCapital.Balansas
{
    public interface IUserStorage
    {
        bool StoreUserDetails(DUser user);
        bool CheckUserExists(string username, ulong guildID);
        DUser GetUser(string username, ulong guildID);
        bool UpdateBalance(string username, ulong guildID, int amount);
    }
}
