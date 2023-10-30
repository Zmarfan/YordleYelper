using DSharpPlus.Entities;

namespace YordleYelper.bot.extensions; 

public static class EmbedExtensions {
    private const string EMPTY = "\u200B";
    
    public static DiscordEmbedBuilder AddExtraLargeField(this DiscordEmbedBuilder embed, string name, string value, bool inline = false) {
        return embed.AddField(name, value + "\n" + EMPTY, inline);
    }
    
    public static DiscordEmbedBuilder AddEmptyField(this DiscordEmbedBuilder embed, bool inline = false) {
        return embed.AddField(EMPTY, EMPTY, inline);
    }
    
    public static DiscordEmbedBuilder AddEmptyNameField(this DiscordEmbedBuilder embed, string value, bool inline = false) {
        return embed.AddField(EMPTY, value, inline);
    }
}