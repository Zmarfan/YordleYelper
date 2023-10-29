﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.data_fetcher.responses.items;
using YordleYelper.bot.extensions;
using YordleYelper.bot.response_creator;

namespace YordleYelper.bot.commands; 

public class ItemCommand : CommandBase {
    private readonly ItemInfo _itemInfo;

    public ItemCommand(ItemInfo itemInfo) {
        _itemInfo = itemInfo;
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

        string stats = CreateStats();
        if (stats != string.Empty) {
            embed.AddField("Stats:", stats);
        }
        
        embed
            .AddExtraLargeField("Description:", CreateDescription())
            .AddField("Builds Into:", CreateBuildsInto())
            .WithThumbnail(_itemInfo.iconUrl);

        await context.CreateCommandOk(embed);
    }

    private string CreateStats() {
        // TODO: fix so that stat effects have their emotes
        // TODO: get right items depending on map
        
        string content = _itemInfo.response.Description;
        int startIndex = content.IndexOf("<stats>", StringComparison.Ordinal);
        int endIndex = content.IndexOf("</stats>", StringComparison.Ordinal);
        return content.Substring(startIndex + 7, endIndex - startIndex - 7)
            .Split(new[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries)
            .ToString((acc, stat) => acc.AppendListEntry(Emote.BULLET_WHITE, stat.FormatLeagueTextForEmbed()));
    }
    
    private string CreateDescription() {
        string content = _itemInfo.response.Description;
        int startIndex = content.IndexOf("</stats>", StringComparison.Ordinal);
        int endIndex = content.IndexOf("</mainText>", StringComparison.Ordinal);

        string description = content.Substring(startIndex + 8, endIndex - startIndex - 8).FormatLeagueTextForEmbed();
        return description == string.Empty ? "None." : description;
    }
    
    private string CreateBuildsInto() {
        // TODO: this
        return "-";
    }
}