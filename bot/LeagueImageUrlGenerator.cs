using YordleYelper.bot.data_fetcher.data_dragon.responses.champion_info;

namespace YordleYelper.bot; 

public static class LeagueImageUrlGenerator {
    private static readonly string CONTENT_BASE = $"https://ddragon.leagueoflegends.com/cdn/{VersionHolder.Version}/img/";
    
    public static string PortraitImageUrl(string championId) {
        return $"{CONTENT_BASE}champion/{championId}.png";
    }

    public static string ItemImageUrl(string itemImage) {
        return $"{CONTENT_BASE}item/{itemImage}";
    }
    
    public static string GetProfileIconUrlFromId(int id) {
        return $"{CONTENT_BASE}profileicon/{id}.png";
    }

    public static string PassiveImageUrl(string passiveImageName) {
        return $"{CONTENT_BASE}passive/{passiveImageName}";
    }

    public static string SpellImageUrl(string spellImageName) {
        return $"{CONTENT_BASE}spell/{spellImageName}";
    }
}