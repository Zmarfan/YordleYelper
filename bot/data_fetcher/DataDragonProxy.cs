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
    private const string API = $"{CONTENT_BASE}api/";
    private string Data => $"{CONTENT_BASE}cdn/{_version}/data/en_US/";

    private readonly string _version;
    private readonly List<BasicChampionInfo> _championBasicInfos;

    public DataDragonProxy() {
        _version = GetCurrentVersion();
        AllChampionsResponse response = HttpClient.Get<AllChampionsResponse>($"{Data}champion.json").Result;
        _championBasicInfos = response.Data.Values.ToList();
    }
    
    public bool TryGetBasicChampionInfo(string championName, out BasicChampionInfo championInfo) {
        championInfo = default;
        List<BasicChampionInfo> matches = _championBasicInfos.FindAll(info => string.Equals(info.Name, championName, StringComparison.CurrentCultureIgnoreCase));
        if (matches.Any()) {
            championInfo = matches.First();
            return true;
        }
        (BasicChampionInfo, int) similarChampion = WordSimilarityChecker.FindMostSimilarEntry(championName, _championBasicInfos, champion => champion.Name);
        if (similarChampion.Item2 <= 3) {
            championInfo = similarChampion.Item1;
            return true;
        }

        return false;
    }

    public TopChampionInfoResponse GetChampionInfo(BasicChampionInfo basicInfo) {
        return HttpClient.Get<TopChampionInfoResponse>($"{Data}champion/{basicInfo.Id}.json").Result;
    }

    private static string GetCurrentVersion() {
        List<string> versions = HttpClient.Get<List<string>>($"{API}versions.json").Result;
        return versions.First();
    }
}