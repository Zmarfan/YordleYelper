using Newtonsoft.Json;
using YordleYelper.bot.data_fetcher.league_api.data;

namespace YordleYelper.bot.data_fetcher.league_api.responses.match; 

public struct ParticipantResponse {
    public readonly Puuid puuid;
    public readonly bool leftTeam;
    public readonly int participantId;
    
    public readonly string summonerName;
    public readonly int summonerLevel;
    public readonly int profileIconId;
    
    public readonly bool win;
    public readonly bool gameEndedInEarlySurrender;
    public readonly bool gameEndedInSurrender;
    public readonly bool teamEarlySurrendered;
    
    public readonly int kills;
    public readonly int deaths;
    public readonly int assists;
    public readonly string teamPosition;
    public readonly int timePlayed;
    public readonly int baronKills;
    public readonly int dragonKills;
    public readonly int bountyLevel;
    
    public readonly int doubleKills;
    public readonly int tripleKills;
    public readonly int quadraKills;
    public readonly int pentaKills;
    public readonly int unrealKills;
    public readonly int killingSprees;
    public readonly int largestKillingSpree;
    public readonly int largestMultiKill;

    public readonly int championId;
    public readonly string championName;
    public readonly int championExperience;
    public readonly int championLevel;
    public readonly int championTransform;

    public readonly int spellQCasts;
    public readonly int spellWCasts;
    public readonly int spellECasts;
    public readonly int spellRCasts;
    public readonly int summoner1Id;
    public readonly int summoner2Id;
    public readonly int summoner1Casts;
    public readonly int summoner2Casts;
    
    public readonly int goldEarned;
    public readonly int goldSpent;
    public readonly int consumablesPurchased;
    public readonly int itemsPurchased;
    public readonly int sightWardsBoughtInGame;
    public readonly int visionWardsBoughtInGame;
    public readonly int item0;
    public readonly int item1;
    public readonly int item2;
    public readonly int item3;
    public readonly int item4;
    public readonly int item5;
    public readonly int item6;
    
    public readonly bool firstBloodAssist;
    public readonly bool firstBloodKill;
    public readonly bool firstTowerAssist;
    public readonly bool firstTowerKill;
    public readonly int turretKills;
    public readonly int turretTakedowns;
    public readonly int turretsLost;
    public readonly int inhibitorKills;
    public readonly int inhibitorTakedowns;
    public readonly int inhibitorsLost;
    public readonly int damageDealtToBuildings;
    public readonly int damageDealtToObjectives;
    public readonly int damageDealtToTurrets;
    public readonly int damageSelfMitigated;
    public readonly int nexusKills;
    public readonly int nexusLost;
    public readonly int nexusTakedowns;
    public readonly int totalMinionsKilled;
    public readonly int neutralMinionsKilled;
    public readonly int totalAllyJungleMinionsKilled;
    public readonly int totalEnemyJungleMinionsKilled;
    public readonly int totalDamageDealt;
    public readonly int totalDamageDealtToChampions;
    public readonly int totalDamageShieldedOnTeammates;
    public readonly int totalDamageTaken;
    public readonly int totalHeal;
    public readonly int totalHealsOnTeammates;
    public readonly int largestCriticalStrike;
    public readonly int longestTimeSpentLiving;
    public readonly int magicDamageDealt;
    public readonly int magicDamageDealtToChampions;
    public readonly int magicDamageTaken;
    public readonly int trueDamageDealt;
    public readonly int trueDamageDealtToChampions;
    public readonly int trueDamageTaken;
    public readonly int objectivesStolen;
    public readonly int objectivesStolenAssists;
    public readonly int physicalDamageDealt;
    public readonly int physicalDamageDealtToChampions;
    public readonly int physicalDamageTaken;
    public readonly int timeCCingOthers;
    public readonly int totalTimeCCDealt;
    public readonly int totalTimeSpentDead;
    public readonly int totalUnitsHealed;

    public readonly int visionScore;
    public readonly int detectorWardsPlaced;
    public readonly int wardsKilled;
    public readonly int wardsPlaced;

    public readonly int allInPings;
    public readonly int assistMePings;
    public readonly int baitPings;
    public readonly int basicPings;
    public readonly int commandPings;
    public readonly int dangerPings;
    public readonly int enemyMissingPings;
    public readonly int enemyVisionPings;
    public readonly int getBackPings;
    public readonly int holdPings;
    public readonly int needVisionPings;
    public readonly int onMyWayPings;
    public readonly int pushPings;
    public readonly int visionClearedPings;
    public readonly ChallengesResponse challenges;
    public readonly PerksResponse perks;

