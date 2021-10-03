using System;
using System.Text.Json.Serialization;
using Kustomaur.Models;

namespace Kustomaur.Dashboard.DashboardParts.Implementations.SubParts
{
    public class ChartInput : Input
    {
        public ChartInput(bool isOptional = true) : base("options", isOptional)
        {
            Value = new ChartInputValue();
        }
    }

    public class ChartInputValue
    {
        public ChartInputValueChart Chart { get; set; }

        public ChartInputValue()
        {
            Chart = new ChartInputValueChart();
        }
    }

    public class ChartInputValueChart
    {
        public ChartInputValueChart()
        {
            TitleKind = 1;
        }
        public int TitleKind { get; set; }
        
        public string Title { get; set; }

        public ChartInputValueChartTimespan Timespan { get; set; }
        
        public ChartInputValueChartGrouping Grouping { get; set; }
        
        public ChartInputValueChartVisualisation Visualization { get; set; }
    }

    public class ChartInputValueChartTimespan
    {
        public int Grain { get; set; }
        public bool ShowUTCTime { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ChartInputValueChartTimespanRelative Relative { get; set; }
        
        public ChartInputValueChartTimespan()
        {
            ShowUTCTime = true;
            Relative = new ChartInputValueChartTimespanRelative();
        }
    }

    public class ChartInputValueChartTimespanRelative
    {
        public int Duration { get; set; }

        public ChartInputValueChartTimespanRelative()
        {
            Duration = 86400000;
        }
    }

    public class ChartInputValueChartGrouping
    {
        public string Dimension { get; set; }
        public int Sort { get; set; }
        public int Top { get; set; }
        

        public ChartInputValueChartGrouping()
        {
            Dimension = "Instance";
            Sort = 2;
            Top = 10;
        }
    }

    public class ChartInputValueChartVisualisation
    {
        public int ChartType { get; set; }
        public LegendVisualisation LegendVisualization { get; set; }
        
        public AxisVisualisation AxisVisualization { get; set; }

        public ChartInputValueChartVisualisation()
        {
            ChartType = 2;
            LegendVisualization = new LegendVisualisation();
            AxisVisualization = new AxisVisualisation();
        }
    }

    public class LegendVisualisation
    {
        public bool IsVisible { get; set; }
        public int Position { get; set; }
        public bool HideSubtitle { get; set; }

        public LegendVisualisation()
        {
            IsVisible = true;
            Position = 2;
            HideSubtitle = false;
        }
    }

    public class AxisVisualisation
    {
        public AxisVisualisationAxis X { get; set; }
        public AxisVisualisationAxis Y { get; set; }

        public AxisVisualisation()
        {
            X = new AxisVisualisationAxis() { IsVisible = true, AxisType = 2 };
            Y = new AxisVisualisationAxis() { IsVisible = true, AxisType = 1 };
        }
    }

    public class AxisVisualisationAxis
    {
        public bool IsVisible { get; set; }
        public int AxisType { get; set; }
    }
}