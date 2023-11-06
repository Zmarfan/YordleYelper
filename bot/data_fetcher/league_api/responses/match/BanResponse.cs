using Newtonsoft.Json;

namespace YordleYelper.bot.data_fetcher.league_api.responses.match; 

public struct BanResponse {
    [JsonProperty("championId")]
    public int ChampionId { get; set; }
    
    [JsonProperty("pickTurn")]
    public int PickTurn { get; set; }
}