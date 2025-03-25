using AdmiralCapital.Balansas;
using DSharpPlus.Entities;
using DSharpPlus;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdmiralCapital.Interfeisas
{
    internal class Shop
    {
        public static async Task Parduotuve(DiscordUser user, DiscordChannel channel, ulong guildID)
        {
            var path = @"C:\Users\Nedas\Desktop\AdmiralCapital\bin\Debug\UserInfo.json";
            var json = File.ReadAllText(path);
            var jsonObj = JObject.Parse(json);
            var members = jsonObj["members"].ToObject<List<DUser>>();
            var InterfaceEngine = new InterfaceEngine();
            
            var userDetails = members.FirstOrDefault(u => u.UserName == user.Username && u.GuildID == guildID);

            if (userDetails == null)
            {
                var failMessage = new DiscordEmbedBuilder()
                {
                    Title = "Profilis nerastas",
                    Description = "Jūsų profilis nerastas. Pabandykite vėl.",
                    Color = DiscordColor.Red
                };
                await channel.SendMessageAsync(embed: failMessage);
                await InterfaceEngine.Pradzia(user, channel, guildID);
                return;
            }

            int prekesKaina = 10000000;

            if (userDetails.Booster == 2)
            {
                var failMessage = new DiscordEmbedBuilder()
                {
                    Title = "Jau turi šį boosterį!",
                    Description = "Jūs jau turite šį boosterį. Jūs negalite jo įsigyti dar kartą.",
                    Color = DiscordColor.Red
                };
                await channel.SendMessageAsync(embed: failMessage);
                await InterfaceEngine.Pradzia(user, channel, guildID);
                return;
            }

            if (userDetails.Balance >= prekesKaina)
            {
                userDetails.Balance -= prekesKaina;
                userDetails.Booster = 2;

                var userToUpdate = members.FirstOrDefault(u => u.UserName == user.Username && u.GuildID == guildID);
                if (userToUpdate != null)
                {
                    userToUpdate.Balance = userDetails.Balance;
                    userToUpdate.Booster = userDetails.Booster;
                }

                jsonObj["members"] = JToken.FromObject(members);
                File.WriteAllText(path, jsonObj.ToString());

                var successMessage = new DiscordEmbedBuilder()
                {
                    Title = "Sėkmingai nusipirkote boosterį!",
                    Description = $"Jūs sėkmingai įsigijote boosterį! Jūsų balansas: {userDetails.Balance}.",
                    Color = DiscordColor.Green
                };
                await channel.SendMessageAsync(embed: successMessage);
                await InterfaceEngine.Pradzia(user, channel, guildID);
            }
            else
            {
                var failMessage = new DiscordEmbedBuilder()
                {
                    Title = "Nepakanka monetų!",
                    Description = $"Šiai prekei reikia {prekesKaina} monetų, o tu turi tik {userDetails.Balance}.",
                    Color = DiscordColor.Red
                };

                await channel.SendMessageAsync(embed: failMessage);
                await InterfaceEngine.Pradzia(user, channel, guildID);
            }
        }
        public static async Task Parduotuve1(DiscordUser user, DiscordChannel channel, ulong guildID)
        {
            var button17 = new DiscordButtonComponent(ButtonStyle.Primary, "button17", "Booster 1 (10,000,000 pinigų)");
            var button2 = new DiscordButtonComponent(ButtonStyle.Primary, "preke2", "Prekė");
            var button3 = new DiscordButtonComponent(ButtonStyle.Primary, "preke3", "Prekė");
            var button12 = new DiscordButtonComponent(ButtonStyle.Secondary, "button12", "Pradžia");
            var button13 = new DiscordButtonComponent(ButtonStyle.Danger, "button13", "Baigti");

            var message = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithColor(DiscordColor.Aquamarine)
                .WithTitle("Pasirinkite prekę, kurią norite įsigyti")
                .WithDescription("Paspauskite ant pasirinktos prekės mygtuko, kad ją nusipirktumėte.")
                )
                .AddComponents(button17, button2, button3, button12, button13);

            await channel.SendMessageAsync(message);
        }


    }
}
    

