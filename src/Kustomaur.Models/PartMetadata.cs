using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Kustomaur.Models
{
    public class PartMetadata
    {
        public string Type { get; set; }
        public List<Input> Inputs { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Asset Asset { get; set; } 
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object Settings { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, object> Filters { get; set; }

        public PartMetadata()
        {
            Settings = new PartMetadataSettings();
            Filters = null;
        }

        public PartMetadata WithType(string type)
        {
            Type = type;
            return this;
        }
        
        public PartMetadata WithAsset(Asset asset)
        {
            Asset = asset;
            return this;
        }
        
        public PartMetadata AddInput(Input input)
        {
            if (Inputs == null)
            {
                Inputs = new List<Input>();
            }
            
            Inputs.Add(input);
            return this;
        }
    }
}