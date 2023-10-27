using System.Collections.Generic;
using System.Linq;
using YordleYelper.bot.data_fetcher.responses;
using YordleYelper.bot.data_fetcher.responses.champion_info;
using YordleYelper.bot.extensions;
using YordleYelper.bot.http_client;

namespace YordleYelper.bot.data_fetcher; 

public class DataDragonProxy {
    public const string CONTENT_BASE = "https://ddragon.leagueoflegends.com/";
    private const string API = $"{CONTENT_BASE}api/";
    private string Data => $"{CONTENT_BASE}cdn/{_version}/data/en_US/";

    private readonly string _version;
    private readonly List<BasicChampionInfo> _championBasicInfos;
    private readonly HttpClient _httpClient;
    
    public DataDragonProxy(HttpClient httpClient) {
        _httpClient = httpClient;
        _version = GetCurrentVersion();
        AllChampionsResponse response = _httpClient.Get<AllChampionsResponse>($"{Data}champion.json").Result;
        _championBasicInfos = response.Data.Values.ToList();
    }

    public bool TryGetBasicChampionInfo(string championName, out BasicChampionInfo championInfo) {
        return _championBasicInfos.TryGetSimilarEntry(championName, entry => entry.Name, out championInfo);
    }

    public TopChampionInfoResponse GetChampionInfo(BasicChampionInfo basicInfo) {
        return _httpClient.Get<TopChampionInfoResponse>($"{Data}champion/{basicInfo.Id}.json").Result;
    }

    private string GetCurrentVersion() {
        List<string> versions = _httpClient.Get<List<string>>($"{API}versions.json").Result;
        return versions.First();
    }
}