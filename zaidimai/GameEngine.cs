using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmiralCapital.zaidimai
{
    public class GameEngine
    {
        private readonly Dictionary<string, IGame> _games;

        public GameEngine()
        {
            _games = new Dictionary<string, IGame>
        {
            { "dice", new DiceGame() },
            { "roulette", new RouletteGame() },
            { "blackjack", new BlackjackGame() },
            { "crash", new CrashGame() },
            { "guess", new GuessGame() }
        };
        }

        public async Task PlayGame(string gameName, DiscordUser user, DiscordChannel channel, ulong guildID)
        {
            if (_games.ContainsKey(gameName))
            {
                await _games[gameName].Play(user, channel, guildID);
            }
            else
            {
             
            }
        }
    }

}
