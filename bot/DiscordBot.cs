using System.IO;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using Newtonsoft.Json;
using YordleYelper.bot.data_fetcher.data_dragon;
using YordleYelper.bot.data_fetcher.league_api;

namespace YordleYelper.bot; 

public class DiscordBot {
    private readonly DiscordClient _client;
        
    public DiscordBot() {
        DiscordBotConfig config = JsonConvert.DeserializeObject<DiscordBotConfig>(File.ReadAllText(@"src\config.json"));

        _client = new DiscordClient(config.DiscordConfiguration);
        SlashCommands.DataDragonProxy = new DataDragonProxy(_client.Logger);
        SlashCommands.LeagueApiProxy = new LeagueApiProxy(_client.Logger, config.LeagueApiAuthToken);
        _client.UseSlashCommands(new SlashCommandsConfiguration()).RegisterCommands<SlashCommands>();
    }

    public async Task Start() {
        await _client.ConnectAsync();
        await Task.Delay(-1);
    }
}