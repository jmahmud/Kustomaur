using System.Collections.Generic;

namespace Kustomaur.Models
{
    public class Parts : DictionaryBase<Part>
    {
        public Parts WithPart(Part part)
        {
            WithItem(part);
            return this;
        }
    }
}