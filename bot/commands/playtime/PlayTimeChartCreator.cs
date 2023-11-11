using System;
using System.Collections.Generic;
using System.Linq;
using YordleYelper.bot.quick_chart_creator;

namespace YordleYelper.bot.commands.playtime; 

public static class PlayTimeChartCreator {
    private const string CONFIG_TEMPLATE = "{{type: 'bar',data: {{ datasets: [ {0} ]}},options: {{ title: {{ display: true, text: \"{1}\" }}, scales: {{ xAxes: [{{ stacked: true, barPercentage: 1.25, type: 'time', time: {{ parser: 'YYYY-MM-DD', minUnit: 'day' }}, scaleLabel: {{ display: true, labelString: 'Date' }} }}], yAxes: [{{ stacked: true, scaleLabel: {{ display: true, labelString: 'Games' }}, ticks: {{ precision: 0, beginAtZero: true }} }}] }} }}}}";
    private const string DATA_SET_TEMPLATE = "{{label: '{0}', backgroundColor: '{1}', data: [{2}] }}";
    
    private const string ALL_COLOR = "#666699";
    private const string CHAMPION_ELSE_COLOR = "#4bc0c0";
    private const string CHAMPION_RIFT_COLOR = "#669900";
    private const string CHAMPION_ARAM_COLOR = "#ff5050";
    
    public static string CreateChart(
        string title,
        int width,
        int height,
        List<PlaysPerDayRecord> records,
        bool compareAgainstAll, 
        bool separateGameMode
    ) {
        List<string> dataSets = new();
        if (separateGameMode) {
            dataSets.Add(FormatData(records, record => record.ChampionRiftAmount, CHAMPION_RIFT_COLOR, "Champion Rift games"));
            dataSets.Add(FormatData(records, record => record.ChampionAramAmount, CHAMPION_ARAM_COLOR, "Champion Aram games"));
            dataSets.Add(FormatData(records, record => record.ChampionTotalAmount - record.ChampionRiftAmount - record.ChampionAramAmount, CHAMPION_ELSE_COLOR, "Champion Other games"));
        }
        else {
            dataSets.Add(FormatData(records, record => record.ChampionTotalAmount, CHAMPION_ELSE_COLOR, "Champion games"));
        }
        if (compareAgainstAll) {
            dataSets.Add(FormatData(records, record => record.Amount - record.ChampionTotalAmount, ALL_COLOR, "Other Champion games"));
        }
        
        string data = string.Join(",", dataSets);
        
        return new QuickChart(width, height, string.Format(CONFIG_TEMPLATE, data, title), backgroundColor: "#2B2D31").GetShortUrl();
    }

    private static string FormatData(List<PlaysPerDayRecord> records, Func<PlaysPerDayRecord, int> dataFetcher, string color, string label) {
        string data = string.Join(",", records.Select(record => $"{{x: '{record.Date.ToShortDateString()}', y: {dataFetcher.Invoke(record)}}}"));
        return string.Format(DATA_SET_TEMPLATE, label, color, data);
    }
}
