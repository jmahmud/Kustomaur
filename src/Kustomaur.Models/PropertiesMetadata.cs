using System.Collections.Generic;

namespace Kustomaur.Models
{
    public class PropertiesMetadata
    {
        public PropertiesMetadata()
        {
            Model = new Dictionary<string, DashboardPropertiesMetadataModel>();
        }
        public Dictionary<string, DashboardPropertiesMetadataModel> Model { get; set; }
    }
}