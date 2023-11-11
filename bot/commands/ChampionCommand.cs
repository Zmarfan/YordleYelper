using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.data;
using YordleYelper.bot.data_fetcher.data_dragon;
using YordleYelper.bot.data_fetcher.data_dragon.responses;
using YordleYelper.bot.data_fetcher.data_dragon.responses.champion_info;
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
        
        await context.RespondCommandOk(new DiscordEmbedBuilder()
            .WithDescription(CreateDescription(fullInfo))
            .WithThumbnail(fullInfo.PortraitImageUrl)
        );
    }
    
    private static string CreateDescription(TopChampionInfoResponse fullInfo) {
        StringBuilder builder = new StringBuilder()
            .AppendListEntry(Emote.BULLET_BLUE, $"{"Name:".ToBold()} {fullInfo.Data.Name}")
            .AppendLine()
            .AppendListEntry(Emote.BULLET_BLUE, $"{"Title:".ToBold()} {fullInfo.Data.Title}")
            .AppendLine()
            .AppendListEntry(Emote.BULLET_BLUE, $"{"Lore:".ToBold()} {fullInfo.Data.Lore}");

        if (fullInfo.Data.AllyTips.Any() || fullInfo.Data.EnemyTips.Any()) {
            builder
                .AppendLine()
                .AppendListEntry(Emote.BULLET_BLUE, "Ally Tips:".ToBold())
                .AppendLine(fullInfo.Data.AllyTips.Aggregate(new StringBuilder(), (acc, tip) => acc.AppendListEntry(Emote.BULLET_WHITE, $"{tip}\n")).ToString())
                .AppendLine(fullInfo.Data.AllyTips.ToString((acc, tip) => acc.AppendListEntry(Emote.BULLET_WHITE, $"{tip}\n")).ToString())
                .AppendListEntry(Emote.BULLET_ORANGE, "Enemy Tips:".ToBold())
                .AppendLine(fullInfo.Data.EnemyTips.ToString((acc, tip) => acc.AppendListEntry(Emote.BULLET_WHITE, $"{tip}\n")).ToString());
        }

        return builder.ToString();
    }
}