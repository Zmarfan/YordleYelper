using Newtonsoft.Json;
using YordleYelper.bot.data_fetcher.league_api.data;

namespace YordleYelper.bot.data_fetcher.league_api.responses; 

public struct LeagueAccount {
    public readonly Puuid puuid;
    public readonly string gameName;
    public readonly string tagLine;

    [JsonConstructor]
    public LeagueAccount(
        [JsonProperty("puuid")] string puuid,
        [JsonProperty("gameName")] string gameName,
        [JsonProperty("tagLine")] string tagLine
    ) {
        this.puuid = new Puuid(puuid);
        this.gameName = gameName;
        this.tagLine = tagLine;
    }
}