using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using YordleYelper.bot.logger;

namespace YordleYelper.bot.http_client; 

public static class HttpClient {
    private static readonly System.Net.Http.HttpClient CLIENT = new();
    
    public static async Task<(bool, T)> TryGet<T>(string endpoint) {
        Logger.Info($"Http get request for: {endpoint}");
        try {
            HttpResponseMessage response = await CLIENT.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            return (true, JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()));
        }
        catch (Exception e) {
            Logger.Error(e, $"Http get request failed for endpoint: {endpoint}");
            return (false, default);
        }
    }
}