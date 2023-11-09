using YordleYelper.bot.data_fetcher.league_api.data;
using YordleYelper.database;
using YordleYelper.database.attributes;

namespace YordleYelper.bot.commands; 

public class IsLeagueAccountRegisteredQueryData : IQueryData<bool> {
    [QueryParameter("p_puuid")] public readonly string puuid;
    
    public string GetStoredProcedureName => "is_league_account_registered";

    public IsLeagueAccountRegisteredQueryData(Puuid puuid) {
        this.puuid = puuid.ToString();
    }
}