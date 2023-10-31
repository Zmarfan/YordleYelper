using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace YordleYelper.bot.quick_chart_creator;

public class QuickChart {
    private const string BASE_URL = "https://quickchart.io";

    private readonly HttpClient _httpClient = new();

    private readonly int _width;
    private readonly int _height;
    private readonly string _format;
    private readonly string _backgroundColor;
    private readonly string _config;

    public QuickChart(int width, int height, string config, string format = "png", string backgroundColor = "transparent") { 
        _width = width;
        _height = height;
        _config = config;
        _format = format;
        _backgroundColor = backgroundColor;
    }

    public string GetUrl() {
        return new StringBuilder()
            .Append(BASE_URL).Append("/chart?")
            .Append("w=").Append(_width)
            .Append("&h=").Append(_height)
            .Append("&f=").Append(_format)
            .Append("&bkg=").Append(Uri.EscapeDataString(_backgroundColor))
            .Append("&c=").Append(Uri.EscapeDataString(_config))
            .ToString();
    }

    public string GetShortUrl() {
        return JsonConvert.DeserializeObject<QuickChartShortUrlResponse>(PostChart().Content.ReadAsStringAsync().Result).Url;
    }

    public void ToFile(string filePath) => File.WriteAllBytes(filePath, PostChart().Content.ReadAsByteArrayAsync().Result);
    
    private HttpResponseMessage PostChart() {
        string content = JsonSerializer.Serialize(new {
            width = _width,
            height = _height,
            backgroundColor = _backgroundColor,
            format = _format,
            chart = _config,
        }, new JsonSerializerOptions {
            IgnoreNullValues = true
        });
        
        HttpResponseMessage response = _httpClient.PostAsync($"{BASE_URL}/chart/create", new StringContent(content, Encoding.UTF8, "application/json")).Result;
        if (!response.IsSuccessStatusCode)
            throw new Exception("Unsuccessful response from API");
        return response;
    }
}
