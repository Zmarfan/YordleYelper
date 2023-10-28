using System.Threading.Tasks;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.commands.choices;
using YordleYelper.bot.data_fetcher;
using YordleYelper.bot.data_fetcher.responses;
using YordleYelper.bot.data_fetcher.responses.items;
using YordleYelper.bot.response_creator;

namespace YordleYelper.bot.commands; 

public class SlashCommands : ApplicationCommandModule {
    public static DataDragonProxy DataDragonProxy;
    
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
        
        await new ItemCommand(itemInfo).Execute(context);
    }
}