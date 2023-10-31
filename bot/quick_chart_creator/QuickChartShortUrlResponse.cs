using Newtonsoft.Json;

namespace YordleYelper.bot.quick_chart_creator; 

public struct QuickChartShortUrlResponse {
    [JsonProperty("success")]
    public bool Status { get; set; }
    [JsonProperty("url")]
    public string Url { get; set; }
}