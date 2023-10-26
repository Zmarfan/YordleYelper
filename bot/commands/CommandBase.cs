using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.logger;

namespace YordleYelper.bot.commands; 

public abstract class CommandBase {
    public async Task Execute(InteractionContext context) {
        try {
            await Run(context);
        } catch (Exception e) {
            Logger.Error(e, $"Command error in {GetType()}!");
            await CreateDefaultErrorResponse(context);
        }
    }

    protected abstract Task Run(InteractionContext context);
    
    private static async Task CreateDefaultErrorResponse(BaseContext context) {
        await context.CreateResponseAsync(new DiscordEmbedBuilder()
            .WithTitle(":scream::scream: Yikes! :scream::scream:")
            .WithDescription("There was an internal error! Please try again later!")
            .WithColor(DiscordColor.Red)
            .Build());
    }
}