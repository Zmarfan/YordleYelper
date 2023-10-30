using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.commands;
using YordleYelper.bot.data;
using YordleYelper.bot.data_fetcher.data_dragon;
using YordleYelper.bot.data_fetcher.data_dragon.responses;
using YordleYelper.bot.data_fetcher.data_dragon.responses.items;
using YordleYelper.bot.data_fetcher.league_api;
using YordleYelper.bot.data_fetcher.league_api.data;
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
        
        await new ChampionCommand(basicInfo, DataDragonProxy).Execute(context);
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
        
        await new AbilityCommand(basicInfo, championAbility, DataDragonProxy).Execute(context);
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
        
        await new ItemCommand(itemInfo, DataDragonProxy).Execute(context);
    }
    
    [SlashCommand("lastplayed", "Shows when a player last played a given champion!")]
    public async Task LastPlayed(
        InteractionContext context, 
        [Option("riotId", "Riot Id.")] string riotId,
        [Option("champion", "Champion name.")] string championName
    ) {
        if (!TryGetLeagueAccount(riotId, out LeagueAccount leagueAccount)) {
            await context.NoSuchRiotIdResponse();
            return;
        }
        
        if (!DataDragonProxy.TryGetBasicChampionInfo(championName, out BasicChampionInfo champion)) {
            await context.NoSuchChampionResponse();
            return;
        }

        await new LastPlayedCommand(leagueAccount, champion, LeagueApiProxy).Execute(context);
    }

    [SlashCommand("lastplayedmultiple", "Shows when a player last played a multitude of champions!")]
    public async Task LastPlayedMultiple(
        InteractionContext context, 
        [Option("riotId", "Riot Id.")] string riotId,
        [Option("amount", "Amount of champions to display.")] long amountToShow = 500,
        [Option("sortOrder", "Order to sort champions in.")] SortOrder sortOrder = SortOrder.Ascending
    ) {
        if (!TryGetLeagueAccount(riotId, out LeagueAccount leagueAccount)) {
            await context.NoSuchRiotIdResponse();
            return;
        }

        await new LastPlayedAllCommand(leagueAccount, DataDragonProxy.AllChampionBasicInfos, (int)amountToShow, sortOrder, LeagueApiProxy, DataDragonProxy).Execute(context);
    }
    
    private static bool TryGetLeagueAccount(string riotId, out LeagueAccount leagueAccount) {
        if (!LeagueApiProxy.TryGetPuuidByRiotId(riotId, out Puuid puuid)) {
            leagueAccount = default;
            return false;
        }

        leagueAccount = LeagueApiProxy.GetLeagueAccountByPuuid(puuid);
        return true;
    }
}