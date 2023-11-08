using Newtonsoft.Json;

namespace YordleYelper.bot.data_fetcher.league_api.responses.match; 

public struct ChallengesResponse {
    public readonly float twelveAssistStreakCount;
    public readonly float abilityUses;
    public readonly float acesBefore15Minutes;
    public readonly float alliedJungleMonsterKills;
    public readonly float baronBuffGoldAdvantageOverThreshold;
    public readonly float baronTakedowns;
    public readonly float blastConeOppositeOpponentCount;
    public readonly float bountyGold;
    public readonly float buffsStolen;
    public readonly float completeSupportQuestInTime;
    public readonly float controlWardsPlaced;
    public readonly float controlWardTimeCoverageInRiverOrEnemyHalf;
    public readonly float damagePerMinute;
    public readonly float damageTakenOnTeamPercentage;
    public readonly float dancedWithRiftHerald;
    public readonly float deathsByEnemyChamps;
    public readonly float dodgeSkillShotsSmallWindow;
    public readonly float doubleAces;
    public readonly float dragonTakedowns;
    public readonly float earliestBaron;
    public readonly float earliestDragonTakedown;
    public readonly float earliestElderDragon;
    public readonly float earlyLaningPhaseGoldExpAdvantage;
    public readonly float effectiveHealAndShielding;
    public readonly float elderDragonKillsWithOpposingSoul;
    public readonly float elderDragonMultikills;
    public readonly float enemyChampionImmobilizations;
    public readonly float enemyJungleMonsterKills;
    public readonly float epicMonsterKillsNearEnemyJungler;
    public readonly float epicMonsterKillsWithin30SecondsOfSpawn;
    public readonly float epicMonsterSteals;
    public readonly float epicMonsterStolenWithoutSmite;
    public readonly float fasterSupportQuestCompletion;
    public readonly float fastestLegendary;
    public readonly float firstTurretKilled;
    public readonly float firstTurretKilledTime;
    public readonly float flawlessAces;
    public readonly float fullTeamTakedown;
    public readonly float gameLength;
    public readonly float getTakedownsInAllLanesEarlyJungleAsLaner;
    public readonly float goldPerMinute;
    public readonly float hadAfkTeammate;
    public readonly float hadOpenNexus;
    public readonly float highestChampionDamage;
    public readonly float highestCrowdControlScore;
    public readonly float highestWardKills;
    public readonly float immobilizeAndKillWithAlly;
    public readonly float initialBuffCount;
    public readonly float initialCrabCount;
    public readonly float jungleCsBefore10Minutes;
    public readonly float junglerKillsEarlyJungle;
    public readonly float junglerTakedownsNearDamagedEpicMonster;
    public readonly float kTurretsDestroyedBeforePlatesFall;
    public readonly float kda;
    public readonly float killAfterHiddenWithAlly;
    public readonly float killParticipation;
    public readonly float killedChampTookFullTeamDamageSurvived;
    public readonly float killingSprees;
    public readonly float killsNearEnemyTurret;
    public readonly float killsOnLanersEarlyJungleAsJungler;
    public readonly float killsOnOtherLanesEarlyJungleAsLaner;
    public readonly float killsOnRecentlyHealedByAramPack;
    public readonly float killsUnderOwnTurret;
    public readonly float killsWithHelpFromEpicMonster;
    public readonly float knockEnemyIntoTeamAndKill;
    public readonly float landSkillShotsEarlyGame;
    public readonly float laneMinionsFirst10Minutes;
    public readonly float laningPhaseGoldExpAdvantage;
    public readonly float legendaryCount;
    public readonly float lostAnInhibitor;
    public readonly float maxCsAdvantageOnLaneOpponent;
    public readonly float maxKillDeficit;
    public readonly float maxLevelLeadLaneOpponent;
    public readonly float mejaisFullStackInTime;
    public readonly float moreEnemyJungleThanOpponent;
    public readonly float mostWardsDestroyedOneSweeper;
    public readonly float multiKillOneSpell;
    public readonly float multikills;
    public readonly float multikillsAfterAggressiveFlash;
    public readonly float multiTurretRiftHeraldCount;
    public readonly float mythicItemUsed;
    public readonly float outerTurretExecutesBefore10Minutes;
    public readonly float outnumberedKills;
    public readonly float outnumberedNexusKill;
    public readonly float perfectDragonSoulsTaken;
    public readonly float perfectGame;
    public readonly float pickKillWithAlly;
    public readonly float playedChampSelectPosition;
    public readonly float poroExplosions;
    public readonly float quickCleanse;
    public readonly float quickFirstTurret;
    public readonly float quickSoloKills;
    public readonly float riftHeraldTakedowns;
    public readonly float saveAllyFromDeath;
    public readonly float scuttleCrabKills;
    public readonly float shortestTimeToAceFromFirstTakedown;
    public readonly float skillshotsDodged;
    public readonly float skillshotsHit;
    public readonly float snowballsHit;
    public readonly float soloBaronKills;
    public readonly float soloTurretsLategame;
    public readonly float soloKills;
    public readonly float stealthWardsPlaced;
    public readonly float survivedSingleDigitHpCount;
    public readonly float survivedThreeImmobilizesInFight;
    public readonly float takedownOnFirstTurret;
    public readonly float takedowns;
    public readonly float takedownsAfterGainingLevelAdvantage;
    public readonly float takedownsBeforeJungleMinionSpawn;
    public readonly float takedownsFirstXminutes;
    public readonly float takedownsFirst25Minutes;
    public readonly float takedownsInAlcove;
    public readonly float takedownsInEnemyFountain;
    public readonly float teamBaronKills;
    public readonly float teamDamagePercentage;
    public readonly float teamElderDragonKills;
    public readonly float teamRiftHeraldKills;
    public readonly float teleportTakedowns;
    public readonly float thirdInhibitorDestroyedTime;
    public readonly float threeWardsOneSweeperCount;
    public readonly float tookLargeDamageSurvived;
    public readonly float turretPlatesTaken;
    public readonly float turretTakedowns;
    public readonly float turretsTakenWithRiftHerald;
    public readonly float twentyMinionsIn3SecondsCount;
    public readonly float twoWardsOneSweeperCount;
    public readonly float unseenRecalls;
    public readonly float visionScoreAdvantageLaneOpponent;
    public readonly float visionScorePerMinute;
    public readonly float wardTakedowns;
    public readonly float wardTakedownsBefore20M;
    public readonly float wardsGuarded;

