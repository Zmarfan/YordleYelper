using Newtonsoft.Json;
using YordleYelper.bot.data_fetcher.league_api.data;

namespace YordleYelper.bot.data_fetcher.league_api.responses; 

public struct LeagueAccount {
    public readonly Puuid puuid;
    public readonly string gameName;
    public readonly string tagLine;
    public readonly Summoner summoner;

    public LeagueAccount(LeagueAccountResponse leagueAccountResponse, Summoner summoner) {
        puuid = leagueAccountResponse.puuid;
        gameName = leagueAccountResponse.gameName;
        tagLine = leagueAccountResponse.tagLine;
        this.summoner = summoner;
    }
}