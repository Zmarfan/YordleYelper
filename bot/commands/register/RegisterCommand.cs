using System.Threading.Tasks;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.data_fetcher.league_api.responses;
using YordleYelper.bot.extensions;
using YordleYelper.bot.response_creator;
using YordleYelper.database;

namespace YordleYelper.bot.commands.register; 

public class RegisterCommand : CommandBase {
    private readonly LeagueAccount _leagueAccount;
    private readonly Database _database;

    public RegisterCommand(LeagueAccount leagueAccount, Database database) {
        _leagueAccount = leagueAccount;
        _database = database;
    }

    protected override async Task Run(InteractionContext context) {
        _database.ExecuteVoidQuery(new RegisterUserQueryData(_leagueAccount));
        
        await context.CreateCommandOk(e => e
            .WithDescription($"Successfully registered {_leagueAccount.gameName.ToBold()} for data collection!\nYou can expect to view your statistics in about 30 min up to a few hours!")
            .WithThumbnail(_leagueAccount.summoner.profileIconImageUrl)
        );
    }
}