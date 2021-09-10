using System.Text.Json.Serialization;

namespace Kustomaur.Models
{
    public class DashboardPropertiesMetadataModel
    {
        public object Value { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Type { get; set; }

        public T ValueAs<T>() => (T)Value;
    }
}