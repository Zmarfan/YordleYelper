using System.Collections.Generic;
using System.Linq;
using YordleYelper.bot.http_client;

namespace YordleYelper.bot.data_fetcher; 

public class DataDragonEndpoints {
    private const string CONTENT_BASE = "https://ddragon.leagueoflegends.com/";
    
    public readonly string allChampionsEndpoint;

    public DataDragonEndpoints() {
        string version = GetCurrentVersion();
        allChampionsEndpoint = $"{CONTENT_BASE}cdn/{version}/data/en_US/champion.json";
    }
    
    private static string GetCurrentVersion() {
        return HttpClient.Get<List<string>>(CONTENT_BASE + "api/versions.json").Result.First();
    }
}