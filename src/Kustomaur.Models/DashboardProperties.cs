using System;

namespace Kustomaur.Models
{
    public class DashboardProperties
    {
        public Lenses Lenses { get; set; }
        
        public PropertiesMetadata Metadata { get; set; }

        public DashboardProperties WithLenses(Lenses lenses)
        {
            Lenses = lenses;
            return this;
        }
    }
}