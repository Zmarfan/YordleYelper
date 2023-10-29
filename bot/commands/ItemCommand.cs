using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.data_fetcher;
using YordleYelper.bot.data_fetcher.data_dragon;
using YordleYelper.bot.data_fetcher.data_dragon.responses.items;
using YordleYelper.bot.extensions;
using YordleYelper.bot.response_creator;

namespace YordleYelper.bot.commands; 

public class ItemCommand : CommandBase {
    private readonly ItemInfo _itemInfo;
    private readonly DataDragonProxy _dataDragonProxy;

    public ItemCommand(ItemInfo itemInfo, DataDragonProxy dataDragonProxy) {
        _itemInfo = itemInfo;
        _dataDragonProxy = dataDragonProxy;
    }

    protected override async Task Run(InteractionContext context) {
        DiscordEmbedBuilder embed = new DiscordEmbedBuilder()
            .WithDescription($"Details for {_itemInfo.response.Name} item:")
            .AddExtraLargeField("Name:", _itemInfo.response.Name, true);

        if (_itemInfo.response.GoldResponse.Total != 0) {
            embed.AddExtraLargeField("Cost:", $"{Emote.GOLD} {_itemInfo.response.GoldResponse.Total}", true);
        }
        if (_itemInfo.response.GoldResponse.Sell != 0) {
            embed.AddExtraLargeField("Sell:", $"{Emote.GOLD} {_itemInfo.response.GoldResponse.Sell}", true);
        }
        embed.AddExtraLargeField("Purchasable:", Emote.FromBool(_itemInfo.response.GoldResponse.Purchasable).ToString(), true);
        if (_itemInfo.response.RequiredChampion != null) {
            embed.AddExtraLargeField("Champion:", $"{_itemInfo.response.RequiredChampion}", true);
        }
        if (_itemInfo.response.RequiredAlly != null) {
            embed.AddExtraLargeField("Requires:", _itemInfo.response.RequiredAlly, true);
        }

        if (embed.Fields.Count == 5) {
            embed.AddEmptyField(true);
        }

        string stats = CreateStats();
        if (stats != string.Empty) {
            embed.AddExtraLargeField("Stats:", stats);
        }

        string description = CreateDescription();
        if (description != string.Empty) {
            embed.AddExtraLargeField("Description:", CreateDescription());
        }

        if (!_itemInfo.response.FromItemsIds.NullOrEmpty()) {
            embed.AddField("Builds From:", CreateItemList(_itemInfo.response.FromItemsIds));
        }
        if (!_itemInfo.response.IntoItemsIds.NullOrEmpty()) {
            embed.AddField("Builds Into:", CreateItemList(_itemInfo.response.IntoItemsIds));
        }

        await context.CreateCommandOk(_ => embed.WithThumbnail(_itemInfo.iconUrl));
    }

    private string CreateStats() {
        string content = _itemInfo.response.Description;
        int startIndex = content.IndexOf("<stats>", StringComparison.Ordinal);
        int endIndex = content.IndexOf("</stats>", StringComparison.Ordinal);
        return content.Substring(startIndex + 7, endIndex - startIndex - 7)
            .Split(new[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries)
            .ToString((acc, stat) => acc.AppendLine(stat.FormatLeagueStat()));
    }
    
    private string CreateDescription() {
        string content = _itemInfo.response.Description;
        int startIndex = content.IndexOf("</stats>", StringComparison.Ordinal);
        int endIndex = content.IndexOf("</mainText>", StringComparison.Ordinal);
        return content.Substring(startIndex + 8, endIndex - startIndex - 8).FormatLeagueTextForEmbed();
    }
    
    private string CreateItemList(IEnumerable<string> itemIds) {
        return string.Join(", ", _dataDragonProxy.ItemNamesFromIds(itemIds));
    }
}