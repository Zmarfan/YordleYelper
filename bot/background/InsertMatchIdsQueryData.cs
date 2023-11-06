using System.Collections.Generic;
using YordleYelper.database;
using YordleYelper.database.attributes;

namespace YordleYelper.bot.background; 

public class InsertMatchIdsQueryData : IQueryData<VoidRecord> {
    [QueryParameter("p_match_ids")]
    public readonly string matchIds; 
    
    public string GetStoredProcedureName => "insert_match_ids";

    public InsertMatchIdsQueryData(List<string> matchIds) {
        this.matchIds = string.Join(",", matchIds) + ",";
    }
}