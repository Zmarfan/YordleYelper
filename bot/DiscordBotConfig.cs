using DSharpPlus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace YordleYelper.bot; 

public struct DiscordBotConfig {
    [JsonProperty("discordAuthToken")]
    public string DiscordAuthToken { get; private set; }
    
    [JsonProperty("leagueApiAuthToken")]
    public string LeagueApiAuthToken { get; private set; }
    
    [JsonProperty("tokenType")]
    public TokenType TokenType { get; private set; }

    [JsonProperty("autoReconnect")]
    public bool AutoReconnect { get; private set; }

    [JsonProperty("minimumLogLevel")]
    public LogLevel MinimumLogLevel { get; private set; }

    public DiscordConfiguration DiscordConfiguration => new() {
        Token = DiscordAuthToken,
        TokenType = TokenType,
        AutoReconnect = AutoReconnect,
        MinimumLogLevel = MinimumLogLevel,
        Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents
    };
}
