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
using YordleYelper.bot.background;
using YordleYelper.bot.data_fetcher.data_dragon;
using YordleYelper.bot.data_fetcher.league_api;
using YordleYelper.bot.http_client;
using YordleYelper.database;
using YordleYelper.database.config;

namespace YordleYelper.bot; 

public class DiscordBot {
    public static DiscordBotConfig Config { get; private set; }
    public static ILogger Logger { get; private set; }
    
    private readonly DiscordClient _client;
    private readonly BackgroundHandler _backgroundHandler;
        
    public DiscordBot() {
        Config = JsonConvert.DeserializeObject<DiscordBotConfig>(File.ReadAllText(@"src\config.json"));
        
        _client = new DiscordClient(Config.DiscordConfiguration);
        Logger = _client.Logger;
        VersionHolder.Init(GetCurrentVersion());

        SlashCommands.Database = new Database(new DatabaseConfig {
            Database = Config.MySqlDatabase,
            Password = Config.MySqlPassword,
            Server = Config.MySqlServer,
            UserId = Config.MySqlUserId
        }, Logger);
        SlashCommands.DataDragonProxy = new DataDragonProxy();
        SlashCommands.LeagueApiProxy = new LeagueApiProxy(Config.LeagueApiAuthToken);
        SlashCommands.Logger = _client.Logger;
        _backgroundHandler = new BackgroundHandler(SlashCommands.LeagueApiProxy, SlashCommands.Database, Logger);
        
        _client.UseSlashCommands(new SlashCommandsConfiguration()).RegisterCommands<SlashCommands>();
    }

    public async Task Start() {
        await _client.ConnectAsync();
        while (true) {
            try {
                Logger.LogInformation("Starting background job!");
                _backgroundHandler.Run();
            }
            catch (Exception e) {
                Logger.LogError(e, "Unable to run background!");
            }
            // max 10 requests per iteration should be about 60 request/min leaving 40 requests to chat requests.
            await Task.Delay(Config.TaskDelayTime);
        }
    }
    
    private static string GetCurrentVersion() {
        return new HttpClient().Get<List<string>>("https://ddragon.leagueoflegends.com/api/versions.json").Result.First();
    }
}