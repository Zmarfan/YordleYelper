using Newtonsoft.Json;

namespace YordleYelper.bot.data_fetcher.league_api.responses.match; 

public struct MatchDataMetaDataResponse {
    [JsonProperty("matchId")]
    public string MatchId { get; set; }
}