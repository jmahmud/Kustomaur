using System.Collections.Generic;

namespace Kustomaur.Models
{
    public abstract class DictionaryBase<TItem> : Dictionary<int, TItem>
    {
        public void WithItem(TItem item)
        {
            Add(Count, item);
        }
    }
}