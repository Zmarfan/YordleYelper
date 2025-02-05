﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace YordleYelper.bot.http_client; 

public class HttpClient {
    private readonly System.Net.Http.HttpClient _client = new();
    private readonly ILogger _logger = DiscordBot.Logger;

    public static HttpClient LeagueApiHttpClient(string authToken) {
        HttpClient client = new();
        client._client.DefaultRequestHeaders.Add("X-Riot-Token", authToken);
        return client;
    }

    public async Task<T> Get<T>(string endpoint) {
        return JsonConvert.DeserializeObject<T>(await GetRaw(endpoint));
    }
    
    public async Task<string> GetRaw(string endpoint) {
        _logger.LogInformation($"Http get request for: {endpoint}");
        HttpResponseMessage response = await _client.GetAsync(endpoint);
        HttpStatusCode statusCode = response.StatusCode;
        try {
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception exception) {
            throw new HttpStatusException(statusCode, exception);
        }
    }
}