    public ChallengesResponse(
        [JsonProperty("12assistStreakCount")] float twelveAssistStreakCount,
        [JsonProperty("abilityUses")] float abilityUses,
        [JsonProperty("acesBefore15Minutes")] float acesBefore15Minutes,
        [JsonProperty("alliedJungleMonsterKills")] float alliedJungleMonsterKills,
        [JsonProperty("baronBuffGoldAdvantageOverThreshold")] float baronBuffGoldAdvantageOverThreshold,
        [JsonProperty("baronTakedowns")] float baronTakedowns,
        [JsonProperty("blastConeOppositeOpponentCount")] float blastConeOppositeOpponentCount,
        [JsonProperty("bountyGold")] float bountyGold,
        [JsonProperty("buffsStolen")] float buffsStolen,
        [JsonProperty("completeSupportQuestInTime")] float completeSupportQuestInTime,
        [JsonProperty("controlWardsPlaced")] float controlWardsPlaced,
        [JsonProperty("controlWardTimeCoverageInRiverOrEnemyHalf")] float controlWardTimeCoverageInRiverOrEnemyHalf,
        [JsonProperty("damagePerMinute")] float damagePerMinute,
        [JsonProperty("damageTakenOnTeamPercentage")] float damageTakenOnTeamPercentage,
        [JsonProperty("dancedWithRiftHerald")] float dancedWithRiftHerald,
        [JsonProperty("deathsByEnemyChamps")] float deathsByEnemyChamps,
        [JsonProperty("dodgeSkillShotsSmallWindow")] float dodgeSkillShotsSmallWindow,
        [JsonProperty("doubleAces")] float doubleAces,
        [JsonProperty("dragonTakedowns")] float dragonTakedowns,
        [JsonProperty("earliestBaron")] float earliestBaron,
        [JsonProperty("earliestDragonTakedown")] float earliestDragonTakedown,
        [JsonProperty("earliestElderDragon")] float earliestElderDragon,
        [JsonProperty("earlyLaningPhaseGoldExpAdvantage")] float earlyLaningPhaseGoldExpAdvantage,
        [JsonProperty("effectiveHealAndShielding")] float effectiveHealAndShielding,
        [JsonProperty("elderDragonKillsWithOpposingSoul")] float elderDragonKillsWithOpposingSoul,
        [JsonProperty("elderDragonMultikills")] float elderDragonMultikills,
        [JsonProperty("enemyChampionImmobilizations")] float enemyChampionImmobilizations,
        [JsonProperty("enemyJungleMonsterKills")] float enemyJungleMonsterKills,
        [JsonProperty("epicMonsterKillsNearEnemyJungler")] float epicMonsterKillsNearEnemyJungler,
        [JsonProperty("epicMonsterKillsWithin30SecondsOfSpawn")] float epicMonsterKillsWithin30SecondsOfSpawn,
        [JsonProperty("epicMonsterSteals")] float epicMonsterSteals,
        [JsonProperty("epicMonsterStolenWithoutSmite")] float epicMonsterStolenWithoutSmite,
        [JsonProperty("fasterSupportQuestCompletion")] float fasterSupportQuestCompletion,
        [JsonProperty("fastestLegendary")] float fastestLegendary,
        [JsonProperty("firstTurretKilled")] float firstTurretKilled,
        [JsonProperty("firstTurretKilledTime")] float firstTurretKilledTime,
        [JsonProperty("flawlessAces")] float flawlessAces,
        [JsonProperty("fullTeamTakedown")] float fullTeamTakedown,
        [JsonProperty("gameLength")] float gameLength,
        [JsonProperty("getTakedownsInAllLanesEarlyJungleAsLaner")] float getTakedownsInAllLanesEarlyJungleAsLaner,
        [JsonProperty("goldPerMinute")] float goldPerMinute,
        [JsonProperty("hadAfkTeammate")] float hadAfkTeammate,
        [JsonProperty("hadOpenNexus")] float hadOpenNexus,
        [JsonProperty("highestChampionDamage")] float highestChampionDamage,
        [JsonProperty("highestCrowdControlScore")] float highestCrowdControlScore,
        [JsonProperty("highestWardKills")] float highestWardKills,
        [JsonProperty("immobilizeAndKillWithAlly")] float immobilizeAndKillWithAlly,
        [JsonProperty("initialBuffCount")] float initialBuffCount,
        [JsonProperty("initialCrabCount")] float initialCrabCount,
        [JsonProperty("jungleCsBefore10Minutes")] float jungleCsBefore10Minutes,
        [JsonProperty("junglerKillsEarlyJungle")] float junglerKillsEarlyJungle,
        [JsonProperty("junglerTakedownsNearDamagedEpicMonster")] float junglerTakedownsNearDamagedEpicMonster,
        [JsonProperty("kTurretsDestroyedBeforePlatesFall")] float kTurretsDestroyedBeforePlatesFall,
        [JsonProperty("kda")] float kda,
        [JsonProperty("killAfterHiddenWithAlly")] float killAfterHiddenWithAlly,
        [JsonProperty("killParticipation")] float killParticipation,
        [JsonProperty("killedChampTookFullTeamDamageSurvived")] float killedChampTookFullTeamDamageSurvived,
        [JsonProperty("killingSprees")] float killingSprees,
        [JsonProperty("killsNearEnemyTurret")] float killsNearEnemyTurret,
        [JsonProperty("killsOnLanersEarlyJungleAsJungler")] float killsOnLanersEarlyJungleAsJungler,
        [JsonProperty("killsOnOtherLanesEarlyJungleAsLaner")] float killsOnOtherLanesEarlyJungleAsLaner,
        [JsonProperty("killsOnRecentlyHealedByAramPack")] float killsOnRecentlyHealedByAramPack,
        [JsonProperty("killsUnderOwnTurret")] float killsUnderOwnTurret,
        [JsonProperty("killsWithHelpFromEpicMonster")] float killsWithHelpFromEpicMonster,
        [JsonProperty("knockEnemyIntoTeamAndKill")] float knockEnemyIntoTeamAndKill,
        [JsonProperty("landSkillShotsEarlyGame")] float landSkillShotsEarlyGame,
        [JsonProperty("laneMinionsFirst10Minutes")] float laneMinionsFirst10Minutes,
        [JsonProperty("laningPhaseGoldExpAdvantage")] float laningPhaseGoldExpAdvantage,
        [JsonProperty("legendaryCount")] float legendaryCount,
        [JsonProperty("lostAnInhibitor")] float lostAnInhibitor,
        [JsonProperty("maxCsAdvantageOnLaneOpponent")] float maxCsAdvantageOnLaneOpponent,
        [JsonProperty("maxKillDeficit")] float maxKillDeficit,
        [JsonProperty("maxLevelLeadLaneOpponent")] float maxLevelLeadLaneOpponent,
        [JsonProperty("mejaisFullStackInTime")] float mejaisFullStackInTime,
        [JsonProperty("moreEnemyJungleThanOpponent")] float moreEnemyJungleThanOpponent,
        [JsonProperty("mostWardsDestroyedOneSweeper")] float mostWardsDestroyedOneSweeper,
        [JsonProperty("multiKillOneSpell")] float multiKillOneSpell,
        [JsonProperty("multikills")] float multikills,
        [JsonProperty("multikillsAfterAggressiveFlash")] float multikillsAfterAggressiveFlash,
        [JsonProperty("multiTurretRiftHeraldCount")] float multiTurretRiftHeraldCount,
        [JsonProperty("mythicItemUsed")] float mythicItemUsed,
        [JsonProperty("outerTurretExecutesBefore10Minutes")] float outerTurretExecutesBefore10Minutes,
        [JsonProperty("outnumberedKills")] float outnumberedKills,
        [JsonProperty("outnumberedNexusKill")] float outnumberedNexusKill,
        [JsonProperty("perfectDragonSoulsTaken")] float perfectDragonSoulsTaken,
        [JsonProperty("perfectGame")] float perfectGame,
        [JsonProperty("pickKillWithAlly")] float pickKillWithAlly,
        [JsonProperty("playedChampSelectPosition")] float playedChampSelectPosition,
        [JsonProperty("poroExplosions")] float poroExplosions,
        [JsonProperty("quickCleanse")] float quickCleanse,
        [JsonProperty("quickFirstTurret")] float quickFirstTurret,
        [JsonProperty("quickSoloKills")] float quickSoloKills,
        [JsonProperty("riftHeraldTakedowns")] float riftHeraldTakedowns,
        [JsonProperty("saveAllyFromDeath")] float saveAllyFromDeath,
        [JsonProperty("scuttleCrabKills")] float scuttleCrabKills,
        [JsonProperty("shortestTimeToAceFromFirstTakedown")] float shortestTimeToAceFromFirstTakedown,
        [JsonProperty("skillshotsDodged")] float skillshotsDodged,
        [JsonProperty("skillshotsHit")] float skillshotsHit,
        [JsonProperty("snowballsHit")] float snowballsHit,
        [JsonProperty("soloBaronKills")] float soloBaronKills,
        [JsonProperty("soloTurretsLategame")] float soloTurretsLategame,
        [JsonProperty("soloKills")] float soloKills,
        [JsonProperty("stealthWardsPlaced")] float stealthWardsPlaced,
        [JsonProperty("survivedSingleDigitHpCount")] float survivedSingleDigitHpCount,
        [JsonProperty("survivedThreeImmobilizesInFight")] float survivedThreeImmobilizesInFight,
        [JsonProperty("takedownOnFirstTurret")] float takedownOnFirstTurret,
        [JsonProperty("takedowns")] float takedowns,
        [JsonProperty("takedownsAfterGainingLevelAdvantage")] float takedownsAfterGainingLevelAdvantage,
        [JsonProperty("takedownsBeforeJungleMinionSpawn")] float takedownsBeforeJungleMinionSpawn,
        [JsonProperty("takedownsFirstXminutes")] float takedownsFirstXminutes,
        [JsonProperty("takedownsFirst25Minutes")] float takedownsFirst25Minutes,
        [JsonProperty("takedownsInAlcove")] float takedownsInAlcove,
        [JsonProperty("takedownsInEnemyFountain")] float takedownsInEnemyFountain,
        [JsonProperty("teamBaronKills")] float teamBaronKills,
        [JsonProperty("teamDamagePercentage")] float teamDamagePercentage,
        [JsonProperty("teamElderDragonKills")] float teamElderDragonKills,
        [JsonProperty("teamRiftHeraldKills")] float teamRiftHeraldKills,
        [JsonProperty("teleportTakedowns")] float teleportTakedowns,
        [JsonProperty("thirdInhibitorDestroyedTime")] float thirdInhibitorDestroyedTime,
        [JsonProperty("threeWardsOneSweeperCount")] float threeWardsOneSweeperCount,
        [JsonProperty("tookLargeDamageSurvived")] float tookLargeDamageSurvived,
        [JsonProperty("turretPlatesTaken")] float turretPlatesTaken,
        [JsonProperty("turretTakedowns")] float turretTakedowns,
        [JsonProperty("turretsTakenWithRiftHerald")] float turretsTakenWithRiftHerald,
        [JsonProperty("twentyMinionsIn3SecondsCount")] float twentyMinionsIn3SecondsCount,
        [JsonProperty("twoWardsOneSweeperCount")] float twoWardsOneSweeperCount,
        [JsonProperty("unseenRecalls")] float unseenRecalls,
        [JsonProperty("visionScoreAdvantageLaneOpponent")] float visionScoreAdvantageLaneOpponent,
        [JsonProperty("visionScorePerMinute")] float visionScorePerMinute,
        [JsonProperty("wardTakedowns")] float wardTakedowns,
        [JsonProperty("wardTakedownsBefore20M")] float wardTakedownsBefore20M,
        [JsonProperty("wardsGuarded")] float wardsGuarded
    ) {
        this.twelveAssistStreakCount = twelveAssistStreakCount;
        this.abilityUses = abilityUses;
        this.acesBefore15Minutes = acesBefore15Minutes;
        this.alliedJungleMonsterKills = alliedJungleMonsterKills;
        this.baronBuffGoldAdvantageOverThreshold = baronBuffGoldAdvantageOverThreshold;
        this.baronTakedowns = baronTakedowns;
        this.blastConeOppositeOpponentCount = blastConeOppositeOpponentCount;
        this.bountyGold = bountyGold;
        this.buffsStolen = buffsStolen;
        this.completeSupportQuestInTime = completeSupportQuestInTime;
        this.controlWardsPlaced = controlWardsPlaced;
        this.controlWardTimeCoverageInRiverOrEnemyHalf = controlWardTimeCoverageInRiverOrEnemyHalf;
        this.damagePerMinute = damagePerMinute;
        this.damageTakenOnTeamPercentage = damageTakenOnTeamPercentage;
        this.dancedWithRiftHerald = dancedWithRiftHerald;
        this.deathsByEnemyChamps = deathsByEnemyChamps;
        this.dodgeSkillShotsSmallWindow = dodgeSkillShotsSmallWindow;
        this.doubleAces = doubleAces;
        this.dragonTakedowns = dragonTakedowns;
        this.earliestBaron = earliestBaron;
        this.earliestDragonTakedown = earliestDragonTakedown;
        this.earliestElderDragon = earliestElderDragon;
        this.earlyLaningPhaseGoldExpAdvantage = earlyLaningPhaseGoldExpAdvantage;
        this.effectiveHealAndShielding = effectiveHealAndShielding;
        this.elderDragonKillsWithOpposingSoul = elderDragonKillsWithOpposingSoul;
        this.elderDragonMultikills = elderDragonMultikills;
        this.enemyChampionImmobilizations = enemyChampionImmobilizations;
        this.enemyJungleMonsterKills = enemyJungleMonsterKills;
        this.epicMonsterKillsNearEnemyJungler = epicMonsterKillsNearEnemyJungler;
        this.epicMonsterKillsWithin30SecondsOfSpawn = epicMonsterKillsWithin30SecondsOfSpawn;
        this.epicMonsterSteals = epicMonsterSteals;
        this.epicMonsterStolenWithoutSmite = epicMonsterStolenWithoutSmite;
        this.fasterSupportQuestCompletion = fasterSupportQuestCompletion;
        this.fastestLegendary = fastestLegendary;
        this.firstTurretKilled = firstTurretKilled;
        this.firstTurretKilledTime = firstTurretKilledTime;
        this.flawlessAces = flawlessAces;
        this.fullTeamTakedown = fullTeamTakedown;
        this.gameLength = gameLength;
        this.getTakedownsInAllLanesEarlyJungleAsLaner = getTakedownsInAllLanesEarlyJungleAsLaner;
        this.goldPerMinute = goldPerMinute;
        this.hadAfkTeammate = hadAfkTeammate;
        this.hadOpenNexus = hadOpenNexus;
        this.highestChampionDamage = highestChampionDamage;
        this.highestCrowdControlScore = highestCrowdControlScore;
        this.highestWardKills = highestWardKills;
        this.immobilizeAndKillWithAlly = immobilizeAndKillWithAlly;
        this.initialBuffCount = initialBuffCount;
        this.initialCrabCount = initialCrabCount;
        this.jungleCsBefore10Minutes = jungleCsBefore10Minutes;
        this.junglerKillsEarlyJungle = junglerKillsEarlyJungle;
        this.junglerTakedownsNearDamagedEpicMonster = junglerTakedownsNearDamagedEpicMonster;
        this.kTurretsDestroyedBeforePlatesFall = kTurretsDestroyedBeforePlatesFall;
        this.kda = kda;
        this.killAfterHiddenWithAlly = killAfterHiddenWithAlly;
        this.killParticipation = killParticipation;
        this.killedChampTookFullTeamDamageSurvived = killedChampTookFullTeamDamageSurvived;
        this.killingSprees = killingSprees;
        this.killsNearEnemyTurret = killsNearEnemyTurret;
        this.killsOnLanersEarlyJungleAsJungler = killsOnLanersEarlyJungleAsJungler;
        this.killsOnOtherLanesEarlyJungleAsLaner = killsOnOtherLanesEarlyJungleAsLaner;
        this.killsOnRecentlyHealedByAramPack = killsOnRecentlyHealedByAramPack;
        this.killsUnderOwnTurret = killsUnderOwnTurret;
        this.killsWithHelpFromEpicMonster = killsWithHelpFromEpicMonster;
        this.knockEnemyIntoTeamAndKill = knockEnemyIntoTeamAndKill;
        this.landSkillShotsEarlyGame = landSkillShotsEarlyGame;
        this.laneMinionsFirst10Minutes = laneMinionsFirst10Minutes;
        this.laningPhaseGoldExpAdvantage = laningPhaseGoldExpAdvantage;
        this.legendaryCount = legendaryCount;
        this.lostAnInhibitor = lostAnInhibitor;
        this.maxCsAdvantageOnLaneOpponent = maxCsAdvantageOnLaneOpponent;
        this.maxKillDeficit = maxKillDeficit;
        this.maxLevelLeadLaneOpponent = maxLevelLeadLaneOpponent;
        this.mejaisFullStackInTime = mejaisFullStackInTime;
        this.moreEnemyJungleThanOpponent = moreEnemyJungleThanOpponent;
        this.mostWardsDestroyedOneSweeper = mostWardsDestroyedOneSweeper;
        this.multiKillOneSpell = multiKillOneSpell;
        this.multikills = multikills;
        this.multikillsAfterAggressiveFlash = multikillsAfterAggressiveFlash;
        this.multiTurretRiftHeraldCount = multiTurretRiftHeraldCount;
        this.mythicItemUsed = mythicItemUsed;
        this.outerTurretExecutesBefore10Minutes = outerTurretExecutesBefore10Minutes;
        this.outnumberedKills = outnumberedKills;
        this.outnumberedNexusKill = outnumberedNexusKill;
        this.perfectDragonSoulsTaken = perfectDragonSoulsTaken;
        this.perfectGame = perfectGame;
        this.pickKillWithAlly = pickKillWithAlly;
        this.playedChampSelectPosition = playedChampSelectPosition;
        this.poroExplosions = poroExplosions;
        this.quickCleanse = quickCleanse;
        this.quickFirstTurret = quickFirstTurret;
        this.quickSoloKills = quickSoloKills;
        this.riftHeraldTakedowns = riftHeraldTakedowns;
        this.saveAllyFromDeath = saveAllyFromDeath;
        this.scuttleCrabKills = scuttleCrabKills;
        this.shortestTimeToAceFromFirstTakedown = shortestTimeToAceFromFirstTakedown;
        this.skillshotsDodged = skillshotsDodged;
        this.skillshotsHit = skillshotsHit;
        this.snowballsHit = snowballsHit;
        this.soloBaronKills = soloBaronKills;
        this.soloTurretsLategame = soloTurretsLategame;
        this.soloKills = soloKills;
        this.stealthWardsPlaced = stealthWardsPlaced;
        this.survivedSingleDigitHpCount = survivedSingleDigitHpCount;
        this.survivedThreeImmobilizesInFight = survivedThreeImmobilizesInFight;
        this.takedownOnFirstTurret = takedownOnFirstTurret;
        this.takedowns = takedowns;
        this.takedownsAfterGainingLevelAdvantage = takedownsAfterGainingLevelAdvantage;
        this.takedownsBeforeJungleMinionSpawn = takedownsBeforeJungleMinionSpawn;
        this.takedownsFirstXminutes = takedownsFirstXminutes;
        this.takedownsFirst25Minutes = takedownsFirst25Minutes;
        this.takedownsInAlcove = takedownsInAlcove;
        this.takedownsInEnemyFountain = takedownsInEnemyFountain;
        this.teamBaronKills = teamBaronKills;
        this.teamDamagePercentage = teamDamagePercentage;
        this.teamElderDragonKills = teamElderDragonKills;
        this.teamRiftHeraldKills = teamRiftHeraldKills;
        this.teleportTakedowns = teleportTakedowns;
        this.thirdInhibitorDestroyedTime = thirdInhibitorDestroyedTime;
        this.threeWardsOneSweeperCount = threeWardsOneSweeperCount;
        this.tookLargeDamageSurvived = tookLargeDamageSurvived;
        this.turretPlatesTaken = turretPlatesTaken;
        this.turretTakedowns = turretTakedowns;
        this.turretsTakenWithRiftHerald = turretsTakenWithRiftHerald;
        this.twentyMinionsIn3SecondsCount = twentyMinionsIn3SecondsCount;
        this.twoWardsOneSweeperCount = twoWardsOneSweeperCount;
        this.unseenRecalls = unseenRecalls;
        this.visionScoreAdvantageLaneOpponent = visionScoreAdvantageLaneOpponent;
        this.visionScorePerMinute = visionScorePerMinute;
        this.wardTakedowns = wardTakedowns;
        this.wardTakedownsBefore20M = wardTakedownsBefore20M;
        this.wardsGuarded = wardsGuarded;
    }
}