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
    public class CrashGame : IGame
    {
        private readonly Random _random = new Random();
        private readonly InterfaceEngine _interfaceEngine = new InterfaceEngine();
        private readonly Pranesimai _pranesimai = new Pranesimai();

        public async Task Play(DiscordUser user, DiscordChannel channel, ulong guildID)
        {
            var Statymas = new Statymas();
            int bet = await GetBet(user, channel, guildID);
            if (bet <= 0)
            {
                await Pranesimai.PranesimaiCrash(user, channel, guildID, 0, bet, 0, "0");
                return;
            }

            {
                Random random = new Random();
                double min = 1;
                double max = 100;
                double randomDouble = min + (random.NextDouble() * (max - min));
                var InterfaceEngine = new InterfaceEngine();
                var Pralaimejimas = new Pralaimejimas();
                var Laimejimas = new Laimejimas();
                var interactivity = Program.Client.GetInteractivity();
                int statymas = await Statymas.Bet(user, channel, guildID);
                TimeSpan.FromSeconds(50);
                if (statymas != -1)
                {
                    var message6 = new DiscordMessageBuilder()
                   .AddEmbed(new DiscordEmbedBuilder()
                   .WithColor(DiscordColor.Aquamarine)
                   .WithTitle("Pasirink skaičių nuo 1 iki 100! "));
                    await channel.SendMessageAsync(message6);
                    var messageResult = await interactivity.WaitForMessageAsync(
                   message => message.Author.Id == user.Id && int.TryParse(message.Content, out _),

                    TimeSpan.FromSeconds(30)
                    );
                    if (int.TryParse(messageResult.Result.Content, out int spejimas))
                    {
                        if (spejimas <= randomDouble)
                        {
                            string doble = randomDouble.ToString("F2");
                            double laimejimoSuma = statymas * spejimas;
                            await Pranesimai.PranesimaiCrash(user, channel, guildID, 1, statymas, spejimas, doble);
                            var win = Laimejimas.Win(user.Username, guildID, statymas, spejimas);
                            await InterfaceEngine.Games(user, channel, guildID);
                        }
                        else
                        {
                            string doble = randomDouble.ToString("F2");
                            await Pranesimai.PranesimaiCrash(user, channel, guildID, 0, statymas, 0, doble);
                            var lose = Pralaimejimas.Lose(user.Username, guildID, statymas, 1);
                            await InterfaceEngine.Games(user, channel, guildID);
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
