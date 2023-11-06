﻿using System;
using System.Collections.Generic;
using System.Linq;
using YordleYelper.bot.data_fetcher.league_api;
using YordleYelper.bot.data_fetcher.league_api.data;
using YordleYelper.bot.data_fetcher.league_api.responses.match;
using YordleYelper.bot.http_client;
using YordleYelper.database;

namespace YordleYelper.bot.background; 

public class BackgroundHandler {
    private readonly LeagueApiProxy _leagueApiProxy;
    private readonly Database _database;

    public BackgroundHandler(LeagueApiProxy leagueApiProxy, Database database) {
        _leagueApiProxy = leagueApiProxy;
        _database = database;
    }

    public void Run() {
        List<Puuid> notInitializedPuuids = _database.ExecuteBasicListQuery(new GetNotInitializedPlayerPuuidsQueryData());
        if (notInitializedPuuids.Any()) {
            InitializePlayer(notInitializedPuuids.First());
            return;
        }

        List<Puuid> dailyDataFetchUsers = _database.ExecuteBasicListQuery(new GetDailyUsersDataFetchQueryData()).Take(10).ToList();
        if (dailyDataFetchUsers.Any()) {
            foreach (Puuid puuid in dailyDataFetchUsers) {
                List<string> matchIds = _leagueApiProxy.FetchMatchesByPuuid(puuid, 0);
                matchIds.Reverse();
                InsertMatchIds(matchIds);
                _database.ExecuteVoidQuery(new MarkPlayerAsCompletedDailyDataFetchQueryData(puuid));
            }
            return;
        }

        List<string> matchIdsToFetchDataFor = _database.ExecuteBasicListQuery(new FetchMatchIdsWithNoDataQueryData()).Take(10).ToList();
        foreach (string matchId in matchIdsToFetchDataFor) {
            MatchDataResponse matchData = _leagueApiProxy.FetchMatchData(matchId);
            Console.WriteLine();
        }
    }

    private void InitializePlayer(Puuid puuid) {
        List<string> matchIds = new();
        int startIndex = 1000;
        while (startIndex >= 0) {
            startIndex -= 100;
            List<string> matchIdsBatch = _leagueApiProxy.FetchMatchesByPuuid(puuid, startIndex);
            matchIdsBatch.Reverse();
            matchIds.AddRange(matchIdsBatch);
        }
        
        InsertMatchIds(matchIds);
        _database.ExecuteVoidQuery(new MarkPlayerAsInitializedQueryData(puuid));
    }

    private void InsertMatchIds(List<string> matchIds) {
        _database.ExecuteVoidQuery(new InsertMatchIdsQueryData(matchIds));
    }
}