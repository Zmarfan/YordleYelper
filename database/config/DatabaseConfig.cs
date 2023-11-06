using Newtonsoft.Json;

namespace YordleYelper.database.config; 

public struct DatabaseConfig {
    [JsonProperty("server")]
    public string Server { get; set; }
    
    [JsonProperty("userId")]
    public string UserId { get; set; }
    
    [JsonProperty("password")]
    public string Password { get; set; }
    
    [JsonProperty("database")]
    public string Database { get; set; }
}