using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace YordleYelper.bot.http_client; 

public class HttpClient {
    private readonly System.Net.Http.HttpClient _client = new();
    private readonly ILogger _logger;

    public HttpClient(ILogger logger) {
        _logger = logger;
    }

    public async Task<(bool, T)> TryGet<T>(string endpoint) {
        try {
            return (true, await Get<T>(endpoint));
        }
        catch (Exception e) {
            _logger.Log(LogLevel.Error, e, $"Http get request failed for endpoint: {endpoint}");
            return (false, default);
        }
    }
    
    public async Task<T> Get<T>(string endpoint) {
        _logger.Log(LogLevel.Information, $"Http get request for: {endpoint}");
        HttpResponseMessage response = await _client.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
    }
}