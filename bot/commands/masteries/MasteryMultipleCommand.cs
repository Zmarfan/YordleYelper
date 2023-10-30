using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.data_fetcher.data_dragon.responses;
using YordleYelper.bot.data_fetcher.league_api;
using YordleYelper.bot.data_fetcher.league_api.responses;
using YordleYelper.bot.extensions;
using YordleYelper.bot.response_creator;

namespace YordleYelper.bot.commands.masteries; 

public class MasteryMultipleCommand : CommandBase {
    private readonly LeagueAccount _leagueAccount;
    private readonly long _amount;
    private readonly bool _filterOutMastered;
    private readonly List<BasicChampionInfo> _basicChampionInfos;
    private readonly LeagueApiProxy _leagueApiProxy;

    public MasteryMultipleCommand(
        LeagueAccount leagueAccount,
        long amount,
        bool filterOutMastered,
        List<BasicChampionInfo> basicChampionInfos,
        LeagueApiProxy leagueApiProxy
    ) {
        _leagueAccount = leagueAccount;
        _amount = amount;
        _filterOutMastered = filterOutMastered;
        _basicChampionInfos = basicChampionInfos;
        _leagueApiProxy = leagueApiProxy;
    }
    
    protected override async Task Run(InteractionContext context) {
        Dictionary<string, BasicChampionInfo> champByKey = _basicChampionInfos.ToDictionary(champ => champ.Key, champ => champ);

        IEnumerable<ChampionMasteryResponse> masteries = (await _leagueApiProxy.GetChampionMasteries(_leagueAccount))
            .OrderByDescending(mastery => mastery.championLevel)
            .ThenByDescending(mastery => mastery.championPoints)
            .Where(mastery => mastery.championLevel != 7 || !_filterOutMastered)
            .Take((int)_amount);

        await context.CreateCommandOk(e => e
            .WithDescription($"The champions with the most mastery point for {_leagueAccount.gameName.ToBold()}:")
            .WithThumbnail(_leagueAccount.summoner.profileIconImageUrl)
        );
        
        foreach (ChampionMasteryResponse mastery in masteries) {
            await context.Channel.SendMessageAsync(
                MasteryEmbedCreator.CreateChampionMasteryMessage(context, mastery, champByKey[mastery.championId])
            );
        }
    }
}