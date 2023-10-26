using System.Collections.Generic;
using Newtonsoft.Json;

namespace YordleYelper.bot.data_fetcher.responses.champion_info; 

public struct ChampionInfoResponse {
    [JsonProperty("id")]
    public string Id { get; set; }
    
    [JsonProperty("key")]
    public int Key { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("title")]
    public string Title { get; set; }
    
    [JsonProperty("lore")]
    public string Lore { get; set; }

    [JsonProperty("blurb")]
    public string Blurb { get; set; }
    
    [JsonProperty("allytips")]
    public List<string> AllyTips { get; set; }
    
    [JsonProperty("enemytips")]
    public List<string> EnemyTips { get; set; }
    
    [JsonProperty("tags")]
    public List<string> Tags { get; set; }
    
    [JsonProperty("partype")]
    public string ParType { get; set; }

    [JsonProperty("spells")] 
    public List<ChampionSpellsResponse> Spells { get; set; }
    
    [JsonProperty("passive")] 
    public ChampionPassiveResponse Passive { get; set; }
}