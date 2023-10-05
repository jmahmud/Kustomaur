using System.Collections.Generic;

namespace Kustomaur.Models.Filters
{
    public class FilterModel
    {
        public string Name { get; set; }
        public FilterOperator Operator { get; set; }
        public List<string> Values { get; set; }
    }
}
