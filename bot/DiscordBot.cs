using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using YordleYelper.bot.data_fetcher.data_dragon;
using YordleYelper.bot.data_fetcher.league_api;
using YordleYelper.bot.http_client;

namespace YordleYelper.bot; 

public class DiscordBot {
    public static DiscordBotConfig Config { get; private set; }
    public static ILogger Logger { get; private set; }
    
    private readonly DiscordClient _client;
        
    public DiscordBot() {
        Config = JsonConvert.DeserializeObject<DiscordBotConfig>(File.ReadAllText(@"src\config.json"));
        
        _client = new DiscordClient(Config.DiscordConfiguration);
        Logger = _client.Logger;
        VersionHolder.Init(GetCurrentVersion());
        SlashCommands.DataDragonProxy = new DataDragonProxy();
        SlashCommands.LeagueApiProxy = new LeagueApiProxy(Config.LeagueApiAuthToken);
        SlashCommands.Logger = _client.Logger;
        _client.UseSlashCommands(new SlashCommandsConfiguration()).RegisterCommands<SlashCommands>();
    }

    public async Task Start() {
        await _client.ConnectAsync();
        while (true) {
            await Task.Delay(Config.TaskDelayTime);
            
            // max 10 requests per iteration should be about 60 request/min leaving 40 requests to chat requests.
            
            // 1. if any new player added -> fetch all their matches (should be 10 requests) then mark player as have done daily check. break;
            // 2. if the daily fetch isn't done for any player do so.
            // 3. Otherwise fetch match data for match ids in db that doesn't have any data
        }
    }
    
    private static string GetCurrentVersion() {
        return new HttpClient().Get<List<string>>("https://ddragon.leagueoflegends.com/api/versions.json").Result.First();
    }
}