using System;
using System.Collections.Generic;

namespace YordleYelper.bot.data; 

public class GameType {
    private static readonly Dictionary<string, GameType> CODE_TO_TYPE = new();
    
    public static readonly GameType CUSTOM_GAME = Add("CUSTOM_GAME");
    public static readonly GameType TUTORIAL_GAME = Add("TUTORIAL_GAME");
    public static readonly GameType MATCHED_GAME = Add("MATCHED_GAME");

    public readonly string code;

    private GameType(string code) {
        this.code = code;
    }

    public static GameType FromCode(string code) {
        return CODE_TO_TYPE[code];
    }

    private static GameType Add(string code) {
        GameType gameMode = new(code);
        CODE_TO_TYPE.Add(code, gameMode);
        return gameMode;
    }
}