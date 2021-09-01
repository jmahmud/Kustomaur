using System.Text.Json;

namespace Kustomaur.Models
{
    public static class Generator
    {
        public static string Generate(DashboardProperties properties)
        {
            return JsonSerializer.Serialize(properties, JsonSerializerOptions);
        }

        public static JsonSerializerOptions JsonSerializerOptions => new JsonSerializerOptions()
            { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    }
}