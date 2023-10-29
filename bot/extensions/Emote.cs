﻿using System;
using YordleYelper.bot.commands.choices;

namespace YordleYelper.bot.extensions; 

public class Emote {
    public static readonly Emote BULLET_BLUE = new(":small_blue_diamond:");
    public static readonly Emote BULLET_ORANGE = new(":small_orange_diamond:");
    public static readonly Emote BULLET_WHITE = new(":white_small_square:");

    public static readonly Emote OK = new(":sparkles:");
    public static readonly Emote ERROR = new(":question:");
    public static readonly Emote INTERNAL_ERROR = new(":scream:");
    
    public static readonly Emote ATTACK_DAMAGE = new(":crossed_swords:");
    public static readonly Emote ABILITY_POWER = new(":magic_wand: ");
    public static readonly Emote PHYSICAL_DAMAGE = new(":small_orange_diamond:");
    public static readonly Emote MAGIC_DAMAGE = new(":small_blue_diamond:");
    public static readonly Emote TRUE_DAMAGE = new(":white_small_square:");
    public static readonly Emote LIFE_STEAL = new(":drop_of_blood:");
    public static readonly Emote MOVE_SPEED = new(":athletic_shoe:");
    public static readonly Emote HEALING = new(":heart_decoration:");
    public static readonly Emote SHIELD = new(":shield:");
    public static readonly Emote ATTACK_SPEED = new(":wave:");
    public static readonly Emote HEALTH = new(":green_heart:");
    public static readonly Emote HEALTH_REGEN = new(":two_hearts:");
    public static readonly Emote MANA = new(":droplet:");
    public static readonly Emote MANA_REGEN = new(":sweat_drops:");
    public static readonly Emote ABILITY_HASTE = new(":hourglass_flowing_sand:");
    public static readonly Emote OMNIVAMP = new(":drop_of_blood:");
    public static readonly Emote CRITICAL_STRIKE_CHANCE = new(":boom:");
    public static readonly Emote ARMOR_PENETRATION = new(":cupid:");
    public static readonly Emote ARMOR = new(":shield:");
    public static readonly Emote MAGIC_RESIST = new(":running_shirt_with_sash:");
    public static readonly Emote MAGIC_PENETRATION = new(":atom:");
    public static readonly Emote HEAL_AND_SHIELD = new(":mending_heart:");
    public static readonly Emote TENACITY = new(":hiking_boot:");
    public static readonly Emote LETHALITY = new(":anger:");
    
    public static readonly Emote GOLD = new(":coin:");

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