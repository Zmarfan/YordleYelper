using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.data_fetcher.data_dragon.responses;
using YordleYelper.bot.data_fetcher.league_api;
using YordleYelper.bot.data_fetcher.league_api.responses;
using YordleYelper.bot.extensions;
using YordleYelper.bot.quick_chart_creator;
using YordleYelper.bot.response_creator;

namespace YordleYelper.bot.commands.masteries; 

public class MasteryDistributionCommand : CommandBase {
    private readonly LeagueAccount _leagueAccount;
    private readonly bool _showAvailableChests;
    private readonly List<BasicChampionInfo> _championInfos;
    private readonly LeagueApiProxy _leagueApiProxy;

    public MasteryDistributionCommand(
        LeagueAccount leagueAccount,
        bool showAvailableChests,
        List<BasicChampionInfo> championInfos,
        LeagueApiProxy leagueApiProxy
    ) {
        _leagueAccount = leagueAccount;
        _showAvailableChests = showAvailableChests;
        _championInfos = championInfos;
        _leagueApiProxy = leagueApiProxy;
    }
    
    protected override async Task Run(InteractionContext context) {
        Dictionary<string, BasicChampionInfo> championInfoByKey = _championInfos.ToDictionary(c => c.Key, c => c);
        List<(ChampionMasteryResponse, BasicChampionInfo)> data = (await _leagueApiProxy.GetChampionMasteries(_leagueAccount))
            .OrderByDescending(mastery => mastery.championPoints)
            .Select(mastery => (mastery, championInfoByKey[mastery.championId]))
            .ToList();

        string chartUrl = MasteryChartCreator.CreateChart(
            "Mastery Points per Champion Distribution Pie Chart",
            800,
            500,
            _showAvailableChests,
            data
        );

        await context.RespondCommandOk(new DiscordEmbedBuilder()
            .WithDescription($"The attached image shows the different mastery points per champion for {_leagueAccount.gameName.ToBold()}")
            .WithThumbnail(_leagueAccount.summoner.profileIconImageUrl)
            .WithImageUrl(chartUrl)
        );
    }
}