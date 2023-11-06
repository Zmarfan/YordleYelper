using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YordleYelper.bot.data;

namespace YordleYelper.bot.data_fetcher.league_api.responses.match; 

public struct MatchDataInfoResponse {
    public readonly DateTime gameCreation;
    public readonly DateTime gameStartTimestamp;
    public readonly DateTime gameEndTimestamp;
    public readonly long gameDuration;
    public readonly GameMode gameMode;
    public readonly GameType gameType;
    public readonly Map mapId;
    public readonly List<ParticipantResponse> participants;

    [JsonConstructor]
    public MatchDataInfoResponse(
        [JsonProperty("gameCreation")] long gameCreation,
        [JsonProperty("gameStartTimestamp")] long gameStartTimestamp,
        [JsonProperty("gameEndTimestamp")] long gameEndTimestamp,
        [JsonProperty("gameDuration")] long gameDuration,
        [JsonProperty("gameMode")] string gameMode,
        [JsonProperty("gameType")] string gameType,
        [JsonProperty("mapId")] int mapId,
        [JsonProperty("participants")] List<ParticipantResponse> participants
    ) {
        this.gameCreation = DateTimeOffset.FromUnixTimeMilliseconds(gameCreation).DateTime;
        this.gameStartTimestamp = DateTimeOffset.FromUnixTimeMilliseconds(gameStartTimestamp).DateTime;
        this.gameEndTimestamp = DateTimeOffset.FromUnixTimeMilliseconds(gameEndTimestamp).DateTime;
        this.gameDuration = gameDuration;
        this.gameMode = GameMode.FromCode(gameMode);
        this.gameType = GameType.FromCode(gameType);
        this.mapId = Map.FromCode(mapId);
        this.participants = participants;
    }
}