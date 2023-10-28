using System;
using YordleYelper.bot.commands.choices;

namespace YordleYelper.bot.extensions; 

public class Emote {
    public static readonly Emote BULLET_BLUE = new(":small_blue_diamond:");
    public static readonly Emote BULLET_ORANGE = new(":small_orange_diamond:");
    public static readonly Emote BULLET_WHITE = new(":white_small_square:");

    public static readonly Emote OK = new(":sparkles:");
    public static readonly Emote ERROR = new(":question:");
    public static readonly Emote INTERNAL_ERROR = new(":scream:");
    
    public static readonly Emote PHYSICAL_DAMAGE = new(":small_orange_diamond:");
    public static readonly Emote MAGIC_DAMAGE = new(":small_blue_diamond:");
    public static readonly Emote TRUE_DAMAGE = new(":white_small_square:");
    public static readonly Emote LIFE_STEAL = new(":small_red_triangle_down:");
    public static readonly Emote SPEED = new(":man_running:");
    public static readonly Emote HEALING = new(":heart_decoration:");
    public static readonly Emote SHIELD = new(":shield:");
    public static readonly Emote ATTACK_SPEED = new(":wave:");

    private static readonly Emote ABILITY_Q = new(":regional_indicator_q:");
    private static readonly Emote ABILITY_W = new(":regional_indicator_w:");
    private static readonly Emote ABILITY_E = new(":regional_indicator_e:");
    private static readonly Emote ABILITY_R = new(":regional_indicator_r:");
    
    private readonly string _emoteText;

    private Emote(string emoteText) {
        _emoteText = emoteText;
    }

    public override string ToString() {
        return _emoteText;
    }

    public static Emote FromAbility(ChampionAbility ability) {
        return ability switch {
            ChampionAbility.Q => ABILITY_Q,
            ChampionAbility.W => ABILITY_W,
            ChampionAbility.E => ABILITY_E,
            ChampionAbility.R => ABILITY_R,
            _ => throw new ArgumentOutOfRangeException(nameof(ability), ability, null)
        };
    }
}