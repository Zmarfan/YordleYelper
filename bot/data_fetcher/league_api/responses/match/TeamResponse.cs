using System.Collections.Generic;
using Newtonsoft.Json;

namespace YordleYelper.bot.data_fetcher.league_api.responses.match; 

public struct TeamResponse {
    public readonly bool leftTeam;
    public readonly List<BanResponse> bans;
    public readonly bool win;

    public ObjectiveResponse Baron => _objectives["baron"];
    public ObjectiveResponse Champion => _objectives["champion"];
    public ObjectiveResponse Dragon => _objectives["dragon"];
    public ObjectiveResponse Inhibitor => _objectives["inhibitor"];
    public ObjectiveResponse RiftHerald => _objectives["riftHerald"];
    public ObjectiveResponse Tower => _objectives["tower"];
    
    private readonly Dictionary<string, ObjectiveResponse> _objectives;

    [JsonConstructor]
    public TeamResponse(
        [JsonProperty("teamId")] int teamId,
        [JsonProperty("bans")] List<BanResponse> bans,
        [JsonProperty("win")] bool win,
        [JsonProperty("objectives")] Dictionary<string, ObjectiveResponse> objectives
    ) {
        leftTeam = teamId == 100;
        this.bans = bans;
        this.win = win;
        _objectives = objectives;
    }
}