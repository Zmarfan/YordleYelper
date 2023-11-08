using System;
using System.Collections.Generic;

namespace YordleYelper.bot.data; 

public class StatPerk {
    private static readonly Dictionary<int, StatPerk> CODE_TO_STAT_PERK = new();
    
    public static readonly StatPerk ADAPTIVE_FORCE = Add(5008);
    public static readonly StatPerk ARMOR = Add(5002);
    public static readonly StatPerk ATTACK_SPEED = Add(5005);
    public static readonly StatPerk ABILITY_HASTE = Add(5007);
    public static readonly StatPerk HEALTH_SCALING = Add(5001);
    public static readonly StatPerk MAGIC_RESIST = Add(5003);

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