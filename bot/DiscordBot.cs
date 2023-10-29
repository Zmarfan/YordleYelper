using System;
using System.IO;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using YordleYelper.bot.commands;
using YordleYelper.bot.data_fetcher;
using YordleYelper.bot.data_fetcher.data_dragon;
using YordleYelper.bot.data_fetcher.data_dragon.responses;
using YordleYelper.bot.data_fetcher.league_api;
using YordleYelper.bot.data_fetcher.league_api.data;
using YordleYelper.bot.data_fetcher.league_api.responses;
using YordleYelper.bot.http_client;

namespace YordleYelper.bot; 

public class DiscordBot {
    private readonly DiscordClient _client;
        
    public DiscordBot() {
        DiscordBotConfig config = JsonConvert.DeserializeObject<DiscordBotConfig>(File.ReadAllText(@"src\config.json"));

        _client = new DiscordClient(config.DiscordConfiguration);
        SlashCommands.DataDragonProxy = new DataDragonProxy(_client.Logger);
        SlashCommands.LeagueApiProxy = new LeagueApiProxy(_client.Logger, config.LeagueApiAuthToken);
        Puuid puuid = SlashCommands.LeagueApiProxy.GetPuuidByRiotId("LordZmarfan").Result;
        Console.WriteLine(puuid);
        SlashCommands.DataDragonProxy.TryGetBasicChampionInfo("viego", out BasicChampionInfo championInfo);
        ChampionMasteryResponse mastery = SlashCommands.LeagueApiProxy.GetChampionMastery(puuid, championInfo).Result;
        _client.UseSlashCommands(new SlashCommandsConfiguration()).RegisterCommands<SlashCommands>();
    }

    public async Task Start() {
        await _client.ConnectAsync();
        await Task.Delay(-1);
    }
}