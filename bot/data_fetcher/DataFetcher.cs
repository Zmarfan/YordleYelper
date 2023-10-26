using System;
using System.Collections.Generic;
using System.Linq;
using YordleYelper.bot.data_fetcher.responses;
using YordleYelper.bot.http_client;

namespace YordleYelper.bot.data_fetcher; 

public class DataFetcher {
    private const string API_BASE = "https://euw1.api.riotgames.com";

    private readonly DataDragonEndpoints _dataDragonEndpoints = new();
    private Dictionary<string, ChampionInfo> _champions;
    
    public ChampionInfo? GetChampion(string championName) {
        ValidateEndpoints();
        return _champions.TryGetValue(championName.ToLower(), out ChampionInfo champion) ? champion : null;
    }

    private void ValidateEndpoints() {
        if (_dataDragonEndpoints.IsValidated) {
            return;
        }
        _dataDragonEndpoints.Validate();
        (bool, AllChampionsResponse) result = HttpClient.TryGet<AllChampionsResponse>(
            _dataDragonEndpoints.AllChampionsEndpoint
        ).Result;

        if (!result.Item1) {
            throw new ApplicationException("Unable to fetch champions from data dragon to validate command input!");
        }
        
        _champions = result.Item2.Data.Values.ToDictionary(c => c.Name.ToLower(), c => c);
    }
}