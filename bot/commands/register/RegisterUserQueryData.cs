using YordleYelper.bot.data_fetcher.league_api.responses;
using YordleYelper.database;
using YordleYelper.database.attributes;

namespace YordleYelper.bot.commands.register; 

public class RegisterUserQueryData : IQueryData<VoidRecord> {
    [QueryParameter("p_puuid")] 
    public readonly string puuid;
    
    [QueryParameter("p_account_id")] 
    public readonly string accountId;
    
    [QueryParameter("p_summoner_id")] 
    public readonly string summonerId;
    
    [QueryParameter("p_game_name")]
    public readonly string gameName;
    
    [QueryParameter("p_tag_line")]
    public readonly string tagLine;
    
    [QueryParameter("p_summoner_name")]
    public readonly string summonerName;
    
    public string GetStoredProcedureName => "register_user";

    public RegisterUserQueryData(LeagueAccount leagueAccount) {
        puuid = leagueAccount.puuid.ToString();
        accountId = leagueAccount.summoner.accountId;
        summonerId = leagueAccount.summoner.summonerId;
        gameName = leagueAccount.gameName;
        tagLine = leagueAccount.tagLine;
        summonerName = leagueAccount.summoner.name;
    }
}