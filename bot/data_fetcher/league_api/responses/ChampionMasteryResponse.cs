using Newtonsoft.Json;
using YordleYelper.bot.data_fetcher.league_api.data;

namespace YordleYelper.bot.data_fetcher.league_api.responses; 

public struct ChampionMasteryResponse {
    public readonly Puuid puuid;

    [JsonConstructor]
    public ChampionMasteryResponse(
        [JsonProperty("puuid")] string puuid
    ) {
        this.puuid = new Puuid(puuid);
    }
}