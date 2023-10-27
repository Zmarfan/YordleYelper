using System;
using System.Collections.Generic;
using System.Linq;
using YordleYelper.bot.data_fetcher.responses;
using YordleYelper.bot.data_fetcher.responses.champion_info;
using YordleYelper.bot.data_fetcher.word_similarity;
using YordleYelper.bot.http_client;

namespace YordleYelper.bot.data_fetcher; 

public class DataDragonProxy {
    public const string CONTENT_BASE = "https://ddragon.leagueoflegends.com/";

    private readonly string _version;
    private readonly List<BasicChampionInfo> _championBasicInfos = new();

    public DataDragonProxy() {
        _version = GetCurrentVersion();
        AllChampionsResponse response = HttpClient.Get<AllChampionsResponse>($"{CONTENT_BASE}cdn/{_version}/data/en_US/champion.json").Result;
        _championBasicInfos = response.Data.Values.ToList();
    }
    
    public bool TryGetBasicChampionInfo(string championName, out BasicChampionInfo championInfo) {
        List<BasicChampionInfo> matches = _championBasicInfos.FindAll(info => string.Equals(info.Name, championName, StringComparison.CurrentCultureIgnoreCase));
        championInfo = matches.Any() ? matches.First() : default;
        return matches.Any();
    }
    
    public (BasicChampionInfo, int) GetMostSimilarChampionBasicInfo(string championName) {
        return WordSimilarityChecker.FindMostSimilarEntry(championName, _championBasicInfos, champion => champion.Name);
    }
    
    public TopChampionInfoResponse GetChampionInfo(BasicChampionInfo basicInfo) {
        return HttpClient.Get<TopChampionInfoResponse>($"{CONTENT_BASE}cdn/{_version}/data/en_US/champion/{basicInfo.Id}.json").Result;
    }

    private static string GetCurrentVersion() {
        List<string> versions = HttpClient.Get<List<string>>(CONTENT_BASE + "api/versions.json").Result;
        return versions.First();
    }
}