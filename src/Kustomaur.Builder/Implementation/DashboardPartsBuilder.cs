using System.Collections.Generic;
using Kustomaur.Models;

namespace Kustomaur.Dashboard.Implementation
{
    public class DashboardPartsBuilder : IDashboardComponetBuilder
    {
        public List<Part> DashboardParts = new List<Part>();
        
        public void Build(Models.Dashboard dashboard)
        {
            throw new System.NotImplementedException();
        }

        public void AddPart(Part part, bool autoCalculatePosition = true)
        {
            DashboardParts.Add(part);
        }
    }
}