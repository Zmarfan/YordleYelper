using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace YordleYelper.bot.extensions; 

public static class StringExtensions {
    private const string BOLD = "**";
    private const string ITALICS = "*";
    private static readonly Dictionary<string, string> FORMAT_TAG_EXCHANGES = new() {
        { "<li>", $"\n{Emote.BULLET_WHITE}" },
        { "<br>", "\n" }, { "<br />", "\n" },
        { "</span>", BOLD },
        { "<toggle>", BOLD }, { "</toggle>", BOLD },
        { "<tap>", BOLD }, { "</tap>", BOLD },
        { "<hold>", BOLD }, { "</hold>", BOLD },
        { "<charge>", BOLD }, { "</charge>", BOLD },
        { "<release>", BOLD }, { "</release>", BOLD },
        { "<evolve>", BOLD }, { "</evolve>", BOLD },
        { "<danger>", BOLD }, { "</danger>", BOLD },
        { "<specialRules>", BOLD }, { "</specialRules>", BOLD },
        { "<spellActive>", BOLD }, { "</spellActive>", BOLD },
        { "<rarityMythic>", BOLD }, { "</rarityMythic>", BOLD },
        { "<rarityLegendary>", BOLD }, { "</rarityLegendary>", BOLD },
        { "<active>", BOLD }, { "</active>", BOLD },
        { "<spellPassive>", BOLD }, { "</spellPassive>", BOLD },
        { "<passive>", BOLD }, { "</passive>", BOLD },
        { "<recast>", BOLD }, { "</recast>", BOLD },
        { "<keywordName>",ITALICS }, { "</keywordName>",ITALICS },
        { "<keywordStealth>",ITALICS }, { "</keywordStealth>",ITALICS },
        { "<status>",ITALICS }, { "</status>",ITALICS },
        { "<rules>",ITALICS }, { "</rules>",ITALICS },
        { "<spellName>",ITALICS }, { "</spellName>",ITALICS },
        { "<keywordMajor>",ITALICS }, { "</keywordMajor>",ITALICS },
        { "<physicalDamage>", BOLD }, { "</physicalDamage>", $"{BOLD}{Emote.PHYSICAL_DAMAGE}" },
        { "<magicDamage>", BOLD }, { "</magicDamage>", $"{BOLD}{Emote.MAGIC_DAMAGE}" },
        { "<trueDamage>", BOLD }, { "</trueDamage>", $"{BOLD}{Emote.TRUE_DAMAGE}" },
        { "<lifeSteal>", BOLD }, { "</lifeSteal>", $"{BOLD}{Emote.LIFE_STEAL}" },
        { "<speed>", BOLD }, { "</speed>", $"{BOLD}{Emote.MOVE_SPEED}" },
        { "<healing>", BOLD }, { "</healing>", $"{BOLD}{Emote.HEALING}" },
        { "<shield>", BOLD }, { "</shield>", $"{BOLD}{Emote.SHIELD}" },
        { "<attackSpeed>", BOLD }, { "</attackSpeed>", $"{BOLD}{Emote.ATTACK_SPEED}" },
    };

    private static List<(string, Emote)> PHRASE_TO_STAT_EMOTE = new() {
        ( "Base Health Regen", Emote.HEALTH_REGEN ),
        ( "Health", Emote.HEALTH ),
        ( "Base Mana Regen", Emote.MANA_REGEN ),
        ( "Mana", Emote.MANA ),
        ( "Ability Haste", Emote.ABILITY_HASTE ),
        ( "Attack Damage", Emote.ATTACK_DAMAGE ),
        ( "Ability Power", Emote.ABILITY_POWER ),
        ( "Life Steal", Emote.LIFE_STEAL ),
        ( "Omnivamp", Emote.OMNIVAMP ),
        ( "Critical Strike Chance", Emote.CRITICAL_STRIKE_CHANCE ),
        ( "Attack Speed", Emote.ATTACK_SPEED ),
        ( "Armor Penetration", Emote.ARMOR_PENETRATION ),
        ( "Armor", Emote.ARMOR ),
        ( "Magic Resist", Emote.MAGIC_RESIST ),
        ( "Magic Penetration", Emote.MAGIC_PENETRATION ),
        ( "Gold Per", Emote.GOLD ),
        ( "Heal and Shield", Emote.HEAL_AND_SHIELD ),
        ( "Move Speed", Emote.MOVE_SPEED ),
        ( "Tenacity", Emote.TENACITY ),
        ( "Lethality", Emote.LETHALITY ),
    };

    public static string ToString<T>(this IEnumerable<T> iEnumerable, Func<StringBuilder, T, StringBuilder> fromEntry) {
        return iEnumerable.Aggregate(new StringBuilder(), fromEntry.Invoke).ToString();
    }

    public static StringBuilder AppendListEntry(this StringBuilder builder, Emote emote, string line) {
        return builder.AppendLine($"{emote} {line}");
    }

    public static string FirstCharToUpper(this string input) {
        return input switch {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1).ToString())
        };
    }
    
    public static string FormatLeagueTextForEmbed(this string text) {
        // TODO: try to grab some stats
        string formattedText = Regex.Replace(
            text,
            @"{{(?>[^{}]+|(?<Open>)\{{|(?<Close-Open>)\}})*(?(Open)(?!))}}",
            "?"
        );
        formattedText = FORMAT_TAG_EXCHANGES.Aggregate(formattedText, (current, pair) => current.Replace(pair.Key, pair.Value));
        formattedText = Regex.Replace(formattedText, @"<span.*?>", BOLD);
        return Regex.Replace(formattedText, @"<.*?>", string.Empty).TrimEnd('?');
    }

    public static string FormatLeagueStat(this string text) {
        string formatted = text.FormatLeagueTextForEmbed();
        Emote statEmote = PHRASE_TO_STAT_EMOTE.First(stat => formatted.Contains(stat.Item1)).Item2;
        return $"{statEmote} {formatted}";
    }
}