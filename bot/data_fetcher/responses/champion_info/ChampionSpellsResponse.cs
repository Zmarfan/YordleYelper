using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace YordleYelper.bot.data_fetcher.responses.champion_info; 

public struct ChampionSpellsResponse {
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("tooltip")]
    public string Tooltip { get; set; }
    
    [JsonProperty("cooldownBurn")]
    public string Cooldowns { get; set; }
    
    [JsonProperty("cost")]
    public List<int> Cost { get; set; }

    [JsonProperty("maxammo")]
    public int MaxAmmo { get; set; }

    [JsonProperty("image")] public Dictionary<string, string> ImageProperties { get; set; }
    
    public string ImageName => ImageProperties["full"];
}