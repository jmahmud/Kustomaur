using System.Collections.Generic;

namespace Kustomaur.Models
{
    public class PartMetadata
    {
        public string Type { get; set; }
        public List<Input> Inputs { get; set; }
        public Asset Asset { get; set; } 
        public PartMetadataSettings Settings { get; set; }

        public PartMetadata()
        {
            Settings = new PartMetadataSettings();
        }
    }
}