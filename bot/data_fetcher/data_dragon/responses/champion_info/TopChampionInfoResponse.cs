using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using YordleYelper.bot.commands.choices;
using YordleYelper.bot.extensions;

namespace YordleYelper.bot.data_fetcher.data_dragon.responses.champion_info; 

public struct TopChampionInfoResponse {
    private static readonly Dictionary<ChampionAbility, (int, string)> ABILITY_DATA = new() {
        { ChampionAbility.Q, (0, "Q") },
        { ChampionAbility.W, (1, "W") },
        { ChampionAbility.E, (2, "E") },
        { ChampionAbility.R, (3, "R") },
    };

    [JsonProperty("version")]
    private string Version { get; set; }
    
    [JsonProperty("data")]
    private Dictionary<string, ChampionInfoResponse> DataDictionary { get; set; }

    public ChampionInfoResponse Data => DataDictionary.First().Value;
    public string PortraitImageUrl => $"{DataDragonProxy.CONTENT_BASE}cdn/{Version}/img/champion/{Data.Id}.png";

    public PassiveInfo Passive => new() {
        response = Data.Passive,
        spellIconUrl = $"{DataDragonProxy.CONTENT_BASE}cdn/{Version}/img/passive/{Data.Passive.ImageName}"
    };

    public AbilityInfo GetAbility(ChampionAbility ability) {
        (int, string) data = ABILITY_DATA[ability];
        return new AbilityInfo {
            response = Data.Spells[data.Item1],
            spellIconUrl = $"{BaseSpellImageUrl}{Data.Spells[data.Item1].ImageName}",
            spellUsageGifUrl = $"https://baron-discord-bot-assets.s3.eu-central-1.amazonaws.com/abilities-gifs/{Data.Key}-{data.Item2}.gif",
            spellEmoji = Emote.FromAbility(ability)
        };
    }
    
    private string BaseSpellImageUrl => $"{DataDragonProxy.CONTENT_BASE}cdn/{Version}/img/spell/";
}