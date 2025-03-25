using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmiralCapital.zaidimai
{
    public interface IGame
    {
        Task Play(DiscordUser user, DiscordChannel channel, ulong guildID);
        Task<int> GetBet(DiscordUser user, DiscordChannel channel, ulong guildID);
    }

}
