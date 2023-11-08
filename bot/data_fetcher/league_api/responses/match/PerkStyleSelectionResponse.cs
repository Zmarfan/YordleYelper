using Newtonsoft.Json;

namespace YordleYelper.bot.data_fetcher.league_api.responses.match; 

public struct PerkStyleSelectionResponse {
    [JsonProperty("perk")]
    public int Perk { get; set; }
    
    [JsonProperty("var1")]
    public int Var1 { get; set; }
    
    [JsonProperty("var2")]
    public int Var2 { get; set; }
    
    [JsonProperty("var3")]
    public int Var3 { get; set; }
}