using System.Collections.Generic;
using System.Linq;
using YordleYelper.bot.data_fetcher.responses;
using YordleYelper.bot.http_client;

namespace YordleYelper.bot.data_fetcher; 

public class DataFetcher {
    private const string API_BASE = "https://euw1.api.riotgames.com";

    private readonly DataDragonEndpoints _dataDragonEndpoints;
    
    private readonly Dictionary<string, ChampionInfo> _champions;
    
    public DataFetcher() {
        _dataDragonEndpoints = new DataDragonEndpoints();
        _champions = HttpClient
            .Get<AllChampionsResponse>(_dataDragonEndpoints.allChampionsEndpoint)
            .Result.Data.Values.ToDictionary(c => c.Name.ToLower(), c => c);
    }
    
    public ChampionInfo? GetChampion(string championName) {
        return _champions.TryGetValue(championName.ToLower(), out ChampionInfo champion) ? champion : null;
    }
}