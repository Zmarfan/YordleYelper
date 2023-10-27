using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace YordleYelper.bot.extensions; 

public static class StringExtensions {
    private static readonly Dictionary<string, string> FORMAT_TAG_EXCHANGES = new() {
        { "<li>", "\n- " },
        { "<br>", " " }, { "</br >", " " },
        { "<toggle>", "**" }, { "</toggle>", "**" },
        { "<tap>", "**" }, { "</tap>", "**" },
        { "<hold>", "**" }, { "</hold>", "**" },
        { "<charge>", "**" }, { "</charge>", "**" },
        { "<release>", "**" }, { "</release>", "**" },
        { "<evolve>", "**" }, { "</evolve>", "**" },
        { "<danger>", "**" }, { "</danger>", "**" },
        { "<specialRules>", "**" }, { "</specialRules>", "**" },
        { "<spellActive>", "**" }, { "</spellActive>", "**" },
        { "<active>", "**" }, { "</active>", "**" },
        { "<spellPassive>", "**" }, { "</spellPassive>", "**" },
        { "<recast>", "**" }, { "</recast>", "**" },
        { "<keywordName>", "*" }, { "</keywordName>", "*" },
        { "<keywordStealth>", "*" }, { "</keywordStealth>", "*" },
        { "<status>", "*" }, { "</status>", "*" },
        { "<rules>", "*" }, { "</rules>", "*" },
        { "<spellName>", "*" }, { "</spellName>", "*" },
        { "<keywordMajor>", "*" }, { "</keywordMajor>", "*" },
        { "<physicalDamage>", "**" }, { "</physicalDamage>", "**:small_orange_diamond:" },
        { "<magicDamage>", "**" }, { "</magicDamage>", "**:small_blue_diamond:" },
        { "<trueDamage>", "**" }, { "</trueDamage>", "**:white_small_square:" },
        { "<lifeSteal>", "**" }, { "</lifeSteal>", "**:small_red_triangle_down:" },
        { "<speed>", "**" }, { "</speed>", "**:man_running:" },
        { "<healing>", "**" }, { "</healing>", "**:heart_decoration:" },
        { "<shield>", "**" }, { "</shield>", "**:shield:" },
        { "<attackSpeed>", "**" }, { "</attackSpeed>", "**:wave:" },
    };
    
    public static string FirstCharToUpper(this string input) {
        return input switch {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1).ToString())
        };
    }
    
    public static string FormatLeagueTextForEmbed(this string text) {
        string formattedText = Regex.Replace(
            text,
            @"{{(?>[^{}]+|(?<Open>)\{{|(?<Close-Open>)\}})*(?(Open)(?!))}}",
            "?"
        );
        formattedText = FORMAT_TAG_EXCHANGES.Aggregate(formattedText, (current, pair) => current.Replace(pair.Key, pair.Value));
        return Regex.Replace(formattedText, @"<.*?>", string.Empty).TrimEnd('?');
    }
}