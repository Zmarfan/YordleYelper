using System;
using System.Collections.Generic;

namespace YordleYelper.bot.data; 

public class Map {
    private static readonly Dictionary<int, Map> CODE_TO_MAP = new();
    
    public static readonly Map NONE = Add(0);
    public static readonly Map RIFT = Add(11);
    public static readonly Map ARAM = Add(12);
    public static readonly Map NEXUS_BLITZ = Add(21);
    public static readonly Map ARENA = Add(30);

    public readonly int code;

    private Map(int code) {
        this.code = code;
    }

    public static Map FromCode(int code) {
        return CODE_TO_MAP[code];
    }

    private static Map Add(int code) {
        Map gameMode = new(code);
        CODE_TO_MAP.Add(code, gameMode);
        return gameMode;
    }
}