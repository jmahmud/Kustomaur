using System.Collections.Generic;

namespace Kustomaur.Models
{
    public class Dashboard
    {
        public DashboardProperties Properties { get; set; }
        //public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public Dictionary<string, string> Tags { get; set; }

        public Dashboard WithProperties(DashboardProperties dashboardProperties)
        {
            Properties = dashboardProperties;
            return this;
        }
    }
}