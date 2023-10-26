using System;
using System.Collections.Generic;
using System.Linq;
using YordleYelper.bot.http_client;

namespace YordleYelper.bot.data_fetcher; 

public class DataDragonEndpoints {
    private const string CONTENT_BASE = "https://ddrgon.leagueoflegends.com/";
    
    public bool IsValidated { get; private set; }
    
    public string AllChampionsEndpoint { get; private set; }

    public void Validate() {
        string version = GetCurrentVersion();
        AllChampionsEndpoint = $"{CONTENT_BASE}cdn/{version}/data/en_US/champion.json";
        
        IsValidated = true;
    }
    
    private static string GetCurrentVersion() {
        (bool, List<string>) result = HttpClient.TryGet<List<string>>(CONTENT_BASE + "api/versions.json").Result;
        if (!result.Item1) {
            throw new ApplicationException("Unable to fetch data dragon version!");
        }

        return result.Item2.First();
    }
}