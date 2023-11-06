using YordleYelper.bot.data_fetcher.league_api.data;
using YordleYelper.database;

namespace YordleYelper.bot.background; 

public class GetDailyUsersDataFetchQueryData : IQueryData<Puuid> {
    public string GetStoredProcedureName => "get_daily_users_data_fetch";
}