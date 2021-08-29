using System.Collections.Generic;

namespace Kustomaur.Models
{
    public class TimeRange
    {
        public string Type { get; set; }
        public Dictionary<string, object> Value { get; set; }
    }
}