using System;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using YordleYelper.bot.data;
using YordleYelper.bot.data_fetcher.data_dragon.responses;
using YordleYelper.bot.data_fetcher.league_api.responses;
using YordleYelper.bot.extensions;
using YordleYelper.bot.response_creator;

namespace YordleYelper.bot.commands.masteries; 

public static class MasteryEmbedCreator {
    public static DiscordEmbedBuilder CreateChampionMasteryMessage(
        InteractionContext context,
        long totalMasteryPoints,
        ChampionMasteryResponse mastery,
        BasicChampionInfo championInfo
    ) {
        double masteryPercentage = Math.Round(mastery.championPoints / (double)totalMasteryPoints * 100, 2);
        string showMasteryPercentage = (masteryPercentage < 0.1f ? "< 0.1" : masteryPercentage.ToString("0.0")) + "%";
        
        return context.CreateCommandEmbedBuilderOk(b => b
            .WithDescription($"The mastery statistics for {championInfo.Name.ToBold()}\n\u200B")
            .AddField("Level", Emote.NumberToEmoteString(mastery.championLevel), true)
            .AddField("Points", mastery.championPoints.SpaceSeparatedNumber(), true)
            .AddField("Points Next Lvl.", mastery.championPointsUntilNextLevel.SpaceSeparatedNumber(), true)
            .AddField("Status", mastery.championLevel == 7 ? Emote.SPARKLES + "Mastered" + Emote.SPARKLES : $"{mastery.tokensEarned} Token{(mastery.tokensEarned > 1 ? "s" : "")}", true)
             .AddField("Mastery Percentage", showMasteryPercentage, true)
            .AddField("Chest", Emote.FromBool(mastery.chestGranted).ToString(), true)
            .AddField("Last Played", (DateTime.Now - mastery.lastPlayed).ToTimeSinceString() + " ago")
            .WithThumbnail(championInfo.PortraitImageUrl)
        );
    }
}