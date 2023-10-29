using YordleYelper.bot.data;
using YordleYelper.bot.extensions;

namespace YordleYelper.bot.data_fetcher.data_dragon.responses.champion_info; 

public struct AbilityInfo {
    public ChampionSpellsResponse response;
    public string spellIconUrl;
    public string spellUsageGifUrl;
    public Emote spellEmoji;
}