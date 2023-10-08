using System;
using System.Collections.Generic;

namespace Kustomaur.Models.Filters
{
    public class FilterModel
    {
        public string Operator { get; set; }

        public List<string> Values { get; set; }
    }
}