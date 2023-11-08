using Newtonsoft.Json;

namespace YordleYelper.bot.data_fetcher.league_api.responses.match; 

public struct ObjectiveResponse {
    [JsonProperty("first")]
    public bool First { get; set; }
    
    [JsonProperty("kills")]
    public int Kills { get; set; }
}