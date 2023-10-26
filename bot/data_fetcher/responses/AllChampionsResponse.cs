using System.Collections.Generic;
using Newtonsoft.Json;

namespace YordleYelper.bot.data_fetcher.responses; 

public struct AllChampionsResponse {
    [JsonProperty("data")]
    public Dictionary<string, ChampionInfo> Data { get; set; }
}

public struct ChampionInfo {
    [JsonProperty("id")]
    public string Id { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("title")]
    public string Title { get; set; }
    
    [JsonProperty("blurb")]
    public string Blurb { get; set; }
}