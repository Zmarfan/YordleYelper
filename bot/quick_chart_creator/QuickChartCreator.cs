using System.Collections.Generic;
using System.Linq;

namespace YordleYelper.bot.quick_chart_creator; 

public static class QuickChartCreator {
    private const string CONFIG_TEMPLATE = "{{type: 'outlabeledPie',data: {{ labels: [{1}],datasets: [{{ data: [{2}],borderWidth: 0, }}]}},options: {{ title: {{ display: true, text: '{0}'}},cutoutPercentage: 15, plugins: {{ legend: false, outlabels: {{text:'%l',color:'white',stretch:8,font:{{resizable:true,minSize: 2,maxSize: 5}} }}, datalabels: {{display: false}} }}}}}}";
 
    public static string CreatePieChart(
        string title,
        int width,
        int height,
        List<KeyValuePair<string, long>> dataList
    ) {

        string labels = string.Join(",", dataList.Select((entry, i) => $"\"{i + 1}. {entry.Key}\""));
        string data = string.Join(",", dataList.Select(entry => entry.Value));

        return new QuickChart(width, height, string.Format(CONFIG_TEMPLATE, title, labels, data)).GetShortUrl();
    }
}