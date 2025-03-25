using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmiralCapital.Balansas
{
    public class DUser
    {
        private string userName;
        private ulong guildID;
        private int balance;
        private int xp;
        private int booster;
        private string avatarUrl;

        public string UserName
        {
            get { return userName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    userName = value;
                else
                    throw new ArgumentException("UserName cannot be empty.");
            }
        }

        public ulong GuildID
        {
            get { return guildID; }
            set { guildID = value; } 
        }

        public int Balance
        {
            get { return balance; }
            set
            {
                if (value >= 0)
                    balance = value;
                else
                    throw new ArgumentException("Balance cannot be negative.");
            }
        }

        public int XP
        {
            get { return xp; }
            set { xp = value; } 
        }

        public int Booster
        {
            get { return booster; }
            set {  booster = value; }
        }

        public string AvatarUrl
        {
            get { return avatarUrl; }
            set { avatarUrl = value; }
        }

        public DUser()
        {
            booster = 1;
        }
    }
}

