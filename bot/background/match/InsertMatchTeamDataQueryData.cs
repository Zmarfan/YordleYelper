using YordleYelper.bot.data_fetcher.league_api.responses.match;
using YordleYelper.bot.extensions;
using YordleYelper.database;
using YordleYelper.database.attributes;

namespace YordleYelper.bot.background.match; 

public class InsertMatchTeamDataQueryData : IQueryData<VoidRecord> {
    [QueryParameter("p_match_id")] public readonly string matchId;
    [QueryParameter("p_left_team")] public readonly bool leftTeam;
    [QueryParameter("p_win")] public readonly bool win;
    [QueryParameter("p_champion_id_ban_1")] public readonly int championIdBan1;
    [QueryParameter("p_champion_id_ban_2")] public readonly int championIdBan2;
    [QueryParameter("p_champion_id_ban_3")] public readonly int championIdBan3;
    [QueryParameter("p_champion_id_ban_4")] public readonly int championIdBan4;
    [QueryParameter("p_champion_id_ban_5")] public readonly int championIdBan5;
    [QueryParameter("p_first_to_take_baron")] public readonly bool firstToTakeBaron;
    [QueryParameter("p_first_to_take_champion")] public readonly bool firstToTakeChampion;
    [QueryParameter("p_first_to_take_dragon")] public readonly bool firstToTakeDragon;
    [QueryParameter("p_first_to_take_inhibitor")] public readonly bool firstToTakeInhibitor;
    [QueryParameter("p_first_to_take_rift_herald")] public readonly bool firstToTakeRiftHerald;
    [QueryParameter("p_first_to_take_tower")] public readonly bool firstToTakeTower;
    [QueryParameter("p_baron_amount")] public readonly int baronAmount;
    [QueryParameter("p_champion_amount")] public readonly int championAmount;
    [QueryParameter("p_dragon_amount")] public readonly int dragonAmount;
    [QueryParameter("p_inhibitor_amount")] public readonly int inhibitorAmount;
    [QueryParameter("p_rift_herald_amount")] public readonly int riftHeraldAmount;
    [QueryParameter("p_tower_amount")] public readonly int TowerAmount;
    
    public string GetStoredProcedureName => "insert_match_team_data";

    public InsertMatchTeamDataQueryData(MatchDataResponse match, TeamResponse team) {
        matchId = match.MetaData.MatchId;
        leftTeam = team.leftTeam;
        win = team.win;
        championIdBan1 = team.bans.OrNull(0, ban => ban.ChampionId);
        championIdBan2 = team.bans.OrNull(1, ban => ban.ChampionId);
        championIdBan3 = team.bans.OrNull(2, ban => ban.ChampionId);
        championIdBan4 = team.bans.OrNull(3, ban => ban.ChampionId);
        championIdBan5 = team.bans.OrNull(4, ban => ban.ChampionId);
        firstToTakeBaron = team.Baron.First;
        firstToTakeChampion = team.Champion.First;
        firstToTakeDragon = team.Dragon.First;
        firstToTakeInhibitor = team.Inhibitor.First;
        firstToTakeRiftHerald = team.RiftHerald.First;
        firstToTakeTower = team.Tower.First;
        baronAmount = team.Baron.Kills;
        championAmount = team.Champion.Kills;
        dragonAmount = team.Dragon.Kills;
        inhibitorAmount = team.Inhibitor.Kills;
        riftHeraldAmount = team.RiftHerald.Kills;
        TowerAmount = team.Tower.Kills;
    }
}