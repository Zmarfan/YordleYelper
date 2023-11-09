using System.Collections.Generic;
using System.Linq;
using YordleYelper.bot.data_fetcher.data_dragon.responses;
using YordleYelper.bot.data_fetcher.league_api.responses;
using YordleYelper.bot.quick_chart_creator;

namespace YordleYelper.bot.commands.masteries; 

public static class MasteryChartCreator {
    private const string CONFIG_TEMPLATE = "{{type: 'outlabeledPie',data: {{ labels: [{1}],datasets: [{{ backgroundColor: [{2}], data: [{3}],borderWidth: 0, }}]}},options: {{ title: {{ display: true, text: '{0}'}},cutoutPercentage: 15, plugins: {{ legend: false, outlabels: {{text:'%l',color:'white',stretch:8,font:{{resizable:true,minSize: 5,maxSize: 10}} }}, datalabels: {{display: false}} }}}}}}";

    private static readonly List<string> COLORS = new() {
        "'#B0462E'", 
        "'#B08F2E'", 
        "'#2EB04D'", 
        "'#2EADAF'", 
        "'#2E51B0'", 
        "'#4E2EB0'", 
        "'#8F2EB0'", 
        "'#B02E8F'", 
        "'#2E70B0'", 
        "'#2EB08C'"
    };
    
    private static readonly List<string> GRAY_SCALE_COLORS = new() {
        "'#6F6F6F'", 
        "'#878787'", 
        "'#5D5D5D'", 
        "'#585858'", 
        "'#4D4D4D'", 
        "'#525252'", 
        "'#6C6C6C'", 
        "'#7A7A7A'", 
        "'#585858'", 
        "'#5D5D5D'"
    };
    
    public static string CreateChart(
        string title,
        int width,
        int height,
        bool showAvailableChests,
        List<(ChampionMasteryResponse, BasicChampionInfo)> dataList
    ) {
        long total = dataList.Sum(entry => entry.Item1.championPoints);

        string labels = string.Join(",", dataList.Select((entry, i) => FormatLabel(entry, i, total)));
        string data = string.Join(",", dataList.Select(entry => entry.Item1.championPoints));
        string colors = string.Join(",", dataList.Select((entry, index) => ColorToShow(entry.Item1, index, showAvailableChests)));

        return new QuickChart(width, height, string.Format(CONFIG_TEMPLATE, title, labels, colors, data)).GetShortUrl();
    }

    private static string FormatLabel((ChampionMasteryResponse, BasicChampionInfo) entry, int i, long total) {
        return entry.Item1.championPoints / (float)total < 0.0075f ? "''" : $"\"{i + 1}. {entry.Item2.Name}\"";
    }
    
    private static string ColorToShow(ChampionMasteryResponse mastery, int index, bool showAvailableChests) {
        List<string> palette = mastery.chestGranted && showAvailableChests ? GRAY_SCALE_COLORS : COLORS;
        return palette[index % palette.Count];
    }
}
