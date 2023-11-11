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

public class AbilityCommand : CommandBase {
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
            PassiveInfo passiveInfo = fullInfo.Passive;
            await context.RespondCommandOk(new DiscordEmbedBuilder()
                .WithDescription(CreatePassiveDescription(fullInfo, passiveInfo))
                .WithThumbnail(passiveInfo.spellIconUrl)
            );
            return;
        }

        AbilityInfo abilityInfo = fullInfo.GetAbility(_ability);
        await context.RespondCommandOk(new DiscordEmbedBuilder()
            .WithDescription(CreateSpellDescription(fullInfo, abilityInfo))
            .WithThumbnail(abilityInfo.spellIconUrl)
            .WithImageUrl(abilityInfo.spellUsageGifUrl)
        );
    }
    
    private static string CreateSpellDescription(TopChampionInfoResponse championInfo, AbilityInfo abilityInfo) {
        return new StringBuilder()
            .AppendListEntry(Emote.BULLET_BLUE, $"{"Champion:".ToBold()} {championInfo.Data.Name}")
            .AppendLine()
            .AppendListEntry(Emote.BULLET_BLUE, $"{"Ability Spell:".ToBold()} {abilityInfo.spellEmoji}")
            .AppendLine()
            .AppendListEntry(Emote.BULLET_BLUE, $"{"Ability Name:".ToBold()} {abilityInfo.response.Name}")
            .AppendLine()
            .AppendListEntry(Emote.BULLET_BLUE, $"{"Cooldowns:".ToBold()} {abilityInfo.response.Cooldowns}")
            .AppendLine()
            .AppendListEntry(Emote.BULLET_BLUE, $"{"Description:".ToBold()} {abilityInfo.response.Description.FormatLeagueTextForEmbed()}")
            .AppendLine()
            .AppendListEntry(Emote.BULLET_BLUE, "Tooltip:".ToBold())
            .AppendLine(abilityInfo.response.Tooltip.FormatLeagueTextForEmbed())
            .ToString();
    }
    
    private static string CreatePassiveDescription(TopChampionInfoResponse championInfo, PassiveInfo passiveInfo) {
        return new StringBuilder()
            .AppendListEntry(Emote.BULLET_BLUE, $"{"Champion:".ToBold()} {championInfo.Data.Name}")
            .AppendLine()
            .AppendListEntry(Emote.BULLET_BLUE, $"{"Passive Name:".ToBold()}: {passiveInfo.response.Name}")
            .AppendLine()
            .AppendListEntry(Emote.BULLET_BLUE, $"{"Description:".ToBold()} {passiveInfo.response.Description.FormatLeagueTextForEmbed()}")
            .ToString();
    }
}