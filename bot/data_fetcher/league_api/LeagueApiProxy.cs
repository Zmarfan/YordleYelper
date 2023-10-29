using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Logging;
using YordleYelper.bot.data_fetcher.data_dragon.responses;
using YordleYelper.bot.data_fetcher.league_api.data;
using YordleYelper.bot.data_fetcher.league_api.responses;
using YordleYelper.bot.http_client;

namespace YordleYelper.bot.data_fetcher.league_api; 

public class LeagueApiProxy {
    private const string API_BASE = "https://europe.api.riotgames.com";
    private const string REGION_API_BASE = "https://euw1.api.riotgames.com";

    private readonly HttpClient _httpClient;
    
    public LeagueApiProxy(ILogger logger, string authToken) {
        _httpClient = HttpClient.LeagueApiHttpClient(logger, authToken);
    }
    
    public LeagueAccount GetLeagueAccountByPuuid(Puuid puuid) {
        return _httpClient.Get<LeagueAccount>($"{API_BASE}/riot/account/v1/accounts/by-puuid/{puuid}").Result;
    }

    public bool TryGetPuuidByRiotId(string riotId, out Puuid puuid) {
        try {
            Dictionary<string, string> data = _httpClient
                .Get<Dictionary<string, string>>($"{API_BASE}/riot/account/v1/accounts/by-riot-id/{riotId}/EUW").Result;
            puuid = new Puuid(data["puuid"]);
            return true;
        }
        catch (HttpException e) {
            if (e.GetHttpCode() != 404) {
                throw;
            }
            puuid = default;
            return false;
        }
    }

    public async Task<ChampionMasteryResponse> GetChampionMastery(LeagueAccount leagueAccount, BasicChampionInfo basicInfo) {
        return await _httpClient.Get<ChampionMasteryResponse>($"{REGION_API_BASE}/lol/champion-mastery/v4/champion-masteries/by-puuid/{leagueAccount.puuid}/by-champion/{basicInfo.Key}");
    }
}