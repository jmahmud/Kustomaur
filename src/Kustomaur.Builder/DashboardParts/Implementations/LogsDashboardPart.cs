using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Kustomaur.Dashboard.DashboardParts.Implementations.SubParts;
using Kustomaur.Models;

namespace Kustomaur.Dashboard.DashboardParts.Implementations
{
    public class LogsDashboardPart : DashboardPart
    {
        public string KustoQuery { get; set; }
       
        public string ComponentName { get; set; }
        public string ComponentId { get; set; }

        public string PartTitle { get; set; }
        public string PartSubTitle { get; set; }
        public bool IsQueryContainTimeRange { get; set; }
        
        public InputControlType ControlType { get; set; }
        
        public DimensionsInput DimensionsInput { get; set; }
        
        public InputSpecificChart? SpecificChart { get; set; }

        public InputLegendOptions LegendOptions { get; set; }
        
        public Guid PartId { get; set; }
        public List<Input> Inputs { get; set; }
        
        public LogsDashboardPart()
        {
            // Default Row and Column
            WithRowSpan(3);
            WithColSpan(3);
            
            //default dimensioninput
            DimensionsInput = new DimensionsInput();
            Inputs = new List<Input>();
        }
        
        public override Part GeneratePart()
        {
            _part.WithPosition(_x, _y, _rowSpan, _colSpan);
            _part.Metadata = new PartMetadata();
            _part.Metadata.WithType("Extension/Microsoft_OperationsManagementSuite_Workspace/PartType/LogsDashboardPart");
            SetInputs();
            _part.Metadata.Inputs = Inputs;
            _part.Metadata.Settings = new {};
            // _part.Metadata.Settings.Content = new LogsDashboardPartSettingsContent()
            // {
            //     Query = KustoQuery
            // };
            return _part;
        }

        private void SetInputs()
        {
            Inputs.Add(new Input(name: "resourceTypeMode"));
            Inputs.Add(new Input(name: "ComponentId"));
            Inputs.Add(new Input(name: "Scope", value: new { resourceIds = new List<string>() { GenerateResourceId()}}));
            Inputs.Add(new Input(name: "PartId", value: Guid.NewGuid()));
            Inputs.Add(new Input(name: "Version", value: "2.0"));
            Inputs.Add(new Input(name: "TimeRange", value: "P1D"));
            Inputs.Add(new Input(name: "DashboardId"));
            Inputs.Add(new Input(name: "DraftRequestParameters"));
            Inputs.Add(new Input(name: "Query", value: KustoQuery));
            Inputs.Add(new Input(name: "SpecificChart", value: SpecificChart?.ToString()));
            
            SetControlType();
            SetLegendOptions();
            SetDimensions();
            
            Inputs.Add(new Input(name: "PartTitle", value: PartTitle));
            Inputs.Add(new Input(name: "PartSubTitle", value: PartSubTitle));
            Inputs.Add(new Input(name: "IsQueryContainTimeRange", value: IsQueryContainTimeRange));
            
            
        }

        private void SetDimensions()
        {
            if (SpecificChart != null)
            {
                if (DimensionsInput.Value == null)
                {
                    // default the dimensions if SpecificChart is set and there are no dimensions
                    var dimensionsInputValue = new DimensionsInputValue();
                    dimensionsInputValue.XAxis.Name = "timestamp";
                    dimensionsInputValue.XAxis.Type = "datetime";
                    
                    dimensionsInputValue.YAxis.Add(new DimensionsInputValueAxis()
                    {
                        Name = "duration",
                        Type = "real"
                    });
                    
                    dimensionsInputValue.SplitBy.Add(new DimensionsInputValueAxis()
                    {
                        Name = "id",
                        Type = "string"
                    });
                    
                    dimensionsInputValue.Aggregation = DimensionsInputValueAggregation.Sum;

                    DimensionsInput.Value = dimensionsInputValue;
                }    
            }            
            Inputs.Add(DimensionsInput);

        }

        private void SetLegendOptions()
        {
            if (SpecificChart == null)
            {
                Inputs.Add(new Input(name: "LegendOptions"));
            }
            else
            {
                if (LegendOptions == null)
                {
                    LegendOptions = new InputLegendOptions() { position = "Bottom", isEnabled = true};
                }
                Inputs.Add(new Input(name: "LegendOptions", value: LegendOptions));
            }
        }

        private void SetControlType()
        {
            if (SpecificChart != null && ControlType != InputControlType.FrameControlChart)
            {
                throw new Exception("ControlType must be FrameControlChart if SpecificChart is set");
            }

            Inputs.Add(new Input(name: "ControlType", value: ControlType.ToString()));
        }

        private string GenerateResourceId()
        {
            return
                $"/subscriptions/{_subscriptionId}/resourceGroups/{_resourceGroup}/providers/microsoft.insights/components/{ComponentName}";
        }

        public LogsDashboardPart WithQuery(string kustoQuery)
        {
            KustoQuery = kustoQuery;
            return this;
        }
        
        public LogsDashboardPart WithInsightsComponentName(string componentName)
        {
            ComponentName = componentName;
            return this;
        }

        public LogsDashboardPart WithComponentId(string componentId)
        {
            ComponentId = componentId;
            return this;
        }
        
        public LogsDashboardPart WithDimension(DimensionsInput dimensionsInput)
        {
            DimensionsInput = dimensionsInput;
            return this;
        }

        public LogsDashboardPart WithTitle(string partTitle)
        {
            PartTitle = partTitle;
            return this;
        }
        
        public LogsDashboardPart WithSubTitle(string subTitle)
        {
            PartSubTitle = subTitle;
            return this;
        }

        public LogsDashboardPart WithIsQueryContainTimeRange(bool isQueryContainTimeRange)
        {
            IsQueryContainTimeRange = isQueryContainTimeRange;
            return this;
        }

        public LogsDashboardPart WithControlType(InputControlType controlType)
        {
            ControlType = controlType;
            return this;
        }
        
        public LogsDashboardPart WithLegendOptions(InputLegendOptions legendOptions)
        {
            LegendOptions = legendOptions;
            return this;
        }
        
    }

    public class LogsDashboardPartSettingsContent
    {
        [JsonPropertyName("Query")]
        public string Query { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DimensionsInput Dimensions { get; set; }
        
    }
}