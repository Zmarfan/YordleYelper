using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.commands.choices;
using YordleYelper.bot.data_fetcher;
using YordleYelper.bot.data_fetcher.responses;
using YordleYelper.bot.data_fetcher.responses.champion_info;
using YordleYelper.bot.extensions;
using YordleYelper.bot.response_creator;

namespace YordleYelper.bot.commands; 

public class AbilityCommand : CommandBase {
    public const string COMMAND_NAME = "ability";

    private readonly BasicChampionInfo _basicChampionInfo;
    private readonly ChampionAbility _ability;
    private readonly DataDragonProxy _dataDragonProxy;
    
    public AbilityCommand(BasicChampionInfo basicChampionInfo, ChampionAbility ability, DataDragonProxy dataDragonProxy) {
        _basicChampionInfo = basicChampionInfo;
        _ability = ability;
        _dataDragonProxy = dataDragonProxy;
    }

    protected override async Task Run(InteractionContext context) {
        TopChampionInfoResponse fullInfo = _dataDragonProxy.GetChampionInfo(_basicChampionInfo);

        if (_ability == ChampionAbility.Passive) {
            return;
        }

        AbilityInfo abilityInfo = fullInfo.GetAbility(_ability);
        await context.CreateCommandOk(new DiscordEmbedBuilder()
            .WithDescription(CreateSpellDescription(fullInfo, abilityInfo))
            .WithThumbnail(abilityInfo.spellIconUrl)
            .WithImageUrl(abilityInfo.spellUsageGifUrl)
        );
    }
    
    private static string CreateSpellDescription(TopChampionInfoResponse championInfo, AbilityInfo abilityInfo) {
        return $"""
:small_blue_diamond: **Champion:** {championInfo.Data.Name}

:small_blue_diamond: **Ability Spell:** {abilityInfo.spellEmoji}

:small_blue_diamond: **Ability Name:** {abilityInfo.response.Name}

:small_blue_diamond: **Description:** {abilityInfo.response.Description.FormatLeagueTextForEmbed()}

:small_blue_diamond: **Tooltip:** {abilityInfo.response.Tooltip.FormatLeagueTextForEmbed()}
""";
    }
}