using System;
using YordleYelper.database.attributes;

namespace YordleYelper.bot.commands.playtime; 

public class PlaysPerDayRecord {
    [RecordParameter("date")] public DateTime Date { get; set; }
    [RecordParameter("amount")] public int Amount { get; set; }
}