using System.Collections.Generic;
using Newtonsoft.Json;

namespace YordleYelper.bot.data_fetcher.responses.champion_info; 

public struct ChampionPassiveResponse {
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("description")]
    public string Description { get; set; }
    
    [JsonProperty("image")] public Dictionary<string, string> ImageProperties { get; set; }

    public string ImageName => ImageProperties["full"];
}