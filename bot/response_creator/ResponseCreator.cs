using System;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.data;
using YordleYelper.bot.extensions;

namespace YordleYelper.bot.response_creator; 

public static class ResponseCreator {
    private static readonly DiscordColor MESSAGE_COLOR = new(76, 133, 255);
    
    public static async Task Create(this BaseContext context, Func<DiscordEmbedBuilder, DiscordEmbedBuilder> builderFunc) {
        await context.CreateResponseAsync(builderFunc.Invoke(new DiscordEmbedBuilder()).WithColor(MESSAGE_COLOR).Build());
    }

    public static async Task CreateCommandOk(this InteractionContext context, Func<DiscordEmbedBuilder, DiscordEmbedBuilder> builderFunc) {
        await context.CreateCommand(Emote.OK, builderFunc);
    }
    
    public static async Task CreateCommandError(this InteractionContext context, Func<DiscordEmbedBuilder, DiscordEmbedBuilder> builderFunc) {
        await context.CreateCommand(Emote.ERROR, builderFunc);
    }
    
    public static async Task CreateCommand(this InteractionContext context, Emote emote, Func<DiscordEmbedBuilder, DiscordEmbedBuilder> builderFunc) {
        await context.CreateResponseAsync(context.CreateCommandEmbedBuilder(emote, builderFunc).Build());
    }
    
    public static DiscordEmbedBuilder CreateCommandEmbedBuilderOk(this InteractionContext context, Func<DiscordEmbedBuilder, DiscordEmbedBuilder> builderFunc) {
        return context.CreateCommandEmbedBuilder(Emote.OK, builderFunc);
    }
    
    public static DiscordEmbedBuilder CreateCommandEmbedBuilderError(this InteractionContext context, Func<DiscordEmbedBuilder, DiscordEmbedBuilder> builderFunc) {
        return context.CreateCommandEmbedBuilder(Emote.ERROR, builderFunc);
    }
    
    public static DiscordEmbedBuilder CreateCommandEmbedBuilder(this InteractionContext context, Emote emote, Func<DiscordEmbedBuilder, DiscordEmbedBuilder> builderFunc) {
        return builderFunc.Invoke(new DiscordEmbedBuilder())
            .WithColor(MESSAGE_COLOR)
            .WithTitle($"{emote} YordleYelper {context.CommandName.FirstCharToUpper()} Command");
    }
    
    public static async Task NoSuchChampionResponse(this InteractionContext context) {
        await context.CreateCommandError(b => b.WithDescription("Provided champion does not exist!"));
    }
    
    public static async Task NoSuchItemResponse(this InteractionContext context) {
        await context.CreateCommandError(b => b.WithDescription("Provided item does not exist!"));
    }
    
    public static async Task NoSuchRiotIdResponse(this InteractionContext context) {
        await context.CreateCommandError(b => b.WithDescription("Unable to find player with specified Riot Id!"));
    }
}