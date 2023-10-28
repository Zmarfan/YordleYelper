using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.data_fetcher;
using YordleYelper.bot.data_fetcher.responses;
using YordleYelper.bot.data_fetcher.responses.champion_info;
using YordleYelper.bot.extensions;
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
        StringBuilder builder = new($":small_blue_diamond: **Name:** {fullInfo.Data.Name}");
        builder.AppendNewLine($":small_blue_diamond: **Title:** {fullInfo.Data.Title}");
        builder.AppendNewLine($":small_blue_diamond: **Lore:** {fullInfo.Data.Lore}");

        if (fullInfo.Data.AllyTips.Any()) {
            builder.AppendNewLine(":small_blue_diamond: **Ally Tips:**");
            builder.AppendLine(fullInfo.Data.AllyTips.Aggregate(new StringBuilder(), (acc, tip) => acc.AppendLine($":white_small_square: {tip}\n")).ToString());
        }
        
        if (fullInfo.Data.EnemyTips.Any()) {
            builder.AppendNewLine(":small_orange_diamond: **Enemy Tips:**");
            builder.AppendLine(fullInfo.Data.EnemyTips.Aggregate(new StringBuilder(), (acc, tip) => acc.AppendLine($":white_small_square: {tip}\n")).ToString());
        }

        return builder.ToString();
    }
}