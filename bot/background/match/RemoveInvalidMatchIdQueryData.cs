using YordleYelper.database;
using YordleYelper.database.attributes;

namespace YordleYelper.bot.background.match; 

public class RemoveInvalidMatchIdQueryData : IQueryData<VoidRecord> {
    [QueryParameter("p_match_id")] public readonly string matchId;
    
    public string GetStoredProcedureName => "remove_invalid_match_id";

    public RemoveInvalidMatchIdQueryData(string matchId) {
        this.matchId = matchId;
    }
}