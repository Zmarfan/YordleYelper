using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.data_fetcher;
using YordleYelper.bot.data_fetcher.responses;
using YordleYelper.bot.data_fetcher.responses.champion_info;
using YordleYelper.bot.response_creator;

namespace YordleYelper.bot.commands; 

public class ChampionCommand : CommandBase {
    private readonly BasicChampionInfo _basicChampionInfo;
    private readonly DataDragonProxy _dataDragonProxy;
    
    public ChampionCommand(BasicChampionInfo basicChampionInfo, DataDragonProxy dataDragonProxy) {
        _basicChampionInfo = basicChampionInfo;
        _dataDragonProxy = dataDragonProxy;
    }

    protected override async Task Run(InteractionContext context) {
        TopChampionInfoResponse fullInfo = _dataDragonProxy.GetChampionInfo(_basicChampionInfo);
        
        await context.CreateCommandOk(new DiscordEmbedBuilder()
            .WithDescription(CreateDescription(fullInfo))
            .WithThumbnail(fullInfo.PortraitImageUrl)
        );
    }
    
    private static string CreateDescription(TopChampionInfoResponse fullInfo) {
        return $"""
:small_blue_diamond: **Name:** {fullInfo.Data.Name}

:small_blue_diamond: **Title:** {fullInfo.Data.Title}

:small_blue_diamond: **Lore:** {fullInfo.Data.Lore}

:small_blue_diamond: **Ally Tips:**

{fullInfo.Data.AllyTips.Aggregate(new StringBuilder(), (acc, tip) => acc.AppendLine($":white_small_square: {tip}\n"))}
:small_orange_diamond: **Enemy Tips:**

{fullInfo.Data.EnemyTips.Aggregate(new StringBuilder(), (acc, tip) => acc.AppendLine($":white_small_square: {tip}\n"))}
""";
    }
}