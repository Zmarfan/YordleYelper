using System.Threading.Tasks;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.commands.choices;
using YordleYelper.bot.data_fetcher;
using YordleYelper.bot.data_fetcher.responses;
using YordleYelper.bot.data_fetcher.responses.champion_info;

namespace YordleYelper.bot.commands; 

public class AbilityCommand : CommandBase {
    public const string COMMAND_NAME = "ability";

    private readonly BasicChampionInfo _basicChampionInfo;
    private readonly ChampionAbility _ability;
    private readonly DataDragonProxy _dataDragonProxy;
    
    public AbilityCommand(BasicChampionInfo basicChampionInfo, ChampionAbility ability, DataDragonProxy dataDragonProxy) {
        _basicChampionInfo = basicChampionInfo;
        _ability = ability;
        _dataDragonProxy = dataDragonProxy;
    }

    protected override async Task Run(InteractionContext context) {
        TopChampionInfoResponse fullInfo = _dataDragonProxy.GetChampionInfo(_basicChampionInfo);
    }
}