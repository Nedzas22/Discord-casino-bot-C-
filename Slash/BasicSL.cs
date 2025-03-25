using DSharpPlus.SlashCommands;
using System.Threading.Tasks;

namespace AdmiralCapital.Slash
{
    public class BasicSL : ApplicationCommandModule
    {
        [SlashCommand("testas", "test")]
        
        public async Task MyFirstSlashCommand(InteractionContext ctx)
        {
            await ctx.Channel.SendMessageAsync("test");
        }
    }
}
