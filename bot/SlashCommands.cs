using System;
using System.Threading.Tasks;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.Logging;
using YordleYelper.bot.commands;
using YordleYelper.bot.commands.last_played;
using YordleYelper.bot.commands.masteries;
using YordleYelper.bot.data;
using YordleYelper.bot.data_fetcher.data_dragon;
using YordleYelper.bot.data_fetcher.data_dragon.responses;
using YordleYelper.bot.data_fetcher.data_dragon.responses.items;
using YordleYelper.bot.data_fetcher.league_api;
using YordleYelper.bot.data_fetcher.league_api.responses;
using YordleYelper.bot.response_creator;

namespace YordleYelper.bot; 

public class SlashCommands : ApplicationCommandModule {
    public static DataDragonProxy DataDragonProxy;
    public static LeagueApiProxy LeagueApiProxy;
    public static ILogger Logger;
    
    [SlashCommand("champion", "General overview of a champion: Name, title, lore and tips!")]
    public async Task Champion(
        InteractionContext context, 
        [Option("champion", "Champion name.")] string championName
    ) {
        LogCommandCall(context, championName);
        if (!DataDragonProxy.TryGetBasicChampionInfo(championName, out BasicChampionInfo basicInfo)) {
            await context.NoSuchChampionResponse();
            return;
        }
        
        await Run(context, new ChampionCommand(basicInfo, DataDragonProxy));
    }
    
    [SlashCommand("ability", "Detailed information about a champion ability!")]
    public async Task Ability(
        InteractionContext context, 
        [Option("champion", "Champion name.")] string championName,
        [Option("ability", "Ability Type.")] ChampionAbility championAbility
    ) {
        LogCommandCall(context, championName, championAbility);
        if (!DataDragonProxy.TryGetBasicChampionInfo(championName, out BasicChampionInfo basicInfo)) {
            await context.NoSuchChampionResponse();
            return;
        }

        await Run(context, new AbilityCommand(basicInfo, championAbility, DataDragonProxy));
    }
    
    [SlashCommand("item", "Detailed information about an item!")]
    public async Task Item(
        InteractionContext context, 
        [Option("item", "Item name.")] string itemName
    ) {
        LogCommandCall(context, itemName);
        if (!DataDragonProxy.TryGetItemInfo(itemName, out ItemInfo itemInfo)) {
            await context.NoSuchItemResponse();
            return;
        }
        
        await Run(context, new ItemCommand(itemInfo, DataDragonProxy));
    }
    
    [SlashCommand("lastplayed", "Shows when a player last played a given champion!")]
    public async Task LastPlayed(
        InteractionContext context, 
        [Option("riotId", "Riot Id.")] string riotId,
        [Option("champion", "Champion name.")] string championName
    ) {
        LogCommandCall(context, riotId, championName);
        if (!LeagueApiProxy.TryGetLeagueAccount(riotId, out LeagueAccount leagueAccount)) {
            await context.NoSuchRiotIdResponse();
            return;
        }
        
        if (!DataDragonProxy.TryGetBasicChampionInfo(championName, out BasicChampionInfo champion)) {
            await context.NoSuchChampionResponse();
            return;
        }

        await Run(context, new LastPlayedCommand(leagueAccount, champion, LeagueApiProxy));
    }

    [SlashCommand("lastplayedmultiple", "Shows when a player last played a multitude of champions!")]
    public async Task LastPlayedMultiple(
        InteractionContext context, 
        [Option("riotId", "Riot Id.")] string riotId,
        [Option("amount", "Amount of champions to display.")] long amountToShow = 25,
        [Option("sortOrder", "Order to sort champions in.")] SortOrder sortOrder = SortOrder.Ascending
    ) {
        LogCommandCall(context, riotId, amountToShow, sortOrder);
        amountToShow = Math.Max(amountToShow, 1);
        
        if (!LeagueApiProxy.TryGetLeagueAccount(riotId, out LeagueAccount leagueAccount)) {
            await context.NoSuchRiotIdResponse();
            return;
        }

        await Run(context, new LastPlayedMultipleCommand(leagueAccount, DataDragonProxy.AllChampionBasicInfos, (int)amountToShow, sortOrder, LeagueApiProxy));
    }
    
    [SlashCommand("masteryMultiple", "List of champions with most mastery points by a summoner!")]
    public async Task MasteryMultiple(
        InteractionContext context, 
        [Option("riotId", "Riot Id.")] string riotId,
        [Option("amount", "Amount of top champions.")] long amount = 5,
        [Option("filterMastered", "Should filter out mastered champions?")] bool filterOutMastered = false
    ) {
        LogCommandCall(context, riotId, amount, filterOutMastered);
        amount = Math.Min(Math.Max(amount, 1), 10);
        if (!LeagueApiProxy.TryGetLeagueAccount(riotId, out LeagueAccount leagueAccount)) {
            await context.NoSuchRiotIdResponse();
            return;
        }

        await Run(context, new MasteryMultipleCommand(leagueAccount, amount, filterOutMastered, DataDragonProxy.AllChampionBasicInfos, LeagueApiProxy));
    }
    
    [SlashCommand("mastery", "Provides mastery statistics for specific champion!")]
    public async Task Mastery(
        InteractionContext context, 
        [Option("riotId", "Riot Id.")] string riotId,
        [Option("champion", "Champion name.")] string championName
    ) {
        LogCommandCall(context, riotId, championName);
        if (!LeagueApiProxy.TryGetLeagueAccount(riotId, out LeagueAccount leagueAccount)) {
            await context.NoSuchRiotIdResponse();
            return;
        }
        
        if (!DataDragonProxy.TryGetBasicChampionInfo(championName, out BasicChampionInfo champion)) {
            await context.NoSuchChampionResponse();
            return;
        }

        await Run(context, new MasteryCommand(leagueAccount, champion, LeagueApiProxy));
    }
    
    [SlashCommand("masterydistribution", "Provides a pie chart over mastery points per champion!")]
    public async Task MasteryDistribution(
        InteractionContext context, 
        [Option("riotId", "Riot Id.")] string riotId,
        [Option("showAvailableChests", "Should highlight those champions which have a mastery chest available?")] bool showAvailableChests = false
    ) {
        LogCommandCall(context, riotId);
        if (!LeagueApiProxy.TryGetLeagueAccount(riotId, out LeagueAccount leagueAccount)) {
            await context.NoSuchRiotIdResponse();
            return;
        }

        await Run(context, new MasteryDistributionCommand(leagueAccount, showAvailableChests, DataDragonProxy.AllChampionBasicInfos, LeagueApiProxy));
    }

    private static async Task Run(InteractionContext context, CommandBase commandBase) {
        await commandBase.Execute(context);
    }
    
    private static void LogCommandCall(InteractionContext context, params object[] parameters) {
        Logger.LogInformation($"Command: {context.CommandName} was called with parameters: [{string.Join(", ", parameters)}] by user: {context.User.Username}");
    }
}