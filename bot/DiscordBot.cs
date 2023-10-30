using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QuickChart;
using YordleYelper.bot.data_fetcher.data_dragon;
using YordleYelper.bot.data_fetcher.league_api;
using YordleYelper.bot.http_client;

namespace YordleYelper.bot; 

public class DiscordBot {
    private readonly DiscordClient _client;
        
    public DiscordBot() {
        DiscordBotConfig config = JsonConvert.DeserializeObject<DiscordBotConfig>(File.ReadAllText(@"src\config.json"));
        
        _client = new DiscordClient(config.DiscordConfiguration);
        VersionHolder.Init(GetCurrentVersion(_client.Logger));
        SlashCommands.DataDragonProxy = new DataDragonProxy(_client.Logger);
        SlashCommands.LeagueApiProxy = new LeagueApiProxy(_client.Logger, config.LeagueApiAuthToken);
        SlashCommands.Logger = _client.Logger;
        _client.UseSlashCommands(new SlashCommandsConfiguration()).RegisterCommands<SlashCommands>();
    }

    public async Task Start() {
        await _client.ConnectAsync();

        string test =
            "{type: 'pie',data: {labels: ['Hello world', 'Test'], datasets: [{label: 'Foo',data: [1, 2, 3, 4, 5, 6],borderWidth: 0,}]},options: {cutoutPercentage: 15,plugins: {datalabels: {display: false}}}}";
        Chart qc = new() {
            Width = 500,
            Height = 300,
            Config = test
        };

        Console.WriteLine(qc.GetUrl());
        await Task.Delay(-1);
    }
    
    private static string GetCurrentVersion(ILogger logger) {
        return new HttpClient(logger).Get<List<string>>("https://ddragon.leagueoflegends.com/api/versions.json").Result.First();
    }
}