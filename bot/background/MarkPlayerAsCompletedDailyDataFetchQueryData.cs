using YordleYelper.bot.data_fetcher.league_api.data;
using YordleYelper.database;
using YordleYelper.database.attributes;

namespace YordleYelper.bot.background; 

public class MarkPlayerAsCompletedDailyDataFetchQueryData : IQueryData<VoidRecord> {
    [QueryParameter("p_puuid")]
    public readonly string puuid;
    
    public string GetStoredProcedureName => "mark_player_as_completed_daily_data_fetch";

    public MarkPlayerAsCompletedDailyDataFetchQueryData(Puuid puuid) {
        this.puuid = puuid.ToString();
    }
}