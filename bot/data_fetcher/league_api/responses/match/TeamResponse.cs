using System.Collections.Generic;
using Newtonsoft.Json;

namespace YordleYelper.bot.data_fetcher.league_api.responses.match; 

public struct TeamResponse {
    public readonly bool leftTeam;
    public readonly List<BanResponse> bans;
    public readonly bool win;

    [JsonConstructor]
    public TeamResponse(
        [JsonProperty("teamId")] int teamId,
        [JsonProperty("bans")] List<BanResponse> bans,
        [JsonProperty("win")] bool win
    ) {
        leftTeam = teamId == 100;
        this.bans = bans;
        this.win = win;
    }
}