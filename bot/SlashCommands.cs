using System.Threading.Tasks;
using DSharpPlus.SlashCommands;
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
    
    [SlashCommand("champion", "General overview of a champion: Name, title, lore and tips!")]
    public async Task Champion(
        InteractionContext context, 
        [Option("champion", "Champion name.")] string championName
    ) {
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
        if (!LeagueApiProxy.TryGetLeagueAccount(riotId, out LeagueAccount leagueAccount)) {
            await context.NoSuchRiotIdResponse();
            return;
        }

        await Run(context, new LastPlayedMultipleCommand(leagueAccount, DataDragonProxy.AllChampionBasicInfos, (int)amountToShow, sortOrder, LeagueApiProxy));
    }
    
    [SlashCommand("mastery", "List of champions with most mastery points by a summoner.!")]
    public async Task Mastery(
        InteractionContext context, 
        [Option("riotId", "Riot Id.")] string riotId,
        [Option("amount", "Amount of top champions.")] long amount = 5
    ) {
        if (!LeagueApiProxy.TryGetLeagueAccount(riotId, out LeagueAccount leagueAccount)) {
            await context.NoSuchRiotIdResponse();
            return;
        }

        await Run(context, new MasteryMultipleCommand(leagueAccount, amount, DataDragonProxy.AllChampionBasicInfos, LeagueApiProxy));
    }

    private static async Task Run(InteractionContext context, CommandBase commandBase) {
        await commandBase.Execute(context);
    }
}