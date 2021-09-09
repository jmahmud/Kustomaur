using System.Text.Json;
using System.Text.Json.Serialization;
using Kustomaur.Models.Serialisation;

namespace Kustomaur.Models
{
    public static class Generator
    {
        public static string Generate(DashboardProperties properties)
        {
            return JsonSerializer.Serialize(properties, JsonSerializerOptions);
        }

        public static JsonSerializerOptions JsonSerializerOptions => new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            
        };
    }
}