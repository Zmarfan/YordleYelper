using System;
using Newtonsoft.Json;
using YordleYelper.bot.data_fetcher.league_api.data;

namespace YordleYelper.bot.data_fetcher.league_api.responses; 

public struct ChampionMasteryResponse {
    public readonly Puuid puuid;
    public readonly string championId;
    public readonly int championLevel;
    public readonly int championPoints;
    public readonly DateTimeOffset lastPlayed;
    public readonly int championPointsSinceLastLevel;
    public readonly int championPointsUntilNextLevel;
    public readonly bool chestGranted;
    public readonly int tokensEarned;
    public readonly SummonerId summonerId;

    [JsonConstructor]
    public ChampionMasteryResponse(
        [JsonProperty("puuid")] string puuid,
        [JsonProperty("championId")] string championId,
        [JsonProperty("championLevel")] int championLevel,
        [JsonProperty("championPoints")] int championPoints,
        [JsonProperty("lastPlayTime")] long lastPlayTime,
        [JsonProperty("championPointsSinceLastLevel")] int championPointsSinceLastLevel,
        [JsonProperty("championPointsUntilNextLevel")] int championPointsUntilNextLevel,
        [JsonProperty("chestGranted")] bool chestGranted,
        [JsonProperty("tokensEarned")] int tokensEarned,
        [JsonProperty("summonerId")] string summonerId
    ) {
        this.puuid = new Puuid(puuid);
        this.championId = championId;
        this.championLevel = championLevel;
        this.championPoints = championPoints;
        lastPlayed = DateTimeOffset.FromUnixTimeMilliseconds(lastPlayTime);
        this.championPointsSinceLastLevel = championPointsSinceLastLevel;
        this.championPointsUntilNextLevel = championPointsUntilNextLevel;
        this.chestGranted = chestGranted;
        this.tokensEarned = tokensEarned;
        this.summonerId = new SummonerId(summonerId);
    }
}