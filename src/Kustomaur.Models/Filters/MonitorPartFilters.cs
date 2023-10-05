using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kustomaur.Models.Filters
{
    public class MonitorPartFilters
    {
        public List<FilterModel> Filter { get; set; }

        public void Add(List<FilterModel> filterModel)
        {
            Filter = filterModel;
        }
    }
}
