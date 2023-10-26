using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.data_fetcher;

namespace YordleYelper.bot.commands; 

public class SlashCommands : ApplicationCommandModule {
    public DataFetcher DataFetcher { private get; set; }
    
    [SlashCommand("ping", "todo")]
    public async Task Ping(InteractionContext context) {
        await context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Pong!"));
    } 
    
    [SlashCommand("champion", "todo")]
    public async Task Champion(
        InteractionContext context, 
        [Option("name", "todo")] string championName
    ) {
        await new ChampionCommand(DataFetcher, championName).Execute(context);
    }
}