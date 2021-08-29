using System.Collections.Generic;
using System.Linq;
using Kustomaur.Models;

namespace Kustomaur.Dashboard.Implementation
{
    public class DashboardPartsBuilder : IDashboardComponetBuilder
    {
        public Models.Dashboard Dashboard;
        public List<Part> DashboardParts = new List<Part>();

        public DashboardPartsBuilder(Models.Dashboard dashboard)
        {
            Dashboard = dashboard;
        }
        public void Build()
        {
            throw new System.NotImplementedException();
        }

        public void AddPart(Part part)
        {
            var lense = Dashboard.Properties.Lenses[0];
            lense.Parts.WithPart(part);
        }
        
        public void AddPartToSameRow(Part part)
        {
        }
        
        public void AddPartToSameColumn(Part part)
        {
        }
    }
}