using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using YordleYelper.bot.data_fetcher.data_dragon.responses;
using YordleYelper.bot.data_fetcher.data_dragon.responses.champion_info;
using YordleYelper.bot.data_fetcher.data_dragon.responses.items;
using YordleYelper.bot.extensions;
using YordleYelper.bot.http_client;

namespace YordleYelper.bot.data_fetcher.data_dragon; 

public class DataDragonProxy {
    private const string CONTENT_BASE = "https://ddragon.leagueoflegends.com/";
    private readonly string _dataUrl;

    private readonly List<BasicChampionInfo> _championBasicInfos;
    private readonly Dictionary<string, ItemInfo> _itemInfos;
    private readonly HttpClient _httpClient;

    public List<BasicChampionInfo> AllChampionBasicInfos => _championBasicInfos.ToList();
    
    public DataDragonProxy(ILogger logger) {
        _dataUrl = $"{CONTENT_BASE}cdn/{VersionHolder.Version}/data/en_US/";
        _httpClient = new HttpClient(logger);
        _championBasicInfos = _httpClient.Get<AllChampionsResponse>($"{_dataUrl}champion.json").Result.Data.Values.ToList();
        _itemInfos = _httpClient.Get<AllItemsResponse>($"{_dataUrl}item.json").Result.Items
            .Where(entry => entry.Value.Description != string.Empty)
            .Where(entry => entry.Value.Maps.Take(2).Any(map => map.Value))
            .ToDictionary(entry => entry.Key, entry => new ItemInfo {
                id = entry.Key,
                response = entry.Value,
                iconUrl = LeagueImageUrlGenerator.ItemImageUrl(entry.Value.ImageName)
            });
    }

    public bool TryGetBasicChampionInfo(string championName, out BasicChampionInfo championInfo) {
        return _championBasicInfos.TryGetSimilarEntry(championName, entry => entry.Name, out championInfo);
    }

    public TopChampionInfoResponse GetChampionInfo(BasicChampionInfo basicInfo) {
        return _httpClient.Get<TopChampionInfoResponse>($"{_dataUrl}champion/{basicInfo.Id}.json").Result;
    }
    
    public bool TryGetItemInfo(string itemName, out ItemInfo itemInfo) {
        return _itemInfos.Values.TryGetSimilarEntry(itemName, info => info.response.Name, out itemInfo);
    }
    
    public IEnumerable<string> ItemNamesFromIds(IEnumerable<string> itemIds) {
        return itemIds.Where(id => _itemInfos.ContainsKey(id)).Select(id => _itemInfos[id].response.Name);
    }
}