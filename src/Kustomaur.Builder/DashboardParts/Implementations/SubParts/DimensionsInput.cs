using System.Collections.Generic;
using System.Text.Json.Serialization;
using Kustomaur.Models;

namespace Kustomaur.Dashboard.DashboardParts.Implementations.SubParts
{
    public class DimensionsInput : Input
    {
        public DimensionsInput() : base("Dimensions")
        {
        }

        public DimensionsInput WithValue(DimensionsInputValue value)
        {
            Value = value;
            return this;
        }

        public DimensionsInput WithXAxis(DimensionsInputValueAxis xAxis)
        {
            ((DimensionsInputValue)Value).XAxis = xAxis;
            return this;
        }

        public DimensionsInput WithYAxis(List<DimensionsInputValueAxis> yAxis)
        {
            ((DimensionsInputValue)Value).YAxis = yAxis;
            return this;
        }
    }
    
    public class DimensionsInputValue
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("xAxis")]
        public DimensionsInputValueAxis XAxis { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("yAxis")]
        public List<DimensionsInputValueAxis> YAxis { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DimensionsInputValueAggregation? Aggregation { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<object> SplitBy { get; set; }

        public DimensionsInputValue()
        {
            XAxis = new DimensionsInputValueAxis();
            YAxis = new List<DimensionsInputValueAxis>();
            Aggregation = DimensionsInputValueAggregation.Sum;
            SplitBy = new List<object>();
        }
    }

    public class DimensionsInputValueAxis
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public enum DimensionsInputValueAggregation
    {
        Sum,
        Count
    }
    
}