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
    private const string COMMAND_NAME = "YordleYelper Champion Command";
    
    private readonly string _championName;
    
    public ChampionCommand(string championName) {
        _championName = championName;
    }

    protected override async Task Run(InteractionContext context) {
        if (!DataDragonProxy.TryGetBasicChampionInfo(_championName, out BasicChampionInfo basicInfo)) {
            (BasicChampionInfo, int) similarChampion = DataDragonProxy.GetMostSimilarChampionBasicInfo(_championName);
            if (similarChampion.Item2 > 2) {
                await NoSuchChampionResponse(context);
                return;
            }

            basicInfo = similarChampion.Item1;
        }

        TopChampionInfoResponse fullInfo = DataDragonProxy.GetChampionInfo(basicInfo);
        
        await context.Create(new DiscordEmbedBuilder()
            .WithTitle($":sparkles: {COMMAND_NAME}")
            .WithDescription(CreateDescription(fullInfo))
            .WithThumbnail(fullInfo.PortraitImageUrl)
        );
    }

    private static async Task NoSuchChampionResponse(BaseContext context) {
        await context.Create(new DiscordEmbedBuilder()
            .WithTitle($":question: {COMMAND_NAME}")
            .WithDescription("Provided champion does not exist?")
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