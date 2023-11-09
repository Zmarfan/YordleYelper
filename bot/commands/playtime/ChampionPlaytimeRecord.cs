using System;
using YordleYelper.database.attributes;

namespace YordleYelper.bot.commands.playtime; 

public class ChampionPlaytimeRecord {
    [RecordParameter("total_play_time_in_seconds")] public int TotalPlayTimeInSeconds { get; set; }
    [RecordParameter("rift_playtime_in_seconds")] public int RiftPlaytimeInSeconds { get; set; }
    [RecordParameter("aram_playtime_in_seconds")] public int AramPlaytimeInSeconds { get; set; }
    [RecordParameter("total_amount")] public int TotalAmount { get; set; }
    [RecordParameter("rift_amount")] public int RiftAmount { get; set; }
    [RecordParameter("aram_amount")] public int AramAmount { get; set; }
    [RecordParameter("first_played")] public DateTime FirstPlayed { get; set; }
    [RecordParameter("last_played")] public DateTime LastPlayed { get; set; }
}