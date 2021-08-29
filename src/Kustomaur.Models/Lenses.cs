using System.Collections;
using System.Collections.Generic;

namespace Kustomaur.Models
{
    public class Lenses : DictionaryBase<Lense>
    {
        public Lenses WithLense(Lense lense)
        {
            WithItem(lense);
            return this;
        }
    }
}