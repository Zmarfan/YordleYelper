﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace YordleYelper.bot.data_fetcher.data_dragon.responses.items; 

public struct ItemResponse {
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("description")]
    public string Description { get; set; }
    
    [JsonProperty("plaintext")]
    public string Plaintext { get; set; }
    
    [JsonProperty("from")]
    public List<string> FromItemsIds { get; set; }
    
    [JsonProperty("into")]
    public List<string> IntoItemsIds { get; set; }

    [JsonProperty("requiredAlly")]
    public string RequiredAlly { get; set; }
    
    [JsonProperty("requiredChampion")]
    public string RequiredChampion { get; set; }
    
    [JsonProperty("maps")]
    public Dictionary<string, bool> Maps { get; set; }
    
    [JsonProperty("gold")]
    public ItemGoldResponse GoldResponse { get; set; }
    
    [JsonProperty("image")] public Dictionary<string, string> ImageProperties { get; set; }

    public string ImageName => ImageProperties["full"];
}