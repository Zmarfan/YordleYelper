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

    private bool _isValidated;
    
    private readonly List<BasicChampionInfo> _championBasicInfos = new();
    private string _getChampionInfoUrl;

    public bool TryGetBasicChampionInfo(string championName, out BasicChampionInfo championInfo) {
        Validate();
        
        List<BasicChampionInfo> matches = _championBasicInfos.FindAll(info => string.Equals(info.Name, championName, StringComparison.CurrentCultureIgnoreCase));
        championInfo = matches.Any() ? matches.First() : default;
        return matches.Any();
    }
    
    public BasicChampionInfo GetMostSimilarChampionBasicInfo(string championName) {
        Validate();
        return WordSimilarityChecker.FindMostSimilarEntry(championName, _championBasicInfos, champion => champion.Name);
    }
    
    public TopChampionInfoResponse GetChampionInfo(BasicChampionInfo basicInfo) {
        return HttpClient.Get<TopChampionInfoResponse>($"{_getChampionInfoUrl}{basicInfo.Id}.json").Result;
    }
    
    private void Validate() {
        if (_isValidated) {
            return;
        }
        
        string version = GetCurrentVersion();
        AllChampionsResponse response = HttpClient.Get<AllChampionsResponse>($"{CONTENT_BASE}cdn/{version}/data/en_US/champion.json").Result;
        _championBasicInfos.AddRange(response.Data.Values);

        _getChampionInfoUrl = $"{CONTENT_BASE}cdn/{version}/data/en_US/champion/";
        
        _isValidated = true;
    }
    
    private static string GetCurrentVersion() {
        List<string> versions = HttpClient.Get<List<string>>(CONTENT_BASE + "api/versions.json").Result;
        return versions.First();
    }
}