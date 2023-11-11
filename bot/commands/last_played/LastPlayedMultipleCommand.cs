using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.data;
using YordleYelper.bot.data_fetcher.data_dragon.responses;
using YordleYelper.bot.data_fetcher.league_api;
using YordleYelper.bot.data_fetcher.league_api.responses;
using YordleYelper.bot.extensions;
using YordleYelper.bot.response_creator;

namespace YordleYelper.bot.commands.last_played; 

public class LastPlayedMultipleCommand : CommandBase {
    private const int CHAMPIONS_TO_SHOW_PER_MESSAGE = 20;
    
    private readonly LeagueAccount _leagueAccount;
    private readonly List<BasicChampionInfo> _basicChampionInfos;
    private readonly int _amountToShow;
    private readonly SortOrder _sortOrder;
    private readonly LeagueApiProxy _leagueApiProxy;

    public LastPlayedMultipleCommand(
        LeagueAccount leagueAccount,
        List<BasicChampionInfo> basicChampionInfos,
        int amountToShow,
        SortOrder sortOrder,
        LeagueApiProxy leagueApiProxy
    ) {
        _leagueAccount = leagueAccount;
        _basicChampionInfos = basicChampionInfos;
        _amountToShow = amountToShow;
        _sortOrder = sortOrder;
        _leagueApiProxy = leagueApiProxy;
    }
    
    protected override async Task Run(InteractionContext context) {
        Dictionary<string, BasicChampionInfo> champByKey = _basicChampionInfos.ToDictionary(champ => champ.Key, champ => champ);
        List<ChampionMasteryResponse> masteries = (await _leagueApiProxy.GetChampionMasteries(_leagueAccount))
            .OrderBy(mastery => Math.Abs((DateTime.Now - mastery.lastPlayed).TotalMilliseconds), _sortOrder)
            .Take(_amountToShow)
            .ToList();

        await context.RespondCommandOk(new DiscordEmbedBuilder()
            .WithDescription($"The last time {_leagueAccount.gameName.ToBold()} played each champion is as follows:")
            .WithThumbnail(_leagueAccount.summoner.profileIconImageUrl)
        );

        for (int i = 0; i < masteries.Count; i += CHAMPIONS_TO_SHOW_PER_MESSAGE) {
            List<ChampionMasteryResponse> takeAmount = masteries.Skip(i).Take(CHAMPIONS_TO_SHOW_PER_MESSAGE).ToList();
            await context.Channel.SendMessageAsync(context.CommandOkEmbed(new DiscordEmbedBuilder()
                .WithThumbnail(_leagueAccount.summoner.profileIconImageUrl)
                .AddField("No.", string.Join(".\n", Enumerable.Range(i + 1, takeAmount.Count)), true)
                .AddField("Champion", string.Join("\n", takeAmount.Select(mastery => champByKey[mastery.championId].Name)), true)
                .AddField("Last Played", string.Join("\n", takeAmount.Select(mastery => (DateTime.Now - mastery.lastPlayed).ToTimeSinceString())), true)
            ));
        }
    }
}