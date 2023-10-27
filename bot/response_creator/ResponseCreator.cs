using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace YordleYelper.bot.response_creator; 

public static class ResponseCreator {
    private static readonly DiscordColor MESSAGE_COLOR = new(76, 133, 255);
    
    public static async Task Create(this BaseContext context, DiscordEmbedBuilder builder) {
        await context.CreateResponseAsync(new DiscordEmbedBuilder(builder).WithColor(MESSAGE_COLOR).Build());
    }
}