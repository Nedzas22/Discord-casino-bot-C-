using AdmiralCapital.Interfeisas;
using DSharpPlus.Entities;
using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Interactivity.Extensions;

namespace AdmiralCapital.zaidimai
{
    public class DiceGame : IGame
    {
        private readonly Random _random = new Random();
        private readonly InterfaceEngine _interfaceEngine = new InterfaceEngine();
        private readonly Pranesimai _pranesimai = new Pranesimai();

        public async Task Play(DiscordUser user, DiscordChannel channel, ulong guildID)
        {
            int bet = await GetBet(user, channel, guildID);
            if (bet <= 0)
            {
                await Pranesimai.PranesimaiDices(user, channel, guildID, 1, bet, 0);
                return;
            }

            var diceMessage = new DiscordMessageBuilder()
                .WithContent("🎲 Pasirink skaičių nuo 1 iki 6:")
                .AddComponents(
                    new DiscordButtonComponent(ButtonStyle.Primary, "1", "1"),
                    new DiscordButtonComponent(ButtonStyle.Primary, "2", "2"),
                    new DiscordButtonComponent(ButtonStyle.Primary, "3", "3"),
                    new DiscordButtonComponent(ButtonStyle.Primary, "4", "4"),
                    new DiscordButtonComponent(ButtonStyle.Primary, "5", "5"),
                    new DiscordButtonComponent(ButtonStyle.Primary, "6", "6")
                );

            var diceResultMessage = await channel.SendMessageAsync(diceMessage);
            var diceResponse = await diceResultMessage.WaitForButtonAsync(user, TimeSpan.FromSeconds(30));

            if (diceResponse.TimedOut)
            {
                await Pranesimai.PranesimaiDices(user, channel, guildID, 2, bet, 0);
                return;
            }

            int chosenNumber = int.Parse(diceResponse.Result.Id);
            int rolledNumber = _random.Next(1, 7);
            bool isWin = chosenNumber == rolledNumber;
            var Pralaimejimas = new Pralaimejimas();
            var Laimejimas = new Laimejimas();

            if (isWin)
            {
                bet *= 6;
                await Pranesimai.PranesimaiDices(user, channel, guildID, 3, bet, rolledNumber);
                Laimejimas.Win(user.Username, guildID, bet, 1);
            }
            else
            {
                await Pranesimai.PranesimaiDices(user, channel, guildID, 4, bet, rolledNumber);
                Pralaimejimas.Lose(user.Username, guildID, bet, 1);
            }

            await InterfaceEngine.Games(user, channel, guildID);
        }

        public async Task<int> GetBet(DiscordUser user, DiscordChannel channel, ulong guildID)
        {
            var Statymas = new Statymas();
            return await Statymas.Bet(user, channel, guildID);
        }
    }

}
