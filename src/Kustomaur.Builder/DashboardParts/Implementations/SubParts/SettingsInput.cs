using System.Collections.Generic;
using System.Text.Json.Serialization;
using Kustomaur.Models;

namespace Kustomaur.Dashboard.DashboardParts.Implementations.SubParts
{
    public class SettingsInput : Input
    {
        [JsonPropertyName("Dimensions")]
        public DimensionsInputValue Dimensions { get; set; }

        public SettingsInput() : base("Settings")
        {}
}
}