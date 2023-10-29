using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.data_fetcher.data_dragon.responses;
using YordleYelper.bot.data_fetcher.league_api;
using YordleYelper.bot.data_fetcher.league_api.data;
using YordleYelper.bot.data_fetcher.league_api.responses;
using YordleYelper.bot.extensions;
using YordleYelper.bot.http_client;
using YordleYelper.bot.response_creator;

namespace YordleYelper.bot.commands; 

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
            await context.CreateCommandOk(new DiscordEmbedBuilder()
                .WithDescription($"The last time **{_leagueAccount.gameName}** played **{_basicChampionInfo.Name}** was {timeSince} ago")
                .WithThumbnail(_basicChampionInfo.PortraitImageUrl)
            );
        }
        catch (HttpStatusException exception) {
            if (exception.statusCode != HttpStatusCode.NotFound) {
                throw;
            }
            
            await context.CreateCommandOk(new DiscordEmbedBuilder()
                .WithDescription($"As far as I can tell; **{_leagueAccount.gameName}** has not played **{_basicChampionInfo.Name}** yet!")
                .WithThumbnail(_basicChampionInfo.PortraitImageUrl)
            );
        }
    }
}