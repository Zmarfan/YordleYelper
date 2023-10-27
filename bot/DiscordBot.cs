using System.IO;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using YordleYelper.bot.commands;
using YordleYelper.bot.data_fetcher;
using YordleYelper.bot.http_client;

namespace YordleYelper.bot; 

public class DiscordBot {
    private readonly DiscordClient _client;
        
    public DiscordBot() {
        DiscordBotConfig config = JsonConvert.DeserializeObject<DiscordBotConfig>(File.ReadAllText(@"src\config.json"));

        _client = new DiscordClient(config.DiscordConfiguration);
        SlashCommands.DataDragonProxy = new DataDragonProxy(new HttpClient(_client.Logger));
        _client.UseSlashCommands(new SlashCommandsConfiguration()).RegisterCommands<SlashCommands>();
    }

    public async Task Start() {
        await _client.ConnectAsync();
        await Task.Delay(-1);
    }
}