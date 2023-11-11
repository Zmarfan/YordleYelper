using System;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.data;
using YordleYelper.bot.extensions;

namespace YordleYelper.bot.response_creator; 

public static class ResponseCreator {
    private static readonly DiscordColor MESSAGE_COLOR = new(76, 133, 255);
    
    public static async Task Respond(this BaseContext context, DiscordEmbedBuilder embedBuilder) {
        await context.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embedBuilder.WithColor(MESSAGE_COLOR).Build()));
    }

    public static async Task RespondCommandOk(this InteractionContext context, DiscordEmbedBuilder embedBuilder) {
        await context.RespondCommand(Emote.OK, embedBuilder);
    }
    
    public static async Task RespondCommandError(this InteractionContext context, DiscordEmbedBuilder embedBuilder) {
        await context.RespondCommand(Emote.ERROR, embedBuilder);
    }
    
    public static async Task RespondCommand(this InteractionContext context, Emote emote, DiscordEmbedBuilder embedBuilder) {
        await context.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(context.CommandEmbed(emote, embedBuilder).Build()));
    }
    
    public static DiscordEmbedBuilder CommandOkEmbed(this InteractionContext context, DiscordEmbedBuilder embedBuilder) {
        return context.CommandEmbed(Emote.OK, embedBuilder);
    }
    
    public static DiscordEmbedBuilder CommandErrorEmbed(this InteractionContext context, DiscordEmbedBuilder embedBuilder) {
        return context.CommandEmbed(Emote.ERROR, embedBuilder);
    }
    
    public static DiscordEmbedBuilder CommandEmbed(this InteractionContext context, Emote emote, DiscordEmbedBuilder embedBuilder) {
        return embedBuilder
            .WithColor(MESSAGE_COLOR)
            .WithTitle($"{emote} YordleYelper {context.CommandName.FirstCharToUpper()} Command");
    }
    
    public static async Task NoSuchChampionResponse(this InteractionContext context) {
        await context.CreateResponseAsync(context.CommandErrorEmbed(new DiscordEmbedBuilder()).WithDescription("Provided champion does not exist!"));
    }
    
    public static async Task NoSuchItemResponse(this InteractionContext context) {
        await context.CreateResponseAsync(context.CommandErrorEmbed(new DiscordEmbedBuilder()).WithDescription("Provided item does not exist!"));
    }
    
    public static async Task NoSuchRiotIdResponse(this InteractionContext context) {
        await context.CreateResponseAsync(context.CommandErrorEmbed(new DiscordEmbedBuilder()).WithDescription("Unable to find player with specified Riot Id!"));
    }
    
    public static async Task NoSuchRegisteredRiotId(this InteractionContext context) {
        await context.CreateResponseAsync(context.CommandErrorEmbed(new DiscordEmbedBuilder()).WithDescription("The specified Riot Id has not been registered for data collection!"));
    }
}