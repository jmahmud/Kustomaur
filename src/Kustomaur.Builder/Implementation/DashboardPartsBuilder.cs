using System.Collections.Generic;
using Kustomaur.Dashboard.DashboardParts;
using Kustomaur.Models;

namespace Kustomaur.Dashboard.Implementation
{
    public class DashboardPartsBuilder : IDashboardPartBuilder
    {
        private Parts _parts;

        public DashboardPartsBuilder()
        {
            _parts = new Parts();
        }
        
        public void Build(Models.Dashboard dashboard)
        {
            if (dashboard.Properties.Lenses == null)
            {
                dashboard.Properties.WithLenses(new Lenses());
                dashboard.Properties.Lenses.WithLense(new Lense());
            }
            
            dashboard.Properties.Lenses[0].WithParts(_parts);
        }

        public IDashboardPartBuilder AddPart(Part part)
        {
            _parts.WithPart(part);
            return this;
        }
        
        public IDashboardPartBuilder AddPart(DashboardPart dashboardPart)
        {
            _parts.WithPart(dashboardPart.GeneratePart());
            return this;
        }
        
        public IDashboardPartBuilder AddPartsAsRow(List<Part> parts)
        {
            return this;
        }
        public IDashboardPartBuilder AddPartsAsColumn(List<Part> parts)
        {
            return this;
        }
    }
}