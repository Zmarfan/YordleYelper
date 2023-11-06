using Newtonsoft.Json;

namespace YordleYelper.bot.data_fetcher.league_api.responses.match; 

public struct MatchDataResponse {
    [JsonProperty("metadata")]
    public MatchDataMetaDataResponse MetaData { get; set; }
    
    [JsonProperty("info")]
    public MatchDataInfoResponse Info { get; set; }
}