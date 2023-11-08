using System.Collections.Generic;
using Newtonsoft.Json;
using YordleYelper.bot.data;

namespace YordleYelper.bot.data_fetcher.league_api.responses.match; 

public struct PerksResponse {
    [JsonProperty("statPerks")]
    public Dictionary<string, int> StatPerks { get; set; }
    
    [JsonProperty("styles")]
    public List<PerkStyleResponse> Styles { get; set; }

    public StatPerk Defense => StatPerk.FromCode(StatPerks["defense"]);
    public StatPerk Flex => StatPerk.FromCode(StatPerks["flex"]);
    public StatPerk Offense => StatPerk.FromCode(StatPerks["offense"]);

    public PerkStyleResponse Primary => Styles[0];
    public PerkStyleResponse Secondary => Styles[1];
}