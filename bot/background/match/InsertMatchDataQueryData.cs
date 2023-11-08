using System;
using YordleYelper.bot.data_fetcher.league_api.responses.match;
using YordleYelper.database;
using YordleYelper.database.attributes;

namespace YordleYelper.bot.background.match; 

public class InsertMatchDataQueryData : IQueryData<VoidRecord> {
    [QueryParameter("p_match_id")] public readonly string matchId;
    [QueryParameter("p_game_creation_timestamp")] public readonly DateTime gameCreation;
    [QueryParameter("p_game_start_timestamp")] public readonly DateTime gameStartTimestamp;
    [QueryParameter("p_game_end_timestamp")] public readonly DateTime gameEndTimestamp;
    [QueryParameter("p_game_duration")] public readonly long gameDuration;
    [QueryParameter("p_game_mode")] public readonly string gameMode;
    [QueryParameter("p_game_type")] public readonly string gameType;
    [QueryParameter("p_map_id")] public readonly int mapId;
    
    public string GetStoredProcedureName => "insert_match_data";

    public InsertMatchDataQueryData(MatchDataResponse match) {
        matchId = match.MetaData.MatchId;
        gameCreation = match.Info.gameCreation;
        gameStartTimestamp = match.Info.gameStartTimestamp;
        gameEndTimestamp = match.Info.gameEndTimestamp;
        gameDuration = match.Info.gameDuration;
        gameMode = match.Info.gameMode.code;
        gameType = match.Info.gameType.code;
        mapId = match.Info.mapId.code;
    }
}