using System.Collections.Generic;
using System.Linq;

namespace YordleYelper.bot.quick_chart_creator; 

public static class QuickChartCreator {
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
    
    public static string CreatePieChart(
        string title,
        int width,
        int height,
        List<KeyValuePair<string, long>> dataList
    ) {
        long total = dataList.Sum(entry => entry.Value);

        string labels = string.Join(",", dataList.Select((entry, i) => FormatLabel(entry, i, total)));
        string data = string.Join(",", dataList.Select(entry => entry.Value));
        string colors = string.Join(",", dataList.Select((_, index) => COLORS[index % COLORS.Count]));

        return new QuickChart(width, height, string.Format(CONFIG_TEMPLATE, title, labels, colors, data)).GetShortUrl();
    }

    private static string FormatLabel(KeyValuePair<string, long> entry, int i, long total) {
        return entry.Value / (float)total < 0.0075f ? "''" : $"\"{i + 1}. {entry.Key}\"";
    }
}
