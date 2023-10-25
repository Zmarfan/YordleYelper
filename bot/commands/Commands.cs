using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace YordleYelper.bot.commands; 

public class Commands : BaseCommandModule {
    [Command("ping")]
    public async Task Ping(CommandContext context) {
        await context.Channel.SendMessageAsync("Pong").ConfigureAwait(false);
    } 
}