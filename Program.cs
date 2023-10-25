using YordleYelper.bot;

namespace YordleYelper; 

internal static class Program {
    public static void Main() {
        DiscordBot bot = new();
        bot.Start().GetAwaiter().GetResult();
    }
}