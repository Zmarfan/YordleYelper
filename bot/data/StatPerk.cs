using System;
using System.Collections.Generic;

namespace YordleYelper.bot.data; 

public class StatPerk {
    private static readonly Dictionary<int, StatPerk> CODE_TO_STAT_PERK = new();
    
    // TODO: change these ids to real ones
    public static readonly StatPerk ADAPTIVE_FORCE = Add(1);
    public static readonly StatPerk ARMOR = Add(2);
    public static readonly StatPerk ATTACK_SPEED = Add(3);
    public static readonly StatPerk ABILITY_HASTE = Add(4);
    public static readonly StatPerk HEALTH_SCALING = Add(5);
    public static readonly StatPerk MAGIC_RESIST = Add(6);

    public readonly int code;

    private StatPerk(int code) {
        this.code = code;
    }

    public static StatPerk FromCode(int code) {
        return CODE_TO_STAT_PERK[code];
    }

    private static StatPerk Add(int code) {
        StatPerk gameMode = new(code);
        CODE_TO_STAT_PERK.Add(code, gameMode);
        return gameMode;
    }
}