using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kustomaur.Models.Filters
{
    public class FilterModel
    {
        public string Name { get; set; }
        public FilterOperator Operator { get; set; }
        public List<string> Values { get; set; }
    }

    public enum FilterOperator
    {
        Equals,
        GreaterThan,
        LessThan,
        // Add more operators as needed
    }
}
