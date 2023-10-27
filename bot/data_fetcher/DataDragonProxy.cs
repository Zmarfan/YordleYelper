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

    private static bool _isValidated;

    private static string _version;
    private static readonly List<BasicChampionInfo> CHAMPION_BASIC_INFOS = new();

    public static bool TryGetBasicChampionInfo(string championName, out BasicChampionInfo championInfo) {
        Validate();
        
        List<BasicChampionInfo> matches = CHAMPION_BASIC_INFOS.FindAll(info => string.Equals(info.Name, championName, StringComparison.CurrentCultureIgnoreCase));
        championInfo = matches.Any() ? matches.First() : default;
        return matches.Any();
    }
    
    public static (BasicChampionInfo, int) GetMostSimilarChampionBasicInfo(string championName) {
        Validate();
        return WordSimilarityChecker.FindMostSimilarEntry(championName, CHAMPION_BASIC_INFOS, champion => champion.Name);
    }
    
    public static TopChampionInfoResponse GetChampionInfo(BasicChampionInfo basicInfo) {
        Validate();
        return HttpClient.Get<TopChampionInfoResponse>($"{CONTENT_BASE}cdn/{_version}/data/en_US/champion/{basicInfo.Id}.json").Result;
    }
    
    private static void Validate() {
        if (_isValidated) {
            return;
        }
        
        _version = GetCurrentVersion();
        AllChampionsResponse response = HttpClient.Get<AllChampionsResponse>($"{CONTENT_BASE}cdn/{_version}/data/en_US/champion.json").Result;
        CHAMPION_BASIC_INFOS.AddRange(response.Data.Values);
        
        _isValidated = true;
    }
    
    private static string GetCurrentVersion() {
        List<string> versions = HttpClient.Get<List<string>>(CONTENT_BASE + "api/versions.json").Result;
        return versions.First();
    }
}