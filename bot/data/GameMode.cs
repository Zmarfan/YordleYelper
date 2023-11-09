using System.Collections.Generic;

namespace YordleYelper.bot.data; 

public class GameMode {
    private static readonly Dictionary<string, GameMode> CODE_TO_MODE = new();
    
    public static readonly GameMode NONE = Add("");
    public static readonly GameMode CLASSIC = Add("CLASSIC");
    public static readonly GameMode ODIN = Add("ODIN");
    public static readonly GameMode ARAM = Add("ARAM");
    public static readonly GameMode TUTORIAL = Add("TUTORIAL");
    public static readonly GameMode ONE_FOR_ALL = Add("ONEFORALL");
    public static readonly GameMode FIRST_BLOOD = Add("FIRSTBLOOD");
    public static readonly GameMode URF = Add("URF");
    public static readonly GameMode CHERRY = Add("CHERRY");
    public static readonly GameMode NEXUSBLITZ = Add("NEXUSBLITZ");
    public static readonly GameMode ULTBOOK = Add("ULTBOOK");

    public readonly string code;

    private GameMode(string code) {
        this.code = code;
    }

    public static GameMode FromCode(string code) {
        return CODE_TO_MODE[code];
    }

    private static GameMode Add(string code) {
        GameMode gameMode = new(code);
        CODE_TO_MODE.Add(code, gameMode);
        return gameMode;
    }
}