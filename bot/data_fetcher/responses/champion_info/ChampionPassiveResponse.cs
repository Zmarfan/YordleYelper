using Newtonsoft.Json;

namespace YordleYelper.bot.data_fetcher.responses.champion_info; 

public struct ChampionPassiveResponse {
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("description")]
    public string Description { get; set; }
}