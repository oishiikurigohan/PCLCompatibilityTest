using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.ComponentModel;
using Xamarin.Forms;

using System.Collections.Generic;

namespace PCLCompatibilityTest.Services
{
    public class OxyPlotModel
    {
        public PlotModel Model { get; private set; }

        public OxyPlotModel()
        {
            this.Model = new PlotModel { Title = "おしごとメーター" };

            var categoryAxis = new CategoryAxis();
            var categoryAxisList = new List<string>();
            categoryAxisList.Add("精度");
            categoryAxisList.Add("速度");
            categoryAxis.ItemsSource = categoryAxisList;
            categoryAxis.Position = AxisPosition.Left;
            this.Model.Axes.Add(categoryAxis);

            var valueAxis = new LinearAxis();
            valueAxis.Position = AxisPosition.Bottom;
            valueAxis.Minimum = 0;
            valueAxis.Maximum = 100;
            this.Model.Axes.Add(valueAxis);

            Update(0);
        }

        public void Update(double sliderNewValue)
        {
            this.Model.Series.Clear();
            var data = new BarSeries();
            var barList = new List<BarItem>();
            barList.Add(new BarItem(100 - sliderNewValue)); // 精度
            barList.Add(new BarItem(sliderNewValue)); // 速度
            data.ItemsSource = barList;
            this.Model.Series.Add(data);
            Device.BeginInvokeOnMainThread(() => { this.Model.InvalidatePlot(true); });
        }
    }
}
