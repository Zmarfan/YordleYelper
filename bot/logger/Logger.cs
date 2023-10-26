using System;

namespace YordleYelper.bot.logger; 

internal static class Logger {
    public static void Info(string text) {
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.ForegroundColor = ConsoleColor.White;
        Log($"[Info] {text}");
        ResetColor();
    }
    
    public static void Error(Exception e, string text = "") {
        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.White;
        Log($"[Error] {text}; exception: {e}");
        ResetColor();
    }
    
    public static void Warning(string text, Exception e) {
        Console.BackgroundColor = ConsoleColor.DarkYellow;
        Console.ForegroundColor = ConsoleColor.White;
        Log($"[Warning] {text}; exception: {e}");
        ResetColor();
    }

    private static void Log(string text) {
        Console.WriteLine($"[{DateTime.Now}] [000 /Application] {text}");
    }
    
    private static void ResetColor() {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Gray;
    }
}