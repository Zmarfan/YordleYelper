using System.Collections.Generic;
using System.Linq;
using QuickChart;

namespace YordleYelper.bot.quick_chart_creator; 

public static class QuickChartCreator {
    private const string CONFIG_TEMPLATE = "{{type: 'pie',data: {{labels: [{1}], datasets: [{{label: 'Foo',data: [{2}],borderWidth: 0,}}]}},options: {{ title: {{ display: true, text: '{0}' }}, cutoutPercentage: 15,plugins: {{datalabels: {{display: false}}}}}}}}";
    
    public static string CreatePieChart(
        string title,
        int width,
        int height,
        List<KeyValuePair<string, long>> dataList,
        int maxLabelsToShow
    ) {

        string labels = string.Join(",", dataList.Take(maxLabelsToShow).Select((entry, i) => $"\"{i + 1}. {entry.Key}\""));
        string data = string.Join(",", dataList.Select(entry => entry.Value));
        
        return new Chart {
            Width = width,
            Height = height,
            Config = string.Format(CONFIG_TEMPLATE, title, labels, data)
        }.GetUrl();
    }
}