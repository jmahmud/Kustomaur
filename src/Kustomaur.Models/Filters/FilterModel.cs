using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace Kustomaur.Models.Filters
{
    public class FilterModel
    {
        [JsonPropertyName("key")]
        public string Property { get; private set; }
        public int Operator { get; private set; }
        public List<string> Values { get; private set; }
        
        public FilterModel(string property, FilterOperator @operator, List<string> values)
        {
            SetProperty(property);
            SetOperator(@operator);
            SetValues(values);
        }

        public void SetProperty(string property)
        {
            if (string.IsNullOrEmpty(property))
            {
                throw new Exception("Property cannot be null or empty!");
            }

            Property = property;
        }
        
        public void SetOperator(FilterOperator @operator)
        {
            Operator = (int)@operator;
        }
        
        public void SetValues(List<string> values)
        {
            if (values == null || values.Any(v => string.IsNullOrEmpty(v)))
            {
                throw new Exception("Values cannot be null or empty!");
            }
            
            Values = values;
        }
    }
}