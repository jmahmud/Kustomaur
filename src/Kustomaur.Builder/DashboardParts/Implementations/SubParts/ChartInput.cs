using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Kustomaur.Models;
using Kustomaur.Models.Filters;

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
            Timespan = new ChartInputValueChartTimespan();
            Grouping = new ChartInputValueChartGrouping();
            Visualization = new ChartInputValueChartVisualisation();
            Metrics = new List<ChartInputValueChartMetric> { new ChartInputValueChartMetric() };
            Filters = new ChartInputValueChartFilter(new List<String>());
        }

        public int TitleKind { get; set; }
    
        public string Title { get; set; }

        public ChartInputValueChartTimespan Timespan { get; set; }
    
        public ChartInputValueChartGrouping Grouping { get; set; }
    
        public ChartInputValueChartVisualisation Visualization { get; set; }

        public List<ChartInputValueChartMetric> Metrics { get; set; }
    
        public ChartInputValueChartFilter Filters { get; set; }
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
            Grain = 1;
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
        
        public bool DisablePinning { get; set; }

        public ChartInputValueChartVisualisation()
        {
            ChartType = 2;
            LegendVisualization = new LegendVisualisation();
            AxisVisualization = new AxisVisualisation();
            DisablePinning = true;
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


    public class Filters
    {
        public Dictionary<string, FilterModel> EntityName { get; set; }
    }

    public class ChartInputValueChartFilter
    {
        public Dictionary<string, FilterModel> Filters { get; set; }

        public ChartInputValueChartFilter(List<string> userValues)
        {
            if (userValues != null && userValues.Any())
            {
                Filters = new Dictionary<string, FilterModel>
                {
                    {
                        "model", new FilterModel
                        {
                            Operator = FilterOperator.equals.ToString(),
                            Values = userValues
                        }
                    }
                };
            }
            else
            {
                Filters = null;
            }
        }
    }





    
    public class ChartInputValueChartMetric
    {
        public ChartInputValueChartMetricResourceMetadata ResourceMetadata { get; set; }
        public string Name { get; set; }
        public int AggregationType { get; set; }
        public string Namespace { get; set; }
        public MetricVisualisation MetricVisualization { get; set; }

        public ChartInputValueChartMetric()
        {
            ResourceMetadata = new ChartInputValueChartMetricResourceMetadata();
            MetricVisualization = new MetricVisualisation();
            Namespace = "microsoft.web/serverfarms";
            AggregationType = 4;
        }
    }

    public class ChartInputValueChartMetricResourceMetadata
    {
        public string Id { get; set; }
    }

    public class MetricVisualisation
    {
        public string DisplayName { get; set; }
    }
    
}