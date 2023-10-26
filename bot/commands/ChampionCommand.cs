using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.data_fetcher;
using YordleYelper.bot.data_fetcher.responses;

namespace YordleYelper.bot.commands; 

public class ChampionCommand : CommandBase {
    private readonly DataFetcher _dataFetcher;
    private readonly string _championName;
    
    public ChampionCommand(DataFetcher dataFetcher, string championName) {
        _dataFetcher = dataFetcher;
        _championName = championName;
    }

    protected override async Task Run(InteractionContext context) {
        ChampionInfo? champion = _dataFetcher.GetChampion(_championName);
        await context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent(champion?.Name ?? "Missing"));
    }
}