using System.Text.Json;

namespace Kustomaur.Dashboard
{
    public static class Generator
    {
        public static string Generate(Models.Dashboard dashboard)
        {
            return JsonSerializer.Serialize(dashboard, Models.Generator.JsonSerializerOptions);
        }
    }
}