    [JsonConstructor]
    public ParticipantResponse(
        [JsonProperty("puuid")] string puuid,
        [JsonProperty("teamId")] int teamId,
        [JsonProperty("participantId")] int participantId,
        
        [JsonProperty("summonerName")] string summonerName,
        [JsonProperty("summonerLevel")] int summonerLevel,
        [JsonProperty("profileIcon")] int profileIcon,
        
        [JsonProperty("win")] bool win,
        [JsonProperty("gameEndedInEarlySurrender")] bool gameEndedInEarlySurrender,
        [JsonProperty("gameEndedInSurrender")] bool gameEndedInSurrender,
        [JsonProperty("teamEarlySurrendered")] bool teamEarlySurrendered,
            
        [JsonProperty("kills")] int kills,
        [JsonProperty("deaths")] int deaths,
        [JsonProperty("assists")] int assists,
        [JsonProperty("teamPosition")] string teamPosition,
        [JsonProperty("timePlayed")] int timePlayed,
        [JsonProperty("baronKills")] int baronKills,
        [JsonProperty("dragonKills")] int dragonKills,
        [JsonProperty("bountyLevel")] int bountyLevel,
        [JsonProperty("doubleKills")] int doubleKills,
        [JsonProperty("tripleKills")] int tripleKills,
        [JsonProperty("quadraKills")] int quadraKills,
        [JsonProperty("pentaKills")] int pentaKills,
        [JsonProperty("unrealKills")] int unrealKills,
        [JsonProperty("killingSprees")] int killingSprees,
        [JsonProperty("largestKillingSpree")] int largestKillingSpree,
        [JsonProperty("largestMultiKill")] int largestMultiKill,
        [JsonProperty("championId")] int championId,
        [JsonProperty("championName")] string championName,
        [JsonProperty("champExperience")] int champExperience,
        [JsonProperty("champLevel")] int champLevel,
        [JsonProperty("championTransform")] int championTransform,
        [JsonProperty("spell1Casts")] int spell1Casts,
        [JsonProperty("spell2Casts")] int spell2Casts,
        [JsonProperty("spell3Casts")] int spell3Casts,
        [JsonProperty("spell4Casts")] int spell4Casts,
        [JsonProperty("summoner1Id")] int summoner1Id,
        [JsonProperty("summoner2Id")] int summoner2Id,
        [JsonProperty("summoner1Casts")] int summoner1Casts,
        [JsonProperty("summoner2Casts")] int summoner2Casts,
        [JsonProperty("goldEarned")] int goldEarned,
        [JsonProperty("goldSpent")] int goldSpent,
        [JsonProperty("consumablesPurchased")] int consumablesPurchased,
        [JsonProperty("itemsPurchased")] int itemsPurchased,
        [JsonProperty("sightWardsBoughtInGame")] int sightWardsBoughtInGame,
        [JsonProperty("visionWardsBoughtInGame")] int visionWardsBoughtInGame,
        [JsonProperty("item0")] int item0,
        [JsonProperty("item1")] int item1,
        [JsonProperty("item2")] int item2,
        [JsonProperty("item3")] int item3,
        [JsonProperty("item4")] int item4,
        [JsonProperty("item5")] int item5,
        [JsonProperty("item6")] int item6,
        [JsonProperty("firstBloodAssist")] bool firstBloodAssist,
        [JsonProperty("firstBloodKill")] bool firstBloodKill,
        [JsonProperty("firstTowerAssist")] bool firstTowerAssist,
        [JsonProperty("firstTowerKill")] bool firstTowerKill,
        [JsonProperty("turretKills")] int turretKills,
        [JsonProperty("turretTakedowns")] int turretTakedowns,
        [JsonProperty("turretsLost")] int turretsLost,
        [JsonProperty("inhibitorKills")] int inhibitorKills,
        [JsonProperty("inhibitorTakedowns")] int inhibitorTakedowns,
        [JsonProperty("inhibitorsLost")] int inhibitorsLost,
        [JsonProperty("damageDealtToBuildings")] int damageDealtToBuildings,
        [JsonProperty("damageDealtToObjectives")] int damageDealtToObjectives,
        [JsonProperty("damageDealtToTurrets")] int damageDealtToTurrets,
        [JsonProperty("damageSelfMitigated")] int damageSelfMitigated,
        [JsonProperty("nexusKills")] int nexusKills,
        [JsonProperty("nexusLost")] int nexusLost,
        [JsonProperty("nexusTakedowns")] int nexusTakedowns,
        [JsonProperty("totalMinionsKilled")] int totalMinionsKilled,
        [JsonProperty("neutralMinionsKilled")] int neutralMinionsKilled,
        [JsonProperty("totalAllyJungleMinionsKilled")] int totalAllyJungleMinionsKilled,
        [JsonProperty("totalEnemyJungleMinionsKilled")] int totalEnemyJungleMinionsKilled,
        [JsonProperty("totalDamageDealt")] int totalDamageDealt,
        [JsonProperty("totalDamageDealtToChampions")] int totalDamageDealtToChampions,
        [JsonProperty("totalDamageShieldedOnTeammates")] int totalDamageShieldedOnTeammates,
        [JsonProperty("totalDamageTaken")] int totalDamageTaken,
        [JsonProperty("totalHeal")] int totalHeal,
        [JsonProperty("totalHealsOnTeammates")] int totalHealsOnTeammates,
        [JsonProperty("largestCriticalStrike")] int largestCriticalStrike,
        [JsonProperty("longestTimeSpentLiving")] int longestTimeSpentLiving,
        [JsonProperty("magicDamageDealt")] int magicDamageDealt,
        [JsonProperty("magicDamageDealtToChampions")] int magicDamageDealtToChampions,
        [JsonProperty("magicDamageTaken")] int magicDamageTaken,
        [JsonProperty("trueDamageDealt")] int trueDamageDealt,
        [JsonProperty("trueDamageDealtToChampions")] int trueDamageDealtToChampions,
        [JsonProperty("trueDamageTaken")] int trueDamageTaken,
        [JsonProperty("objectivesStolen")] int objectivesStolen,
        [JsonProperty("objectivesStolenAssists")] int objectivesStolenAssists,
        [JsonProperty("physicalDamageDealt")] int physicalDamageDealt,
        [JsonProperty("physicalDamageDealtToChampions")] int physicalDamageDealtToChampions,
        [JsonProperty("physicalDamageTaken")] int physicalDamageTaken,
        [JsonProperty("timeCCingOthers")] int timeCCingOthers,
        [JsonProperty("totalTimeCCDealt")] int totalTimeCCDealt,
        [JsonProperty("totalTimeSpentDead")] int totalTimeSpentDead,
        [JsonProperty("totalUnitsHealed")] int totalUnitsHealed,
        [JsonProperty("visionScore")] int visionScore,
        [JsonProperty("detectorWardsPlaced")] int detectorWardsPlaced,
        [JsonProperty("wardsKilled")] int wardsKilled,
        [JsonProperty("wardsPlaced")] int wardsPlaced,
        [JsonProperty("allInPings")] int allInPings,
        [JsonProperty("assistMePings")] int assistMePings,
        [JsonProperty("baitPings")] int baitPings,
        [JsonProperty("basicPings")] int basicPings,
        [JsonProperty("commandPings")] int commandPings,
        [JsonProperty("dangerPings")] int dangerPings,
        [JsonProperty("enemyMissingPings")] int enemyMissingPings,
        [JsonProperty("enemyVisionPings")] int enemyVisionPings,
        [JsonProperty("getBackPings")] int getBackPings,
        [JsonProperty("holdPings")] int holdPings,
        [JsonProperty("needVisionPings")] int needVisionPings,
        [JsonProperty("onMyWayPings")] int onMyWayPings,
        [JsonProperty("pushPings")] int pushPings,
        [JsonProperty("visionClearedPings")] int visionClearedPings,
        [JsonProperty("challenges")] ChallengesResponse challenges,
        [JsonProperty("perks")] PerksResponse perks
     ) {
        this.puuid = new Puuid(puuid);
        leftTeam = teamId == 100;
        this.participantId = participantId;
        this.summonerName = summonerName;
        this.summonerLevel = summonerLevel;
        profileIconId = profileIcon;
        this.win = win;
        this.gameEndedInEarlySurrender = gameEndedInEarlySurrender;
        this.gameEndedInSurrender = gameEndedInSurrender;
        this.teamEarlySurrendered = teamEarlySurrendered;
        this.kills = kills;
        this.deaths = deaths;
        this.assists = assists;
        this.teamPosition = teamPosition;
        this.timePlayed = timePlayed;
        this.baronKills = baronKills;
        this.dragonKills = dragonKills;
        this.bountyLevel = bountyLevel;
        this.doubleKills = doubleKills;
        this.tripleKills = tripleKills;
        this.quadraKills = quadraKills;
        this.pentaKills = pentaKills;
        this.unrealKills = unrealKills;
        this.killingSprees = killingSprees;
        this.largestKillingSpree = largestKillingSpree;
        this.largestMultiKill = largestMultiKill;
        this.championId = championId;
        this.championName = championName;
        championExperience = champExperience;
        championLevel = champLevel;
        this.championTransform = championTransform;
        spellQCasts = spell1Casts;
        spellWCasts = spell2Casts;
        spellECasts = spell3Casts;
        spellRCasts = spell4Casts;
        this.summoner1Id = summoner1Id;
        this.summoner2Id = summoner2Id;
        this.summoner1Casts = summoner1Casts;
        this.summoner2Casts = summoner2Casts;
        this.goldEarned = goldEarned;
        this.goldSpent = goldSpent;
        this.consumablesPurchased = consumablesPurchased;
        this.itemsPurchased = itemsPurchased;
        this.sightWardsBoughtInGame = sightWardsBoughtInGame;
        this.visionWardsBoughtInGame = visionWardsBoughtInGame;
        this.item0 = item0;
        this.item1 = item1;
        this.item2 = item2;
        this.item3 = item3;
        this.item4 = item4;
        this.item5 = item5;
        this.item6 = item6;
        this.firstBloodAssist = firstBloodAssist;
        this.firstBloodKill = firstBloodKill;
        this.firstTowerAssist = firstTowerAssist;
        this.firstTowerKill = firstTowerKill;
        this.turretKills = turretKills;
        this.turretTakedowns = turretTakedowns;
        this.turretsLost = turretsLost;
        this.inhibitorKills = inhibitorKills;
        this.inhibitorTakedowns = inhibitorTakedowns;
        this.inhibitorsLost = inhibitorsLost;
        this.damageDealtToBuildings = damageDealtToBuildings;
        this.damageDealtToObjectives = damageDealtToObjectives;
        this.damageDealtToTurrets = damageDealtToTurrets;
        this.damageSelfMitigated = damageSelfMitigated;
        this.nexusKills = nexusKills;
        this.nexusLost = nexusLost;
        this.nexusTakedowns = nexusTakedowns;
        this.totalMinionsKilled = totalMinionsKilled;
        this.neutralMinionsKilled = neutralMinionsKilled;
        this.totalAllyJungleMinionsKilled = totalAllyJungleMinionsKilled;
        this.totalEnemyJungleMinionsKilled = totalEnemyJungleMinionsKilled;
        this.totalDamageDealt = totalDamageDealt;
        this.totalDamageDealtToChampions = totalDamageDealtToChampions;
        this.totalDamageShieldedOnTeammates = totalDamageShieldedOnTeammates;
        this.totalDamageTaken = totalDamageTaken;
        this.totalHeal = totalHeal;
        this.totalHealsOnTeammates = totalHealsOnTeammates;
        this.largestCriticalStrike = largestCriticalStrike;
        this.longestTimeSpentLiving = longestTimeSpentLiving;
        this.magicDamageDealt = magicDamageDealt;
        this.magicDamageDealtToChampions = magicDamageDealtToChampions;
        this.magicDamageTaken = magicDamageTaken;
        this.trueDamageDealt = trueDamageDealt;
        this.trueDamageDealtToChampions = trueDamageDealtToChampions;
        this.trueDamageTaken = trueDamageTaken;
        this.objectivesStolen = objectivesStolen;
        this.objectivesStolenAssists = objectivesStolenAssists;
        this.physicalDamageDealt = physicalDamageDealt;
        this.physicalDamageDealtToChampions = physicalDamageDealtToChampions;
        this.physicalDamageTaken = physicalDamageTaken;
        this.timeCCingOthers = timeCCingOthers;
        this.totalTimeCCDealt = totalTimeCCDealt;
        this.totalTimeSpentDead = totalTimeSpentDead;
        this.totalUnitsHealed = totalUnitsHealed;
        this.visionScore = visionScore;
        this.detectorWardsPlaced = detectorWardsPlaced;
        this.wardsKilled = wardsKilled;
        this.wardsPlaced = wardsPlaced;
        this.allInPings = allInPings;
        this.assistMePings = assistMePings;
        this.baitPings = baitPings;
        this.basicPings = basicPings;
        this.commandPings = commandPings;
        this.dangerPings = dangerPings;
        this.enemyMissingPings = enemyMissingPings;
        this.enemyVisionPings = enemyVisionPings;
        this.getBackPings = getBackPings;
        this.holdPings = holdPings;
        this.needVisionPings = needVisionPings;
        this.onMyWayPings = onMyWayPings;
        this.pushPings = pushPings;
        this.visionClearedPings = visionClearedPings;
        this.challenges = challenges;
        this.perks = perks;
    }
}