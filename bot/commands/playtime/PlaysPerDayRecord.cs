using System;
using YordleYelper.database.attributes;

namespace YordleYelper.bot.commands.playtime; 

public class PlaysPerDayRecord {
    [RecordParameter("amount")] public int Amount { get; set; }
    [RecordParameter("champion_total_amount")] public int ChampionTotalAmount { get; set; }
    [RecordParameter("champion_rift_amount")] public int ChampionRiftAmount { get; set; }
    [RecordParameter("champion_aram_amount")] public int ChampionAramAmount { get; set; }
    [RecordParameter("date")] public DateTime Date { get; set; }
}