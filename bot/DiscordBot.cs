using System.IO;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using Newtonsoft.Json;
using YordleYelper.bot.commands;

namespace YordleYelper.bot; 

public class DiscordBot {
    private readonly DiscordClient _client;
        
    public DiscordBot() {
        DiscordBotConfig config = JsonConvert.DeserializeObject<DiscordBotConfig>(File.ReadAllText(@"src\config.json"));

        _client = new DiscordClient(config.DiscordConfiguration);
        _client.UseCommandsNext(config.CommandsNextConfiguration).RegisterCommands<Commands>();
    }
        
    public async Task Start() {
        await _client.ConnectAsync();
        await Task.Delay(-1);
    }
}