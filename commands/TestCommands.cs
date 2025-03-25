using AdmiralCapital.Balansas;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmiralCapital.commands
{
    public class TestCommands : BaseCommandModule
    {
        [Command("pinigai")]
        public async Task MyFirstCommand(CommandContext ctx)
        {
            var interactivity = Program.Client.GetInteractivity();

            var messageToReact = await interactivity.WaitForReactionAsync(message => message.Message.Id == 1298998464352485436);
            if (messageToReact.Result.Message.Id == 1298998464352485436)
            {
                await ctx.Channel.SendMessageAsync($"{ctx.User.Username} panaudojo emoji kuris vadinasi{messageToReact.Result.Emoji.Name}");
            }
        }
        [Command("pridek")]
        public async Task Add(CommandContext ctx, int number1, int number2)
        {
            int result = number1 + number2;
            await ctx.Channel.SendMessageAsync(result.ToString());
        }
        [Command("zaisti")]
        public async Task Poll(CommandContext ctx)
        {
            string option1 = "Blackjack";
            string option2 = "Slots";
            string option3 = "Card Game";
            string option4 = "Guess Game";
            string pollTitle = "Pasirink norima žaidimą";
            var interactivity = Program.Client.GetInteractivity();
            var pollTime = TimeSpan.FromSeconds(10);

            DiscordEmoji[] emojiOptions = {DiscordEmoji.FromName(Program.Client, ":one:"),
                                            DiscordEmoji.FromName(Program.Client, ":two:"),
                                            DiscordEmoji.FromName(Program.Client, ":three:"),
                                            DiscordEmoji.FromName(Program.Client, ":four:") };
            string optionsDescription = $"{emojiOptions[0]} | {option1} \n" +
                                        $"{emojiOptions[1]} | {option2} \n" +
                                        $"{emojiOptions[2]} | {option3} \n" +
                                        $"{emojiOptions[3]} | {option4}";


            var pollMessage = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Red,
                Title = pollTitle,
                Description = optionsDescription,


            };
            var sentPoll = await ctx.Channel.SendMessageAsync(embed: pollMessage);
            foreach (var emoji in emojiOptions)
            {
                await sentPoll.CreateReactionAsync(emoji);
            }
            var totalReactions = await interactivity.CollectReactionsAsync(sentPoll, pollTime);

            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            int count4 = 0;

            foreach (var emoji in totalReactions)
            {
                if (emoji.Emoji == emojiOptions[0])
                {
                    count1++;
                }
                if (emoji.Emoji == emojiOptions[1])
                {
                    count2++;
                }
                if (emoji.Emoji == emojiOptions[2])
                {
                    count3++;
                }
                if (emoji.Emoji == emojiOptions[3])
                {
                    count4++;
                }
            }
            int totalVotes = count1 + count2 + count3 + count4;
            string resultsDescription = $"{emojiOptions[0]}: {count1} Votes \n" +
                                        $"{emojiOptions[1]}: {count2} Votes \n" +
                                        $"{emojiOptions[2]}: {count3} Votes \n" +
                                        $"{emojiOptions[3]}: {count4} Votes";

            var resultEmbed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Green,
                Title = "Results of the poll",
                Description = resultsDescription,
            };
            await ctx.Channel.SendMessageAsync(embed: resultEmbed); 
        }
        [Command("button")]
        public async Task Buttons(CommandContext ctx)
        {
            var button = new DiscordButtonComponent(ButtonStyle.Primary, "button1", "button");
            var button2 = new DiscordButtonComponent(ButtonStyle.Primary, "button2", "button");

            var message = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithColor(DiscordColor.Aquamarine)
                .WithTitle("Test Embed"))
                .AddComponents(button,button2);


            await ctx.Channel.SendMessageAsync(message);

             
        }
        [Command("profilis")]
        public async Task ProfileCommand(CommandContext ctx)
        {
        string username = ctx.User.Username;
            ulong guildID = ctx.Guild.Id;
            var userDetails = new DUser()
            {
                UserName = ctx.User.Username,
                GuildID = ctx.Guild.Id,
                Balance = 1,
                AvatarUrl = ctx.User.AvatarUrl
            };
            IUserStorage userStorage = new UserDataStorage();
            BalansoEngine balansoEngine = new BalansoEngine(userStorage);
            bool doesExist = balansoEngine.CheckUserExists(username, guildID);
            if (doesExist == false)
            {
                bool isStored = balansoEngine.StoreUserDetails(userDetails);
                if (isStored == true)
                {
                    var succesMessage = new DiscordEmbedBuilder()
                    {
                        Title = "Sekmingai sukurtas profilis",
                        Description = "Kraunamas profilis",
                        Color = DiscordColor.IndianRed
                    };
                    await ctx.Channel.SendMessageAsync(embed: succesMessage);

                    var pullUser = balansoEngine.GetUser(username, guildID);

                    var profile = new DiscordMessageBuilder()
                        .AddEmbed(new DiscordEmbedBuilder()
                        .WithColor(DiscordColor.Wheat)
                        .WithTitle(pullUser.UserName + " profilis") 
                        .AddField("Balansas", pullUser.Balance.ToString())
                        .AddField("xp", pullUser.XP.ToString())
                        .WithThumbnail(pullUser.AvatarUrl)
                );
                    await ctx.Channel.SendMessageAsync(profile);
            }

            else
            {
                var failedMessage = new DiscordEmbedBuilder()
                {
                    Title = "Kazkas nepavyko kuriant profili",
                    Color = DiscordColor.Sienna
                };
                await ctx.Channel.SendMessageAsync(embed: failedMessage);
            }
            }
            else
            {
               var pullUser = balansoEngine.GetUser(username, guildID);
               
                var profile = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Wheat)
                    .WithTitle(pullUser.UserName + " profilis")
                    .AddField("Balansas", pullUser.Balance.ToString())
                    .AddField("xp", pullUser.XP.ToString())
                    .WithThumbnail(pullUser.AvatarUrl)
            );
                await ctx.Channel.SendMessageAsync(profile);
            }

           
         
        }
        [Command("Taskai")]
        public async Task TaskuKomanda(CommandContext ctx)
        {
            IUserStorage userStorage = new UserDataStorage();
            BalansoEngine balansoEngine = new BalansoEngine(userStorage);
            string username = ctx.User.Username;
            ulong guildID = ctx.Guild.Id;
            var pullUser = balansoEngine.GetUser(username, guildID);
            int balance = pullUser.Balance;
           
            var papildyta = balansoEngine.Nobalance(username, guildID);
            if (papildyta == true )
            {
                var profile2 = new DiscordEmbedBuilder()
                {
                    Title = "Taskai sekmingai papildyti",
                    Color = DiscordColor.Green,
                    Description = "į " + pullUser.UserName + "pridėta 500 taškų"
                    
                    
                };
                await ctx.Channel.SendMessageAsync(embed: profile2);
            }
            else
            {
                var profile3 = new DiscordEmbedBuilder()
                {
                    Title = "Taskai nepapildyti",
                    Color = DiscordColor.Red,
                    Description = "Kad papildyti taskus reikia tureti 5 ar maziau tasku balanse "


                };
                await ctx.Channel.SendMessageAsync(embed: profile3);
            }
         
        }
        [Command("pradeti")]
        public async Task Pradzia(CommandContext ctx)
        {
            var button = new DiscordButtonComponent(ButtonStyle.Danger, "button", "👤Profilis📊");
            var button2 = new DiscordButtonComponent(ButtonStyle.Primary, "button2", "🎮Žaidimai🕹️");
            var button16 = new DiscordButtonComponent(ButtonStyle.Success, "button16", "🛍️Parduotuvė💰");

            var message = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithColor(DiscordColor.Aquamarine)
                .WithTitle("Sveiki, aš esu Admiral Capital! Pasirink norimą veiksmą spustelėjęs mygtuką žemiau."))
                .AddComponents(button, button2, button16);


            await ctx.Channel.SendMessageAsync(message);


        }
    }
}

