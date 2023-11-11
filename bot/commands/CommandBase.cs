using System;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.Logging;
using YordleYelper.bot.data;
using YordleYelper.bot.extensions;
using YordleYelper.bot.response_creator;

namespace YordleYelper.bot.commands; 

public abstract class CommandBase {
    public async Task Execute(InteractionContext context) {
        try {
            context.Client.Logger.LogInformation($"Running: {GetType()}");
            await context.DeferAsync();
            await Run(context);
        } catch (Exception e) {
            context.Client.Logger.LogError(e, $"Command error in {GetType()}!");
            await CreateDefaultErrorResponse(context);
        }
    }

    protected abstract Task Run(InteractionContext context);

    private async Task CreateDefaultErrorResponse(BaseContext context) {
        await context.Respond(new DiscordEmbedBuilder()
            .WithTitle($"{Emote.INTERNAL_ERROR}{Emote.INTERNAL_ERROR} Yikes! {Emote.INTERNAL_ERROR}{Emote.INTERNAL_ERROR}")
            .WithDescription("There was an internal error! Please try again later!")
        );
    }
}