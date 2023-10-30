using System.Threading.Tasks;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.data_fetcher.data_dragon.responses;
using YordleYelper.bot.data_fetcher.league_api;
using YordleYelper.bot.data_fetcher.league_api.responses;
using YordleYelper.bot.extensions;
using YordleYelper.bot.response_creator;

namespace YordleYelper.bot.commands.masteries; 

public class MasteryCommand : CommandBase {
    private readonly LeagueAccount _leagueAccount;
    private readonly BasicChampionInfo _championInfo;
    private readonly LeagueApiProxy _leagueApiProxy;

    public MasteryCommand(LeagueAccount leagueAccount, BasicChampionInfo championInfo, LeagueApiProxy leagueApiProxy) {
        _leagueAccount = leagueAccount;
        _championInfo = championInfo;
        _leagueApiProxy = leagueApiProxy;
    }
    
    protected override async Task Run(InteractionContext context) {
        ChampionMasteryResponse mastery = await _leagueApiProxy.GetChampionMastery(_leagueAccount, _championInfo);
        await context.CreateCommandOk(_ => MasteryEmbedCreator.CreateChampionMasteryMessage(context, mastery, _championInfo)
            .WithDescription($"The mastery statistics of {_leagueAccount.gameName.ToBold()} for {_championInfo.Name.ToBold()}")
        );
    }
}