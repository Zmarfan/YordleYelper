using System.Threading.Tasks;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.data_fetcher;

namespace YordleYelper.bot.commands; 

public class SlashCommands : ApplicationCommandModule {
    public DataDragonProxy DataDragonProxy { private get; set; }
    
    [SlashCommand("champion", "General overview of a champion: Name, title, lore and tips!")]
    public async Task Champion(
        InteractionContext context, 
        [Option("champion", "Champion name.")] string championName
    ) {
        await new ChampionCommand(DataDragonProxy, championName).Execute(context);
    }
}