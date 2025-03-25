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
    public class BlackjackGame : IGame
    {
        private readonly Random _random = new Random();
        private readonly InterfaceEngine _interfaceEngine = new InterfaceEngine();
        private readonly Pranesimai _pranesimai = new Pranesimai();

        public async Task Play(DiscordUser  user, DiscordChannel channel, ulong guildID)
        {
            int bet = await GetBet(user, channel, guildID);
            if (bet <= 0)
            {
                await Pranesimai.PranesimaiBJ(user, channel, guildID, 0, 0, bet, 6);
                return;
            }

            Random random = new Random();

            int DrawCard() => random.Next(1, 12);

            var InterfaceEngine = new InterfaceEngine();
            var Pralaimejimas = new Pralaimejimas();
            var Laimejimas = new Laimejimas();
            


            if (bet <= 0)
            {
                await Pranesimai.PranesimaiBJ(user, channel, guildID, 0, 0, bet, 6);
                return;
            }

            int userScore = DrawCard() + DrawCard();
            int computerScore = DrawCard();

            await channel.SendMessageAsync($"🎲 Tavo pradiniai taškai: {userScore}. Kompiuterio pradinis taškų skaičius po pirmos kortos: {computerScore}.");

            bool userStand = false;
            while (userScore < 21 && !userStand)
            {

                var messageBuilder = new DiscordMessageBuilder()
                    .WithContent($"Tavo taškai: {userScore}. Pasirink veiksmą:\n⚡ Hit (traukti kortą)\n🛑 Stand (sustabdyti)")
                    .AddComponents(
                        new DiscordButtonComponent(ButtonStyle.Success, "hit", "⚡ Hit"),
                        new DiscordButtonComponent(ButtonStyle.Danger, "stand", "🛑 Stand")
                    );

                var message = await channel.SendMessageAsync(messageBuilder);

                var interactionResult = await message.WaitForButtonAsync(user, TimeSpan.FromSeconds(30));
                if (interactionResult.TimedOut)
                {
                    await Pranesimai.PranesimaiBJ(user, channel, guildID, userScore, computerScore, bet, 5);
                    return;
                }

                if (interactionResult.Result.Id == "hit")
                {
                    int newCard = DrawCard();
                    userScore += newCard;
                    await channel.SendMessageAsync($"🎴 Gavai kortą su {newCard} taškais. Tavo dabartiniai taškai: {userScore}.");
                }
                else if (interactionResult.Result.Id == "stand")
                {
                    userStand = true;
                    await channel.SendMessageAsync("🛑 Pasirinkai sustoti.");
                }
            }

            while (computerScore < 17)
            {
                computerScore += DrawCard();
            }

            if (userScore > 21)
            {
                await Pranesimai.PranesimaiBJ(user, channel, guildID, userScore, computerScore, bet, 1);
                Pralaimejimas.Lose(user.Username, guildID, bet, 1);
            }
            else if (computerScore > 21 || userScore > computerScore)
            {
                await Pranesimai.PranesimaiBJ(user, channel, guildID, userScore, computerScore, bet, 2);
                Laimejimas.Win(user.Username, guildID, bet, 2);
            }
            else if (userScore == computerScore)
            {
                await Pranesimai.PranesimaiBJ(user, channel, guildID, userScore, computerScore, bet, 3);
            }
            else
            {
                await Pranesimai.PranesimaiBJ(user, channel, guildID, userScore, computerScore, bet, 4);
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
