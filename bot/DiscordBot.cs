using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    private readonly DiscordClient _client;
        
    public DiscordBot() {
        DiscordBotConfig config = JsonConvert.DeserializeObject<DiscordBotConfig>(File.ReadAllText(@"src\config.json"));
        
        _client = new DiscordClient(config.DiscordConfiguration);
        VersionHolder.Init(GetCurrentVersion(_client.Logger));
        SlashCommands.DataDragonProxy = new DataDragonProxy(_client.Logger);
        SlashCommands.LeagueApiProxy = new LeagueApiProxy(_client.Logger, config.LeagueApiAuthToken);
        _client.UseSlashCommands(new SlashCommandsConfiguration()).RegisterCommands<SlashCommands>();
    }

    public async Task Start() {
        await _client.ConnectAsync();
        await Task.Delay(-1);
    }
    
    private static string GetCurrentVersion(ILogger logger) {
        return new HttpClient(logger).Get<List<string>>("https://ddragon.leagueoflegends.com/api/versions.json").Result.First();
    }
}