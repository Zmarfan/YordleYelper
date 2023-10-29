using System;
using System.Text;

namespace YordleYelper.bot.extensions; 

public static class TimeSpanExtensions {
    public static string ToTimeSinceString(this TimeSpan timeSpan) {
        StringBuilder builder = new();
        if (timeSpan.Days != 0) {
            builder.Append($"**{timeSpan.Days}** day{(timeSpan.Days > 1 ? "s" : "")}, ");
        }
        if (timeSpan.Days != 0 || timeSpan.Hours != 0) {
            builder.Append($"**{timeSpan.Hours}** hour{(timeSpan.Hours > 1 ? "s" : "")} and ");
        }

        return builder.Append($"**{timeSpan.Minutes}** minute{(timeSpan.Minutes > 1 ? "s" : "")}").ToString();
    }
}