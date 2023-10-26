using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace YordleYelper.bot.data_fetcher.responses.champion_info; 

public struct TopChampionInfoResponse {
    [JsonProperty("version")]
    private string Version { get; set; }
    
    [JsonProperty("data")]
    private Dictionary<string, ChampionInfoResponse> DataDictionary { get; set; }

    public ChampionInfoResponse Data => DataDictionary.First().Value;
    public string PortraitImageUrl => $"{DataDragonProxy.CONTENT_BASE}cdn/{Version}/img/champion/{Data.Id}.png";
}