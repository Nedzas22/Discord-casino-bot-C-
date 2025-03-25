using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmiralCapital.Interfeisas
{
    internal class Pranesimai
    {
        public static async Task PranesimaiCrash(DiscordUser user, DiscordChannel channel, ulong guildID, int rezultatas, int statymas, int daugiklis, string rez)
        {
            if (rezultatas == 0)
            {
                var pralaimejimas = new DiscordMessageBuilder()
                                         .AddEmbed(new DiscordEmbedBuilder()
                                         .WithColor(DiscordColor.Red)
                                         .WithTitle($"Pralaimejai crashas ivyko ties " + rez + "X"));

                await channel.SendMessageAsync(pralaimejimas);
            }
            else
            {
                var laimejimas = new DiscordMessageBuilder()
                  .AddEmbed(new DiscordEmbedBuilder()
                  .WithColor(DiscordColor.Green)
                  .WithTitle($"Laimejai {statymas * daugiklis:F2} pinigų! crash įvyko " + rez + "X"));
                await channel.SendMessageAsync(laimejimas);
            }
        }
        public static async Task PranesimaiGuess(DiscordUser user, DiscordChannel channel, ulong guildID, int rezultatas, int statymas, int daugiklis, int rez)
        {
            if (rezultatas == 0)
            {
                var pralaimejimas = new DiscordMessageBuilder()
                                         .AddEmbed(new DiscordEmbedBuilder()
                                         .WithColor(DiscordColor.Red)
                                         .WithTitle($"Pralaimejai, skaičius buvo" + rez));

                await channel.SendMessageAsync(pralaimejimas);
            }
            else
            {
                var laimejimas = new DiscordMessageBuilder()
                  .AddEmbed(new DiscordEmbedBuilder()
                  .WithColor(DiscordColor.Green)
                  .WithTitle($"Laimejai {statymas * daugiklis:F2} pinigų! Tavo skaičius buvo teisingas."));
                await channel.SendMessageAsync(laimejimas);
            }
        }
        public static async Task PranesimaiBJ(DiscordUser user, DiscordChannel channel, ulong guildID, int userScore, int computerScore, int bet, int rez)
        {
            if (rez == 1)
            {
                await channel.SendMessageAsync("❌ Jūs pralaimėjote nes viršijote 21 tašką!");
            }
            else if (rez == 2)
            {
                await channel.SendMessageAsync($"🎉 Laimėjai {bet * 2} pinigų! Tavo taškai: {userScore}, Kompiuterio taškai: {computerScore}.");
            }
            else if (rez == 3)
            {
                await channel.SendMessageAsync($"🤝 Lygiosios, {bet} pinigų buvo grąžinti! Tavo taškai: {userScore}, Kompiuterio taškai: {computerScore}.");
            }
            else if (rez == 5)
            {
                await channel.SendMessageAsync("⏱️ Nepasirinkai jokio veiksmo, žaidimas baigtas.");
            }
            else if (rez == 4)
            {
                await channel.SendMessageAsync($"❌ Pralaimėjai {bet} pinigų. Tavo taškai: {userScore}, Kompiuterio taškai: {computerScore}.");
            }
            else if (rez == 6)
            {
                await channel.SendMessageAsync("⚠ Statymas turi būti skaičius didesnis nei 0. Žaidimas atšauktas.");
            }
        }
        public static async Task PranesimaiRulete(DiscordUser user, DiscordChannel channel, ulong guildID, int rez, int resultNumber, string resultColor, int bet)
        {

            if (rez == 1)
            {
                await channel.SendMessageAsync("⏱️ Nepasirinkai spalvos, žaidimas baigtas.");
            }
            else if (rez == 2)
            {
                await channel.SendMessageAsync("⚠ Statymas turi būti skaičius tarp 1 ir 36. Žaidimas atšauktas.");
            }
            else if (rez == 3)
            {
                await channel.SendMessageAsync("🔢 Įvesk skaičių, ties kuriuo dedate savo statymą (1–36):");
            }
            else if (rez == 4)
            {
                await channel.SendMessageAsync("⏱️ Nepasirinkai statymo tipo, žaidimas baigtas.");
            }
            else if (rez == 5)
            {
                await channel.SendMessageAsync("⚠ Statymas turi būti skaičius didesnis nei 0. Žaidimas atšauktas.");
            }
            else if (rez == 6)
            {
                await channel.SendMessageAsync("⏱️ Nepasirinkai skaičiaus tipo, žaidimas baigtas.");
            }
            else if (rez == 7)
            {
                await channel.SendMessageAsync($"🎲 Ruletė sustojo ties skaičiumi {resultNumber} ({resultColor}).\n");
            }
            else if (rez == 8)
            {
                await channel.SendMessageAsync($"🎉 Sveikiname, laimėjai {bet} pinigų!");
            }
            else if (rez == 9)
            {
                await channel.SendMessageAsync($"❌ Deja, pralaimėjai.");
            }
        }
        public static async Task PranesimaiDices(DiscordUser user, DiscordChannel channel, ulong guildID, int rez, int bet, int rolledNumber)
        {
            if (rez == 1)
            {
                await channel.SendMessageAsync("⚠ Statymas turi būti skaičius didesnis nei 0. Žaidimas atšauktas.");
            }
            if (rez == 2)
            {
                await channel.SendMessageAsync("⏱️ Nepasirinkai skaičiaus, žaidimas baigtas.");
            }
            if (rez == 3)
            {
                await channel.SendMessageAsync($"🎉 Sveikiname, laimėjai {bet} pinigų! 🎲 Kauliukas parodė {rolledNumber}.");
            }
            if (rez == 4)
            {
                await channel.SendMessageAsync($"❌ Deja, pralaimėjai. 🎲 Kauliukas parodė {rolledNumber}.");
            }

        }
    }
}
