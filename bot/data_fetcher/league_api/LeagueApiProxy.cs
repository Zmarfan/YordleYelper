using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using YordleYelper.bot.data_fetcher.data_dragon.responses;
using YordleYelper.bot.data_fetcher.league_api.data;
using YordleYelper.bot.data_fetcher.league_api.responses;
using YordleYelper.bot.data_fetcher.league_api.responses.match;
using YordleYelper.bot.http_client;
using YordleYelper.database;

namespace YordleYelper.bot.data_fetcher.league_api; 

public class LeagueApiProxy {
    private const string API_BASE = "https://europe.api.riotgames.com";
    private const string REGION_API_BASE = "https://euw1.api.riotgames.com";

    private readonly HttpClient _httpClient;
    
    public LeagueApiProxy(string authToken) {
        _httpClient = HttpClient.LeagueApiHttpClient(authToken);
    }
    
    public LeagueAccountResponse GetLeagueAccountByPuuid(Puuid puuid) {
        return _httpClient.Get<LeagueAccountResponse>($"{API_BASE}/riot/account/v1/accounts/by-puuid/{puuid}").Result;
    }

    public bool TryGetLeagueAccount(string riotId, out LeagueAccount leagueAccount) {
        if (!TryGetPuuidByRiotId(riotId, out Puuid puuid)) {
            leagueAccount = default;
            return false;
        }

        LeagueAccountResponse leagueAccountResponse = GetLeagueAccountByPuuid(puuid);
        leagueAccount = new LeagueAccount(leagueAccountResponse, GetSummonerByPuuid(puuid));
        return true;
    }

    public async Task<ChampionMasteryResponse> GetChampionMastery(LeagueAccount leagueAccount, BasicChampionInfo basicInfo) {
        return await _httpClient.Get<ChampionMasteryResponse>($"{REGION_API_BASE}/lol/champion-mastery/v4/champion-masteries/by-puuid/{leagueAccount.puuid}/by-champion/{basicInfo.Key}");
    }

    public async Task<List<ChampionMasteryResponse>> GetChampionMasteries(LeagueAccount leagueAccount) {
        return await _httpClient.Get<List<ChampionMasteryResponse>>($"{REGION_API_BASE}/lol/champion-mastery/v4/champion-masteries/by-puuid/{leagueAccount.puuid}");
    }
    
    public List<string> FetchMatchesByPuuid(Puuid puuid, int startIndex) {
        return _httpClient.Get<List<string>>($"{API_BASE}/lol/match/v5/matches/by-puuid/{puuid}/ids?start={startIndex}&count=100").Result;
    }
    
    public MatchDataResponse FetchMatchData(string matchId) {
        return _httpClient.Get<MatchDataResponse>($"{API_BASE}/lol/match/v5/matches/{matchId}").Result;
    }
    
    private bool TryGetPuuidByRiotId(string riotId, out Puuid puuid) {
        try {
            Dictionary<string, string> data = _httpClient.Get<Dictionary<string, string>>($"{API_BASE}/riot/account/v1/accounts/by-riot-id/{riotId}/EUW").Result;
            puuid = new Puuid(data["puuid"]);
            return true;
        }
        catch (AggregateException exception) {
            Exception innerException = exception.InnerExceptions.First();
            if (innerException is HttpStatusException httpStatusException && httpStatusException.statusCode != HttpStatusCode.NotFound) {
                throw;
            }
            puuid = default;
            return false;
        }
    }
    
    private Summoner GetSummonerByPuuid(Puuid puuid) {
        return _httpClient.Get<Summoner>($"{REGION_API_BASE}/lol/summoner/v4/summoners/by-puuid/{puuid}").Result;
    }
}