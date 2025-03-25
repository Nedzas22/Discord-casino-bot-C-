using AdmiralCapital.commands;
using AdmiralCapital.config;
using AdmiralCapital.Interfeisas;
using AdmiralCapital.Slash;
using AdmiralCapital.zaidimai;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmiralCapital
{
    public sealed class Program
    {
        public static DiscordClient Client { get; set; }
        private static CommandsNextExtension Commands {  get; set; }
        static async Task Main(string[] args)
        
        {
            var jsonReader = new JSONreader();
            await jsonReader.ReadJSON();

        

            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = jsonReader.Token,
                TokenType = TokenType.Bot,
                AutoReconnect  = true
            
            };
            Client = new DiscordClient(discordConfig);

            Client.UseInteractivity(new InteractivityConfiguration()
            {
                Timeout = TimeSpan.FromMinutes(2)
            });

            Client.Ready += Client_Ready;
            Client.ComponentInteractionCreated += Client_ComponentInteractionCreated;
       

            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] { jsonReader.Prefix },
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false,
            };
            Commands = Client.UseCommandsNext(commandsConfig);
            var slashCommandsConfiguration = Client.UseSlashCommands();

            Commands.RegisterCommands<TestCommands>();
            slashCommandsConfiguration.RegisterCommands<BasicSL>();
          

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private static async Task Client_ComponentInteractionCreated(DiscordClient client, DSharpPlus.EventArgs.ComponentInteractionCreateEventArgs args)
        {
            var GameEngine = new GameEngine();
            
            switch (args.Interaction.Data.CustomId)
            {
                case "button":
                   
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DSharpPlus.Entities.DiscordInteractionResponseBuilder().WithContent($"{args.User.Username} Pasirinko pamatyti savo profilį"));
                    await InterfaceEngine.callProfile(args.User, args.Channel, args.Guild.Id);
                    break;
                case "button2":
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DSharpPlus.Entities.DiscordInteractionResponseBuilder().WithContent($"{args.User.Username} Pasirinko pamatyti žaidimus"));
                    await InterfaceEngine.Games(args.User, args.Channel, args.Guild.Id);
                    break;
                case "button3":
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DSharpPlus.Entities.DiscordInteractionResponseBuilder().WithContent($"{args.User.Username} has pressed button 3"));
                    await GameEngine.PlayGame("blackjack", args.User, args.Channel, args.Guild.Id);
                    break;
                case "button4":
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DSharpPlus.Entities.DiscordInteractionResponseBuilder().WithContent($"{args.User.Username} has pressed button 4"));
                    await GameEngine.PlayGame("roulette", args.User, args.Channel, args.Guild.Id);
                    break;
                case "button5":
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DSharpPlus.Entities.DiscordInteractionResponseBuilder().WithContent($"{args.User.Username} has pressed button 5"));
                    await GameEngine.PlayGame("dice", args.User, args.Channel, args.Guild.Id);
                    break;
                case "button6":
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DSharpPlus.Entities.DiscordInteractionResponseBuilder().WithContent($"{args.User.Username} has pressed button 2"));
                    await GameEngine.PlayGame("guess", args.User, args.Channel, args.Guild.Id);
                    break;
                case "button7":
            
                    await GameEngine.PlayGame("crash", args.User, args.Channel, args.Guild.Id);
                    break;
                case "button8":
                   
                    break;
                case "button9":
                   
                    break;
                case "button10":
                 
                    break;
                case "button11":
                 
                    break;
                case "button12":
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DSharpPlus.Entities.DiscordInteractionResponseBuilder().WithContent($"{args.User.Username} Pasirinko pamatyti žaidimus"));
                    await InterfaceEngine.Pradzia(args.User, args.Channel, args.Guild.Id);
                    break;
                case "button13":
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DSharpPlus.Entities.DiscordInteractionResponseBuilder().WithContent($"{args.User.Username} Tai yra pabaiga. Norėdami pradėti iš naujo naudokite komanda /pradeti"));
                    break;
                case "button14":
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DSharpPlus.Entities.DiscordInteractionResponseBuilder().WithContent($"{args.User.Username} Kitas puslapis"));
                    await InterfaceEngine.Games2(args.User, args.Channel, args.Guild.Id);
                    break;
                case "button15":
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DSharpPlus.Entities.DiscordInteractionResponseBuilder().WithContent($"{args.User.Username} Pirmas puslapis"));
                    await InterfaceEngine.Games(args.User, args.Channel, args.Guild.Id);
                    break;
                case "button16":
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DSharpPlus.Entities.DiscordInteractionResponseBuilder().WithContent($"16 mygtukas"));
                    await Shop.Parduotuve1(args.User, args.Channel, args.Guild.Id);
                    break;
                case "button17":
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DSharpPlus.Entities.DiscordInteractionResponseBuilder().WithContent($"16 mygtukas"));
                    await Shop.Parduotuve(args.User, args.Channel, args.Guild.Id);
                    break;
            }
        }

            private static Task Client_Ready(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
