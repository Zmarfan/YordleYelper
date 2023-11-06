using YordleYelper.database;

namespace YordleYelper.bot.background; 

public class FetchMatchIdsWithNoDataQueryData : IQueryData<string> {
    public string GetStoredProcedureName => "fetch_match_ids_with_no_data";
}