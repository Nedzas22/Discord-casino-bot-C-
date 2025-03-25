using AdmiralCapital.Interfeisas;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmiralCapital.zaidimai
{
    public class GuessGame : IGame
    {
        private readonly Random _random = new Random();
        private readonly InterfaceEngine _interfaceEngine = new InterfaceEngine();
        private readonly Pranesimai _pranesimai = new Pranesimai();

        public async Task Play(DiscordUser user, DiscordChannel channel, ulong guildID)
        {
            int bet = await GetBet(user, channel, guildID);
            if (bet <= 0)
            {
                await Pranesimai.PranesimaiGuess(user, channel, guildID, 0, bet, 0, 0);
                return;
            }

            {
                Random random = new Random();
                int randomNumber = random.Next(1, 6);
                var InterfaceEngine = new InterfaceEngine();
                var Statymas = new Statymas();
                var Pralaimejimas = new Pralaimejimas();
                var Laimejimas = new Laimejimas();
                var interactivity = Program.Client.GetInteractivity();
                int statymas = await Statymas.Bet(user, channel, guildID);
                TimeSpan.FromSeconds(50);
                if (statymas != -1)
                {
                    var message5 = new DiscordMessageBuilder()
                   .AddEmbed(new DiscordEmbedBuilder()
                   .WithColor(DiscordColor.Aquamarine)
                   .WithTitle("Pasirink skaičių nuo 1 iki 5! "));
                    await channel.SendMessageAsync(message5);
                    var messageResult = await interactivity.WaitForMessageAsync(
                   message => message.Author.Id == user.Id && int.TryParse(message.Content, out _),

                    TimeSpan.FromSeconds(30)
                    );
                    if (int.TryParse(messageResult.Result.Content, out int spejimas))
                    {
                        if (spejimas == randomNumber)
                        {
                            await Pranesimai.PranesimaiGuess(user, channel, guildID, 1, statymas, 2, randomNumber);
                            var win = Laimejimas.Win(user.Username, guildID, statymas, 20);
                        }
                        else
                        {
                            await Pranesimai.PranesimaiGuess(user, channel, guildID, 0, statymas, 0, randomNumber);
                            var lose = Pralaimejimas.Lose(user.Username, guildID, statymas, 1);
                        }
                        await InterfaceEngine.Games(user, channel, guildID);
                    }

                }
            }
        }

        public async Task<int> GetBet(DiscordUser user, DiscordChannel channel, ulong guildID)
        {
            var Statymas = new Statymas();
            return await Statymas.Bet(user, channel, guildID);
        }
    }

}
