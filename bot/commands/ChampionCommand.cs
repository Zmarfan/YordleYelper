﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.data_fetcher.responses;
using YordleYelper.bot.data_fetcher.responses.champion_info;
using YordleYelper.bot.response_creator;

namespace YordleYelper.bot.commands; 

public class ChampionCommand : CommandBase {
    public const string BUTTON_UNIQUE_IDENTIFIER = "ChampionCommandDidYouMean";
    private const string COMMAND_NAME = "YordleYelper Champion Command";
    
    private readonly string _championName;
    
    public ChampionCommand(string championName) {
        _championName = championName;
    }

    protected override async Task Run(InteractionContext context) {
        if (!SlashCommands.DATA_FETCHER.DataDragonProxy.TryGetBasicChampionInfo(_championName, out BasicChampionInfo basicInfo)) {
            await NoSuchChampionResponse(context);
            return;
        }

        TopChampionInfoResponse fullInfo = SlashCommands.DATA_FETCHER.DataDragonProxy.GetChampionInfo(basicInfo);
        
        await context.CreateResponse(new DiscordEmbedBuilder()
            .WithTitle($":sparkles: {COMMAND_NAME}")
            .WithDescription($"""
:small_blue_diamond: **Name:** {fullInfo.Data.Name}

:small_blue_diamond: **Title:** {fullInfo.Data.Title}

:small_blue_diamond: **Lore:** {fullInfo.Data.Lore}

:small_blue_diamond: **Ally Tips:**

{fullInfo.Data.AllyTips.Aggregate(new StringBuilder(), (acc, tip) => acc.AppendLine($":white_small_square: {tip}\n"))}
:small_orange_diamond: **Enemy Tips:**

{fullInfo.Data.EnemyTips.Aggregate(new StringBuilder(), (acc, tip) => acc.AppendLine($":white_small_square: {tip}\n"))}
""")
            .WithThumbnail(fullInfo.PortraitImageUrl)
        );
    }

    private async Task NoSuchChampionResponse(BaseContext context) {
        string similarChampionName = SlashCommands.DATA_FETCHER.DataDragonProxy.GetMostSimilarChampionBasicInfo(_championName).Name;
        await context.CreateResponse(new DiscordEmbedBuilder()
            .WithTitle($":question: {COMMAND_NAME}")
            .WithDescription($"Provided champion does not exist, did you mean **{similarChampionName}**?")
        );
    }
}