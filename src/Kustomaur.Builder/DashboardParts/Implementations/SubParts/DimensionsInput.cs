using System.Collections.Generic;
using System.Text.Json.Serialization;
using Kustomaur.Models;

namespace Kustomaur.Dashboard.DashboardParts.Implementations.SubParts
{
    public class DimensionsInput : Input
    {
        public DimensionsInputValue Value { get; set; }
        public DimensionsInput() : base("Dimensions")
        {
            Value = new DimensionsInputValue();
        }

        public DimensionsInput WithValue(DimensionsInputValue value)
        {
            Value = value;
            return this;
        }

        public DimensionsInput WithXAxis(DimensionsInputValueAxis xAxis)
        {
            Value.XAxis = xAxis;
            return this;
        }

        public DimensionsInput WithYAxis(List<DimensionsInputValueAxis> yAxis)
        {
            Value.YAxis = yAxis;
            return this;
        }
    }

    public class DimensionsInputValue
    {
        [JsonPropertyName("xAxis")]
        public DimensionsInputValueAxis XAxis { get; set; }
        
        [JsonPropertyName("yAxis")]
        public List<DimensionsInputValueAxis> YAxis { get; set; }
        
        public DimensionsInputValueAggregation Aggregation { get; set; }
        
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