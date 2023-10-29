using System.Collections.Generic;
using Newtonsoft.Json;

namespace YordleYelper.bot.data_fetcher.data_dragon.responses; 

public struct AllChampionsResponse {
    [JsonProperty("data")]
    public Dictionary<string, BasicChampionInfo> Data { get; set; }
}

public struct BasicChampionInfo {
    [JsonProperty("version")]
    private string Version { get; set; }
    
    [JsonProperty("id")]
    public string Id { get; set; }
    
    [JsonProperty("key")]
    public string Key { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("title")]
    public string Title { get; set; }
    
    [JsonProperty("blurb")]
    public string Blurb { get; set; }
    
    public string PortraitImageUrl => $"{DataDragonProxy.CONTENT_BASE}cdn/{Version}/img/champion/{Id}.png";
}