using System.Threading.Tasks;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.data_fetcher;

namespace YordleYelper.bot.commands; 

public class SlashCommands : ApplicationCommandModule {
    public DataDragonProxy DataDragonProxy { private get; set; }
    
    [SlashCommand("champion", "todo")]
    public async Task Champion(
        InteractionContext context, 
        [Option("name", "todo")] string championName
    ) {
        await new ChampionCommand(DataDragonProxy, championName).Execute(context);
    }
}