using System.Collections.Generic;
using Newtonsoft.Json;

namespace YordleYelper.bot.data_fetcher.responses.champion_info; 

public struct ChampionSpellsResponse {
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("cooldown")]
    public List<float> Cooldowns { get; set; }
    
    [JsonProperty("cost")]
    public List<int> Cost { get; set; }

    [JsonProperty("maxammo")]
    public int MaxAmmo { get; set; }
}