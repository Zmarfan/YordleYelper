using YordleYelper.bot.data_fetcher.league_api.data;
using YordleYelper.database;
using YordleYelper.database.attributes;

namespace YordleYelper.bot.background; 

public class MarkPlayerAsInitializedQueryData : IQueryData<VoidRecord> {
    [QueryParameter("p_puuid")]
    public readonly string puuid;

    public string GetStoredProcedureName => "mark_player_as_initialized";
    
    public MarkPlayerAsInitializedQueryData(Puuid puuid) {
        this.puuid = puuid.ToString();
    }
}