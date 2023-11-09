using System;
using System.Collections.Generic;
using System.Linq;
using YordleYelper.bot.quick_chart_creator;

namespace YordleYelper.bot.commands.playtime; 

public static class PlayTimeChartCreator {
    private const string CONFIG_TEMPLATE = "{{type: 'bar',data: {{ datasets: [ {{label: '{0}', backgroundColor: 'rgb(75, 192, 192)', data: [{1}] }}]}},options: {{ scales: {{ xAxes: [{{ barPercentage: 1.25, type: 'time', time: {{ parser: 'YYYY-MM-DD', minUnit: 'day' }}, scaleLabel: {{ display: true, labelString: 'Date' }} }}], yAxes: [{{ scaleLabel: {{ display: true, labelString: 'Games' }}, ticks: {{ precision: 0, beginAtZero: true }} }}] }} }}}}";

    public static string CreateChart(
        string title,
        int width,
        int height,
        List<PlaysPerDayRecord> records
    ) {
        return new QuickChart(width, height, string.Format(CONFIG_TEMPLATE, title, FormatData(records)), backgroundColor: "#2B2D31").GetShortUrl();
    }

    private static string FormatData(List<PlaysPerDayRecord> records) {
        return string.Join(",", records.Select(record => $"{{x: '{record.Date.ToShortDateString()}', y: {record.Amount}}}"));
    }
}
