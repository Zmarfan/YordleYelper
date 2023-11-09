using YordleYelper.bot.data_fetcher.data_dragon.responses;
using YordleYelper.bot.data_fetcher.league_api.responses;
using YordleYelper.database;
using YordleYelper.database.attributes;

namespace YordleYelper.bot.commands.playtime; 

public class FetchChampionPlaysPerDayQueryData : IQueryData<PlaysPerDayRecord> {
    [QueryParameter("p_puuid")] public readonly string puuid;
    [QueryParameter("p_champion_id")] public readonly string championId;
    
    public string GetStoredProcedureName => "fetch_champion_plays_per_day";

    public FetchChampionPlaysPerDayQueryData(LeagueAccount leagueAccount, BasicChampionInfo championInfo) {
        puuid = leagueAccount.puuid.ToString();
        championId = championInfo.Key;
    }
}