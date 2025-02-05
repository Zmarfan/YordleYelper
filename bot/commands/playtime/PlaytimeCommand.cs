﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.data_fetcher.data_dragon.responses;
using YordleYelper.bot.data_fetcher.league_api.responses;
using YordleYelper.bot.extensions;
using YordleYelper.bot.response_creator;
using YordleYelper.database;

namespace YordleYelper.bot.commands.playtime; 

public class PlaytimeCommand : CommandBase {
    private readonly LeagueAccount _leagueAccount;
    private readonly BasicChampionInfo _championInfo;
    private readonly bool _compareAgainstAll;
    private readonly bool _separateGameMode;
    private readonly Database _database;

    public PlaytimeCommand(LeagueAccount leagueAccount, BasicChampionInfo championInfo, bool compareAgainstAll, bool separateGameMode, Database database) {
        _leagueAccount = leagueAccount;
        _championInfo = championInfo;
        _compareAgainstAll = compareAgainstAll;
        _separateGameMode = separateGameMode;
        _database = database;
    }

    protected override async Task Run(InteractionContext context) {
        ChampionPlaytimeRecord playtimeData = _database.ExecuteQuery(new FetchChampionPlayTimeDataQueryData(_leagueAccount, _championInfo));
        List<PlaysPerDayRecord> playsPerDayRecords = _database.ExecuteListQuery(new FetchChampionPlaysPerDayQueryData(_leagueAccount, _championInfo));
        string chartUrl = PlayTimeChartCreator.CreateChart($"{_championInfo.Name} games per day for {_leagueAccount.gameName}", 800, 400, playsPerDayRecords, _compareAgainstAll, _separateGameMode);
        await context.RespondCommandOk(new DiscordEmbedBuilder()
            .WithDescription($"The statistics shown below regarding playtime are based on the last 1000+ games {_leagueAccount.gameName.ToBold()} has played!")
            .AddExtraLargeField("Champion", _championInfo.Name, true)
            .AddExtraLargeField("First Played", playtimeData.TotalAmount == 0 ? "-" : playtimeData.FirstPlayed.ToShortDateString(), true)
            .AddExtraLargeField("Last Played", playtimeData.TotalAmount == 0 ? "-" : playtimeData.LastPlayed.ToShortDateString(), true)
            .AddExtraLargeField("Total Games", playtimeData.TotalAmount.ToString(), true)
            .AddExtraLargeField("Rift Games", playtimeData.RiftAmount.ToString(), true)
            .AddExtraLargeField("Aram Games", playtimeData.AramAmount.ToString(), true)
            .AddField("Total Playtime", TimeSpan.FromSeconds(playtimeData.TotalPlayTimeInSeconds).ToTimeSinceString(), true)
            .AddField("Rift Playtime", TimeSpan.FromSeconds(playtimeData.RiftPlaytimeInSeconds).ToTimeSinceString(), true)
            .AddField("Aram Playtime", TimeSpan.FromSeconds(playtimeData.AramPlaytimeInSeconds).ToTimeSinceString(), true)
            .WithThumbnail(_championInfo.PortraitImageUrl)
            .WithImageUrl(chartUrl)
        );
    }
}