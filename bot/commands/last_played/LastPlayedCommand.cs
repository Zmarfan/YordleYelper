using System;
using System.Net;
using System.Threading.Tasks;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.data_fetcher.data_dragon.responses;
using YordleYelper.bot.data_fetcher.league_api;
using YordleYelper.bot.data_fetcher.league_api.responses;
using YordleYelper.bot.extensions;
using YordleYelper.bot.http_client;
using YordleYelper.bot.response_creator;

namespace YordleYelper.bot.commands.last_played; 

public class LastPlayedCommand : CommandBase {
    private readonly LeagueAccount _leagueAccount;
    private readonly BasicChampionInfo _basicChampionInfo;
    private readonly LeagueApiProxy _leagueApiProxy;

    public LastPlayedCommand(LeagueAccount leagueAccount, BasicChampionInfo basicChampionInfo, LeagueApiProxy leagueApiProxy) {
        _leagueAccount = leagueAccount;
        _basicChampionInfo = basicChampionInfo;
        _leagueApiProxy = leagueApiProxy;
    }

    protected override async Task Run(InteractionContext context) {
        try {
            ChampionMasteryResponse mastery = await _leagueApiProxy.GetChampionMastery(_leagueAccount, _basicChampionInfo);
            string timeSince = (DateTimeOffset.Now - mastery.lastPlayed).ToTimeSinceString();
            await context.CreateCommandOk(b => b
                .WithDescription($"The last time {_leagueAccount.gameName.ToBold()} played {_basicChampionInfo.Name.ToBold()} was {timeSince} ago")
                .WithThumbnail(_basicChampionInfo.PortraitImageUrl)
            );
        }
        catch (HttpStatusException exception) {
            if (exception.statusCode != HttpStatusCode.NotFound) {
                throw;
            }
            
            await context.CreateCommandOk(b => b
                .WithDescription($"As far as I can tell; {_leagueAccount.gameName.ToBold()} has not played {_basicChampionInfo.Name.ToBold()} yet!")
                .WithThumbnail(_basicChampionInfo.PortraitImageUrl)
            );
        }
    }
}