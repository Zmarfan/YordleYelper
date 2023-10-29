using System.Collections.Generic;
using Newtonsoft.Json;

namespace YordleYelper.bot.data_fetcher.data_dragon.responses.items; 

public struct AllItemsResponse {
    [JsonProperty("data")] 
    public Dictionary<string, ItemResponse> Items { get; set; }
}