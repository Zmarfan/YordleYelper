using System.Threading.Tasks;
using Newtonsoft.Json;

namespace YordleYelper.bot.http_client; 

public static class HttpClient {
    private static readonly System.Net.Http.HttpClient CLIENT = new();
    
    public static async Task<T> Get<T>(string endpoint) {
        string rawData = await CLIENT.GetStringAsync(endpoint);
        return JsonConvert.DeserializeObject<T>(rawData);
    }
}