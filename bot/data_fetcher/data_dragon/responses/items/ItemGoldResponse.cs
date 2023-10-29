using Newtonsoft.Json;

namespace YordleYelper.bot.data_fetcher.data_dragon.responses.items; 

public struct ItemGoldResponse {
    [JsonProperty("base")]
    public int Base { get; set; }
    
    [JsonProperty("purchasable")]
    public bool Purchasable { get; set; }
    
    [JsonProperty("total")]
    public int Total { get; set; }
    
    [JsonProperty("sell")]
    public int Sell { get; set; }
}