using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.data_fetcher.data_dragon;
using YordleYelper.bot.data_fetcher.data_dragon.responses;
using YordleYelper.bot.data_fetcher.league_api;
using YordleYelper.bot.data_fetcher.league_api.data;
using YordleYelper.bot.data_fetcher.league_api.responses;
using YordleYelper.bot.extensions;
using YordleYelper.bot.response_creator;

namespace YordleYelper.bot.commands; 

public class LastPlayedAllCommand : CommandBase {
    private readonly LeagueAccount _leagueAccount;
    private readonly List<BasicChampionInfo> _basicChampionInfos;
    private readonly LeagueApiProxy _leagueApiProxy;

    public LastPlayedAllCommand(LeagueAccount leagueAccount, List<BasicChampionInfo> basicChampionInfos, LeagueApiProxy leagueApiProxy) {
        _leagueAccount = leagueAccount;
        _basicChampionInfos = basicChampionInfos;
        _leagueApiProxy = leagueApiProxy;
    }
    
    protected override async Task Run(InteractionContext context) {
        Dictionary<string, BasicChampionInfo> champByKey = _basicChampionInfos.ToDictionary(champ => champ.Key, champ => champ);
        List<string> championPlayTimes = (await _leagueApiProxy.GetChampionMasteries(_leagueAccount))
            .OrderBy(mastery => Math.Abs((DateTimeOffset.Now - mastery.lastPlayed).TotalMilliseconds))
            .Select((mastery, i) => $"{i + 1}. **{champByKey[mastery.championId].Name}**: {(DateTimeOffset.Now - mastery.lastPlayed).ToTimeSinceString()}")
            .ToList();

        await context.CreateCommandOk(b => b
            .WithDescription($"The last time **{_leagueAccount.gameName}** played each champion is as follows:")
        );

        while (championPlayTimes.Any()) {
            List<string> takeEntries = championPlayTimes.Take(50).ToList();
            championPlayTimes = championPlayTimes.Skip(50).ToList();
            string description = string.Join("\n", takeEntries);
            await context.Channel.SendMessageAsync(context.CreateCommandEmbedBuilderOk(b => b.WithDescription(description)));
        }
    }
}