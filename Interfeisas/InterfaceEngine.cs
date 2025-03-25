using AdmiralCapital.Balansas;
using DSharpPlus.CommandsNext;
using DSharpPlus;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using DSharpPlus.Interactivity.Extensions;
using Newtonsoft.Json.Linq;
using System.Xml.XPath;
using System.Data.SqlClient;
using System.Threading.Channels;
using AdmiralCapital.commands;
using System.ComponentModel.Composition.Primitives;

namespace AdmiralCapital.Interfeisas
{
    internal class InterfaceEngine
    {
        public static async Task callProfile(DiscordUser user, DiscordChannel channel, ulong guildID)
{
          
    string username = user.Username;

    var path = @"C:\Users\Nedas\Desktop\AdmiralCapital\bin\Debug\UserInfo.json";
    var json = File.ReadAllText(path);
    var jsonObj = JObject.Parse(json);
    var members = jsonObj["members"].ToObject<List<DUser>>();

    var userDetails = members.FirstOrDefault(u => u.UserName == user.Username && u.GuildID == guildID);

    if (userDetails == null)
    {
        userDetails = new DUser()
        {
            UserName = user.Username,
            GuildID = guildID,
            Balance = 1,
            Booster = 1, 
            AvatarUrl = user.AvatarUrl
        };

        members.Add(userDetails);

        jsonObj["members"] = JToken.FromObject(members);
        File.WriteAllText(path, jsonObj.ToString());

        var succesMessage = new DiscordEmbedBuilder()
        {
            Title = "Sekmingai sukurtas profilis",
            Description = "Kraunamas profilis",
            Color = DiscordColor.IndianRed
        };
        await channel.SendMessageAsync(embed: succesMessage);
    }

    var pullUser = members.FirstOrDefault(u => u.UserName == user.Username && u.GuildID == guildID);
            var experienceService = new ExperienceService();
            ExperienceLevel experienceLevel = experienceService.GetXp(username);

            if (pullUser == null)
    {
        var failedMessage = new DiscordEmbedBuilder()
        {
            Title = "Kazkas nepavyko kuriant profili",
            Color = DiscordColor.Sienna
        };
        await channel.SendMessageAsync(embed: failedMessage);
        return;
    }


            var profile = new DiscordMessageBuilder()
        .AddEmbed(new DiscordEmbedBuilder()
        .WithColor(DiscordColor.Blue)
        .WithTitle(pullUser.UserName + " profilis")
        .AddField("Balansas", pullUser.Balance.ToString())
        .AddField("Patirties lygis: ", experienceLevel.GetLevel())
        .AddField("XP boosteris: ", pullUser.Booster.ToString())
        .WithThumbnail(pullUser.AvatarUrl)
    );

    var button12 = new DiscordButtonComponent(ButtonStyle.Primary, "button12", "Atgal");
    var button13 = new DiscordButtonComponent(ButtonStyle.Danger, "button13", "Baigti");
    var message = new DiscordMessageBuilder();
    profile.AddComponents(button12, button13);

    await channel.SendMessageAsync(profile);
}

        public static async Task Games(DiscordUser user, DiscordChannel channel, ulong guildID)
        {
            var button3 = new DiscordButtonComponent(ButtonStyle.Danger, "button3", "🎴Blackjack🃏");
            var button4 = new DiscordButtonComponent(ButtonStyle.Primary, "button4", "🔴Rulete⚫");
            var button5 = new DiscordButtonComponent(ButtonStyle.Success, "button5", "🎲Kauliukai🎯");
            var button6 = new DiscordButtonComponent(ButtonStyle.Secondary, "button6", "🔢Atspėk skaičių❓");
            var button7 = new DiscordButtonComponent(ButtonStyle.Success, "button7", "📈Crash💥");
            var button14 = new DiscordButtonComponent(ButtonStyle.Primary, "button14", "Kitas");

            var message = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithColor(DiscordColor.Aquamarine)
                .WithTitle("Pasirink žaidimą!"))
                .AddComponents(button3, button4, button5, button6, button14);


            await channel.SendMessageAsync(message);


        }
        

       
        
       
        public static async Task Games2(DiscordUser user, DiscordChannel channel, ulong guildID)
        {
            var button7 = new DiscordButtonComponent(ButtonStyle.Success, "button7", "📈Crash💥");
            var button14 = new DiscordButtonComponent(ButtonStyle.Primary, "button15", "Atgal");
            var button12 = new DiscordButtonComponent(ButtonStyle.Primary, "button12", "Pradžia");
            var button13 = new DiscordButtonComponent(ButtonStyle.Primary, "button13", "Baigti");

            var message = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithColor(DiscordColor.Aquamarine)
                .WithTitle("Pasirink žaidimą!"))
                .AddComponents(button7, button14, button12, button13);


            await channel.SendMessageAsync(message);


        }
        public static async Task Pradzia(DiscordUser user, DiscordChannel channel, ulong guildID)
        {
            var button = new DiscordButtonComponent(ButtonStyle.Danger, "button", "👤Profilis📊");
            var button2 = new DiscordButtonComponent(ButtonStyle.Primary, "button2", "🎮Žaidimai🕹️");
            var button16 = new DiscordButtonComponent(ButtonStyle.Success, "button16", "🛍️Parduotuvė💰");

            var message = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithColor(DiscordColor.Aquamarine)
                .WithTitle("Sveiki, aš esu Admiral Capital! Pasirink norimą veiksmą spustelėjęs mygtuką žemiau."))
                .AddComponents(button, button2, button16);


            await channel.SendMessageAsync(message);


        }

        


    }
}
