using YordleYelper.bot.data_fetcher.league_api.data;
using YordleYelper.database;

namespace YordleYelper.bot.background; 

public class GetNotInitializedPlayerPuuidsQueryData : IQueryData<Puuid> {
    public string GetStoredProcedureName => "get_not_initialized_player_puuids";
}