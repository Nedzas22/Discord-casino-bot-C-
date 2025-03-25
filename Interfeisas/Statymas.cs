using AdmiralCapital.Balansas;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmiralCapital.Interfeisas
{
    internal class Statymas
    {
        public async Task<int> Bet(DiscordUser userr, DiscordChannel channel, ulong guildID)
        {

            var username = userr.Username;
            var interactivity = Program.Client.GetInteractivity();
            await channel.SendMessageAsync("Parasyk suma kuria nori statyti(skaičiais):");
            var messageResult = await interactivity.WaitForMessageAsync(
                message => message.Author.Id == userr.Id && int.TryParse(message.Content, out _),

                TimeSpan.FromSeconds(30)

            );
            if (messageResult.TimedOut)
            {
                await channel.SendMessageAsync("Nespejai laiku.");
                return -1;
            }
            if (int.TryParse(messageResult.Result.Content, out int bet))
            {
                var path = @"C:\Users\Nedas\Desktop\AdmiralCapital\bin\Debug\UserInfo.json";
                var json = File.ReadAllText(path);
                var jsonObj = JObject.Parse(json);
                var members = jsonObj["members"].ToObject<List<DUser>>();
                foreach (var user in members)
                {
                    if (user.UserName == username && user.GuildID == guildID)
                    {
                        if (user.Balance >= bet)
                        {
                            await channel.SendMessageAsync($"Tavo statymas {bet} išsaugotas.");
                            return bet;
                        }
                        else
                        {
                            await channel.SendMessageAsync("Tu neturi tiek savo balanse tasku");
                        }
                    }
                    
                }
                return -1;
            }
            else
            {
                await channel.SendMessageAsync("Kažkas nepavyko bandyk iš naujo.");
                return -1;
            }


        }
    }
}
