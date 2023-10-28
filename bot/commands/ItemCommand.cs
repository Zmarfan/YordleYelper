using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.data_fetcher.responses.items;
using YordleYelper.bot.response_creator;

namespace YordleYelper.bot.commands; 

public class ItemCommand : CommandBase {
    private readonly ItemInfo _itemInfo;

    public ItemCommand(ItemInfo itemInfo) {
        _itemInfo = itemInfo;
    }

    protected override async Task Run(InteractionContext context) {
        await context.CreateCommandOk(new DiscordEmbedBuilder()
            .WithThumbnail(_itemInfo.iconUrl)
        );
    }
}