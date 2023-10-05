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
        public string Operator { get; set; }
        public List<string> Values { get; set; }
    }
}
