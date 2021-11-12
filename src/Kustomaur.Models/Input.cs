using System.Text.Json.Serialization;

namespace Kustomaur.Models
{
    public class Input
    {
        public string Name { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? IsOptional { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object Value { get; set; }

        public T ValueAs<T>()
        {
            return (T)Value;
        }

        public Input(string name, bool? isOptional = true, object value = null)
        {
            Name = name;
            IsOptional = isOptional;
            Value = value;
        }
    }
}