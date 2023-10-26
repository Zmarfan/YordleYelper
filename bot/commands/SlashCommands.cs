using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.data_fetcher;

namespace YordleYelper.bot.commands; 

public class SlashCommands : ApplicationCommandModule {
    public DataFetcher DataFetcher {
        private get => DATA_FETCHER;
        set {
            DATA_FETCHER = value;
        }
    }
    public static DataFetcher DATA_FETCHER { get; private set; }
    

    [SlashCommand("champion", "todo")]
    public async Task Champion(
        InteractionContext context, 
        [Option("name", "todo")] string championName
    ) {
        await new ChampionCommand(championName).Execute(context);
    }
}