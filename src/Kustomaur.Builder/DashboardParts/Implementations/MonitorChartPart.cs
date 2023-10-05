using System.Collections.Generic;
using System.Linq;
using Kustomaur.Dashboard.DashboardParts.Implementations.SubParts;
using Kustomaur.Models;
using Kustomaur.Models.Filters;

namespace Kustomaur.Dashboard.DashboardParts.Implementations
{
    public class MonitorChartPart : DashboardPart
    {
        public string _title;
        public MonitorChartPart WithTitle(string title)
        {
            _title = title;
            return this;
        }

        private int _titleKind;
        public MonitorChartPart WithTitleKind(int titleKind)
        {
            _titleKind = titleKind;
            return this;
        }

        // with timespan
        private ChartInputValueChartTimespan _timespan;
        public MonitorChartPart WithTimespan(ChartInputValueChartTimespan timespan)
        {
            _timespan = timespan;
            return this;
        }
        
        // with grouping
        private ChartInputValueChartGrouping _grouping;
        public MonitorChartPart WithGrouping(ChartInputValueChartGrouping grouping)
        {
            _grouping = grouping;
            return this;
        }

        // with Visualsation
        private ChartInputValueChartVisualisation _virtualisation;
        public MonitorChartPart WithVisualisation(ChartInputValueChartVisualisation visualisation)
        {
            _virtualisation = visualisation;
            return this;
        }
        
        //add
        private List<ChartInputValueChartMetric> _metrics;
        public MonitorChartPart WithMetric(ChartInputValueChartMetric metric)
        {
            _metrics.Add(metric);
            return this;
        }

        //with Filters
        private Dictionary<string, List<string>> _filters;
        public MonitorChartPart WithFilters(ChartInputValueChartFilter filter)
        {
            if (_filters == null)
                _filters = new Dictionary<string, List<string>>();

            if (filter != null && !string.IsNullOrEmpty(filter.Name) && filter.Values != null)
            {
                if (_filters.ContainsKey(filter.Name))
                {
                    _filters[filter.Name].AddRange(filter.Values);
                }
                else
                {
                    _filters[filter.Name] = new List<string>(filter.Values);
                }
            }

            return this;
        }
        
        // resource id
        // resource name
        // resource aggregation type
        // resource namespace
        // resource DisplayName

        public MonitorChartPart()
        {
            WithRowSpan(3);
            WithColSpan(3);
            _metrics = new List<ChartInputValueChartMetric>();
        }

        public override Part GeneratePart()
        {
            _part.WithPosition(_x, _y, _rowSpan, _colSpan);
            if (_part.Metadata == null)
            {
                _part.Metadata = new PartMetadata();
            }
            _part.Metadata.WithType("Extension/HubsExtension/PartType/MonitorChartPart");
            _part.Metadata.AddInput(new Input("sharedTimeRange", isOptional: true));
            _part.Metadata.AddInput(BuildChartInput());
            _part.Metadata.Settings = null;
            _part.Metadata.Asset = new Asset()
            {
                IdInputName = "ComponentId",
                Type = "ApplicationInsights"
            };
            return _part;
        }

        private Input BuildChartInput()
        {
            var chartInput = new ChartInput();

            var value = chartInput.ValueAs<ChartInputValue>();

            if (_grouping != null)
                value.Chart.Grouping = _grouping;

            if (_timespan != null)
                value.Chart.Timespan = _timespan;

            if (_metrics != null && _metrics.Any())
                value.Chart.Metrics = _metrics;

            if (!string.IsNullOrEmpty(_title))
                value.Chart.Title = _title;

            if (_filters != null && _filters.Count > 0)
            {
                value.Chart.Filters = new ChartInputValueChartFilter
                {
                    Filters = _filters.Select(filter =>
                        new FilterModel
                        {
                            Name = filter.Key,
                            Operator = FilterOperator.Equals,
                            Values = filter.Value
                        }).ToList()
                };
            }
            else
            {
                // If no filters specified, set Filters to null or an empty list
                value.Chart.Filters = null; // or value.Chart.Filters = new ChartInputValueChartFilter { Filters = new List<FilterModel>() };
            }

            return chartInput;
        }
    }
}