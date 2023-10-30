﻿using System;
using Newtonsoft.Json;
using YordleYelper.bot.data_fetcher.league_api.data;

namespace YordleYelper.bot.data_fetcher.league_api.responses; 

public struct Summoner {
    public readonly string accountId;
    public readonly int profileIconId;
    public readonly DateTimeOffset revisionDate;
    public readonly string name;
    public readonly string summonerId;
    public readonly Puuid puuid;
    public readonly long summonerLevel;
    
    [JsonConstructor]
    public Summoner(
        [JsonProperty("accountId")] string accountId,
        [JsonProperty("profileIconId")] int profileIconId,
        [JsonProperty("revisionDate")] long revisionDate,
        [JsonProperty("name")] string name,
        [JsonProperty("id")] string summonerId,
        [JsonProperty("puuid")] string puuid,
        [JsonProperty("summonerLevel")] long summonerLevel
    ) {
        this.accountId = accountId;
        this.profileIconId = profileIconId;
        this.revisionDate = DateTimeOffset.FromUnixTimeMilliseconds(revisionDate);
        this.name = name;
        this.summonerId = summonerId;
        this.puuid = new Puuid(puuid);
        this.summonerLevel = summonerLevel;
    }
}