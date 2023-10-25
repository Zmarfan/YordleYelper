using DSharpPlus;
using DSharpPlus.CommandsNext;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace YordleYelper.bot; 

public struct DiscordBotConfig {
    [JsonProperty("token")]
    public string Token { get; private set; }
    
    [JsonProperty("tokenType")]
    public TokenType TokenType { get; private set; }

    [JsonProperty("autoReconnect")]
    public bool AutoReconnect { get; private set; }

    [JsonProperty("minimumLogLevel")]
    public LogLevel MinimumLogLevel { get; private set; }

    
    
    [JsonProperty("stringPrefixes")]
    public string[] StringPrefixes { get; private set; }
    
    [JsonProperty("enableMentionPrefix")]
    public bool EnableMentionPrefix { get; private set; }
    
    [JsonProperty("enableDms")]
    public bool EnableDms { get; private set; }
    
    [JsonProperty("caseSensitive")]
    public bool CaseSensitive { get; private set; }
    
    [JsonProperty("dmHelp")]
    public bool DmHelp { get; private set; }
    
    [JsonProperty("ignoreExtraArguments")]
    public bool IgnoreExtraArguments { get; private set; }

    public DiscordConfiguration DiscordConfiguration => new() {
        Token = Token,
        TokenType = TokenType,
        AutoReconnect = AutoReconnect,
        MinimumLogLevel = MinimumLogLevel,
        Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents
    };
    
    public CommandsNextConfiguration CommandsNextConfiguration => new() {
        StringPrefixes = StringPrefixes,
        EnableMentionPrefix = EnableMentionPrefix,
        EnableDms = EnableDms,
        CaseSensitive = CaseSensitive,
        DmHelp = DmHelp,
        IgnoreExtraArguments = IgnoreExtraArguments,
    };
}
