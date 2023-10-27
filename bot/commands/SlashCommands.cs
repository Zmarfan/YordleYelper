using System.Threading.Tasks;
using DSharpPlus.SlashCommands;

namespace YordleYelper.bot.commands; 

public class SlashCommands : ApplicationCommandModule {
    [SlashCommand("champion", "todo")]
    public async Task Champion(
        InteractionContext context, 
        [Option("name", "todo")] string championName
    ) {
        await new ChampionCommand(championName).Execute(context);
    }
}