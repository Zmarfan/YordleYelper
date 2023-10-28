using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.extensions;

namespace YordleYelper.bot.response_creator; 

public static class ResponseCreator {
    private static readonly DiscordColor MESSAGE_COLOR = new(76, 133, 255);
    
    public static async Task Create(this BaseContext context, DiscordEmbedBuilder builder) {
        await context.CreateResponseAsync(new DiscordEmbedBuilder(builder).WithColor(MESSAGE_COLOR).Build());
    }
    
    public static async Task CreateCommandOk(this InteractionContext context, DiscordEmbedBuilder builder) {
        await context.CreateCommand(":sparkles:", builder);
    }
    
    public static async Task CreateCommandError(this InteractionContext context, DiscordEmbedBuilder builder) {
        await context.CreateCommand(":question:", builder);
    }
    
    public static async Task CreateCommand(this InteractionContext context, string emoji, DiscordEmbedBuilder builder) {
        await context.CreateResponseAsync(new DiscordEmbedBuilder(builder)
            .WithColor(MESSAGE_COLOR)
            .WithTitle($"{emoji} YordleYelper {context.CommandName.FirstCharToUpper()} Command")
            .Build());
    }
    
    public static async Task NoSuchChampionResponse(this InteractionContext context) {
        await context.CreateCommandError(new DiscordEmbedBuilder()
            .WithDescription("Provided champion does not exist!")
        );
    }
    
    public static async Task NoSuchItemResponse(this InteractionContext context) {
        await context.CreateCommandError(new DiscordEmbedBuilder()
            .WithDescription("Provided item does not exist!")
        );
    }
}