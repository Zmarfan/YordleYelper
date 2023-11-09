using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Extensions.Logging;
using YordleYelper.bot.background.match;
using YordleYelper.bot.data;
using YordleYelper.bot.data_fetcher.league_api;
using YordleYelper.bot.data_fetcher.league_api.data;
using YordleYelper.bot.data_fetcher.league_api.responses.match;
using YordleYelper.bot.http_client;
using YordleYelper.database;

namespace YordleYelper.bot.background; 

public class BackgroundHandler {
    private readonly LeagueApiProxy _leagueApiProxy;
    private readonly Database _database;
    private readonly ILogger _logger;

    public BackgroundHandler(LeagueApiProxy leagueApiProxy, Database database, ILogger logger) {
        _leagueApiProxy = leagueApiProxy;
        _database = database;
        _logger = logger;
    }

    public void Run() {
        try {
            List<Puuid> notInitializedPuuids = _database.ExecuteBasicListQuery(new GetNotInitializedPlayerPuuidsQueryData());
            if (notInitializedPuuids.Any()) {
                InitializePlayer(notInitializedPuuids.First());
                return;
            }

            List<Puuid> dailyDataFetchUsers = _database.ExecuteBasicListQuery(new GetDailyUsersDataFetchQueryData()).Take(10).ToList();
            if (dailyDataFetchUsers.Any()) {
                FetchDailyDataBatch(dailyDataFetchUsers);
                return;
            }

            List<string> matchIdsToFetchDataFor = _database.ExecuteBasicListQuery(new FetchMatchIdsWithNoDataQueryData()).Take(10).ToList();
            FetchMatchData(matchIdsToFetchDataFor);
        }
        catch (AggregateException e) {
            if (e.InnerException is HttpStatusException httpStatusException && (int)httpStatusException.statusCode == 429) {
                _logger.LogWarning(httpStatusException, "Background rate limit");
            }
            else {
                throw;
            }
        }
    }

    private void InitializePlayer(Puuid puuid) {
        List<string> matchIds = new();
        int startIndex = 1000;
        while (startIndex > 0) {
            startIndex -= 100;
            List<string> matchIdsBatch = _leagueApiProxy.FetchMatchesByPuuid(puuid, startIndex);
            matchIdsBatch.Reverse();
            matchIds.AddRange(matchIdsBatch);
        }
        
        InsertMatchIds(matchIds);
        _database.ExecuteVoidQuery(new MarkPlayerAsInitializedQueryData(puuid));
    }

    private void FetchDailyDataBatch(List<Puuid> dailyDataFetchUsers) {
        foreach (Puuid puuid in dailyDataFetchUsers) {
            List<string> matchIds = _leagueApiProxy.FetchMatchesByPuuid(puuid, 0);
            matchIds.Reverse();
            InsertMatchIds(matchIds);
            _database.ExecuteVoidQuery(new MarkPlayerAsCompletedDailyDataFetchQueryData(puuid));
        }
    }
    
    private void FetchMatchData(List<string> matchIdsToFetchDataFor) {
        foreach (string matchId in matchIdsToFetchDataFor) {
            MatchDataResponse matchData;
            try {
                matchData = _leagueApiProxy.FetchMatchData(matchId);
            }
            catch (AggregateException e) {
                if (e.InnerException is not HttpStatusException { statusCode: HttpStatusCode.NotFound }) {
                    throw;
                }
                _database.ExecuteVoidQuery(new RemoveInvalidMatchIdQueryData(matchId));
                continue;
            }

            Console.WriteLine(matchData.MetaData.MatchId);
            
            if (matchData.Info.gameMode == GameMode.NONE) {
                _database.ExecuteVoidQuery(new RemoveInvalidMatchIdQueryData(matchId));
                continue;
            }
            _database.ExecuteVoidQuery(new InsertMatchDataQueryData(matchData));
            _database.ExecuteVoidQuery(new InsertMatchTeamDataQueryData(matchData, matchData.Info.teams[0]));
            _database.ExecuteVoidQuery(new InsertMatchTeamDataQueryData(matchData, matchData.Info.teams[1]));
            
            foreach (ParticipantResponse participant in matchData.Info.participants) {
                _database.ExecuteVoidQuery(new InsertMatchParticipantDataQueryData(matchData, participant));
                _database.ExecuteVoidQuery(new InsertMatchParticipantChallengesDataQueryData(matchData, participant));
            }
        }
    }
    
    private void InsertMatchIds(List<string> matchIds) {
        _database.ExecuteVoidQuery(new InsertMatchIdsQueryData(matchIds));
    }
}