using AdmiralCapital.Interfeisas;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmiralCapital.zaidimai
{
    public class RouletteGame : IGame
    {
        private readonly Random _random = new Random();
        private readonly InterfaceEngine _interfaceEngine = new InterfaceEngine();
        private readonly Pranesimai _pranesimai = new Pranesimai();

        public async Task Play(DiscordUser user, DiscordChannel channel, ulong guildID)
        {
            int bet = await GetBet(user, channel, guildID);
            if (bet <= 0)
            {
                await Pranesimai.PranesimaiRulete(user, channel, guildID, 5, 0, "s", bet);
                return;
            }

            Random random = new Random();

            var InterfaceEngine = new InterfaceEngine();

           
            if (bet <= 0)
            {
                await Pranesimai.PranesimaiRulete(user, channel, guildID, 5, 0, "s", bet);
                return;
            }

            var messageBuilder = new DiscordMessageBuilder()
                .WithContent($"🎰 Pasirink savo statymo tipą:\n1️⃣ Konkretus skaičius (1–36)\n🔴 Raudona arba 🔵 Juoda\n⚖️ Lyginis arba Nelyginis")
                .AddComponents(
                    new DiscordButtonComponent(ButtonStyle.Primary, "number", "1️⃣ Skaičius"),
                    new DiscordButtonComponent(ButtonStyle.Success, "color", "🔴🔵 Spalva"),
                    new DiscordButtonComponent(ButtonStyle.Secondary, "evenodd", "⚖️ Lyginis/Nelyginis")
                );

            var betTypeMessage = await channel.SendMessageAsync(messageBuilder);
            var betTypeResult = await betTypeMessage.WaitForButtonAsync(user, TimeSpan.FromSeconds(30));

            if (betTypeResult.TimedOut)
            {
                await Pranesimai.PranesimaiRulete(user, channel, guildID, 4, 0, "s", bet);
                return;
            }

            string betType = betTypeResult.Result.Id;
            int? chosenNumber = null;
            string chosenColor = null;
            bool? isEven = null;

            if (betType == "number")
            {
                await Pranesimai.PranesimaiRulete(user, channel, guildID, 3, 0, "s", bet);
                var numberResponse = await channel.GetNextMessageAsync(user, TimeSpan.FromSeconds(30));
                if (numberResponse.TimedOut || !int.TryParse(numberResponse.Result.Content, out int number) || number < 1 || number > 36)
                {
                    await Pranesimai.PranesimaiRulete(user, channel, guildID, 2, 0, "s", bet);
                    return;
                }
                chosenNumber = number;
            }
            else if (betType == "color")
            {
                var colorMessage = new DiscordMessageBuilder()
                    .WithContent("🔴 Pasirink spalvą:")
                    .AddComponents(
                        new DiscordButtonComponent(ButtonStyle.Danger, "raudona", "🔴 Raudona"),
                        new DiscordButtonComponent(ButtonStyle.Primary, "juoda", "🔵 Juoda")
                    );

                var colorResult = await channel.SendMessageAsync(colorMessage);
                var colorResponse = await colorResult.WaitForButtonAsync(user, TimeSpan.FromSeconds(30));

                if (colorResponse.TimedOut)
                {
                    await Pranesimai.PranesimaiRulete(user, channel, guildID, 1, 0, "s", bet);
                    return;
                }
                chosenColor = colorResponse.Result.Id;
            }
            else if (betType == "evenodd")
            {
                var evenOddMessage = new DiscordMessageBuilder()
                    .WithContent("⚖️ Pasirink skaičiaus tipą:")
                    .AddComponents(
                        new DiscordButtonComponent(ButtonStyle.Success, "lyginiu", "⚖️ Lyginis"),
                        new DiscordButtonComponent(ButtonStyle.Secondary, "nelyginiu", "⚖️ Nelyginis")
                    );

                var evenOddResult = await channel.SendMessageAsync(evenOddMessage);
                var evenOddResponse = await evenOddResult.WaitForButtonAsync(user, TimeSpan.FromSeconds(30));

                if (evenOddResponse.TimedOut)
                {
                    await Pranesimai.PranesimaiRulete(user, channel, guildID, 6, 0, "s", bet);
                    return;
                }
                isEven = evenOddResponse.Result.Id == "lyginiu";
            }

            int resultNumber = random.Next(0, 37);
            string resultColor = resultNumber == 0 ? "žalia" : (resultNumber % 2 == 0 ? "juoda" : "raudona");

            bool isWin = false;
            if (chosenNumber.HasValue && chosenNumber.Value == resultNumber)
            {
                isWin = true;
                bet *= 35;
            }
            else if (!string.IsNullOrEmpty(chosenColor) && chosenColor == resultColor)
            {
                isWin = true;
                bet *= 2;
            }
            else if (isEven.HasValue && isEven.Value == (resultNumber % 2 == 0))
            {
                isWin = true;
                bet *= 2;
            }

            await Pranesimai.PranesimaiRulete(user, channel, guildID, 7, resultNumber, resultColor, bet);
            var Pralaimejimas = new Pralaimejimas();
            var Laimejimas = new Laimejimas();

            if (isWin)
            {
                await Pranesimai.PranesimaiRulete(user, channel, guildID, 8, resultNumber, resultColor, bet);
                Laimejimas.Win(user.Username, guildID, bet, 1);
            }
            else
            {
                await Pranesimai.PranesimaiRulete(user, channel, guildID, 9, resultNumber, resultColor, bet);
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
