using YordleYelper.bot.data_fetcher.data_dragon.responses;
using YordleYelper.bot.data_fetcher.league_api.responses;
using YordleYelper.database;
using YordleYelper.database.attributes;

namespace YordleYelper.bot.commands.playtime; 

public class FetchChampionPlayTimeDataQueryData : IQueryData<ChampionPlaytimeRecord> {
    [QueryParameter("p_puuid")] public readonly string puuid; 
    [QueryParameter("p_champion_id")] public readonly string championId; 
    
    public string GetStoredProcedureName => "fetch_champion_play_time_data";

    public FetchChampionPlayTimeDataQueryData(LeagueAccount leagueAccount, BasicChampionInfo championInfo) {
        puuid = leagueAccount.puuid.ToString();
        championId = championInfo.Key;
    }
}