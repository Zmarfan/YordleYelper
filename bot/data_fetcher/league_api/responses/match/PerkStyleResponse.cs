using System.Collections.Generic;
using Newtonsoft.Json;

namespace YordleYelper.bot.data_fetcher.league_api.responses.match; 

public struct PerkStyleResponse {
    [JsonProperty("style")]
    public int Style { get; set; }
    
    [JsonProperty("selections")]
    public List<PerkStyleSelectionResponse> Selections { get; set; }
}