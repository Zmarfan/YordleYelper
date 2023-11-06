using System;
using System.Collections.Generic;
using System.Linq;
using YordleYelper.bot.data_fetcher.league_api;
using YordleYelper.bot.data_fetcher.league_api.data;
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

        // 2. if the daily fetch isn't done for any player do so.
        // 3. Otherwise fetch match data for match ids in db that doesn't have any data
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