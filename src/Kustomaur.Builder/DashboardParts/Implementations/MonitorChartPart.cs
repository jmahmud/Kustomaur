using Kustomaur.Models;

namespace Kustomaur.Dashboard.DashboardParts.Implementations
{
    public class MonitorChartPart : DashboardPart
    {
        public MonitorChartPart()
        {
            WithRowSpan(3);
            WithColSpan(3);
        }
        
        public override Part GeneratePart()
        {
            _part.WithPosition(_x, _y, _rowSpan, _colSpan);
            _part.Metadata = new PartMetadata();
            _part.Metadata.WithType("Extension/HubsExtension/PartType/MonitorChartPart");
            _part.Metadata.AddInput(new Input("sharedTimeRange", isOptional: true));
            _part.Metadata.AddInput(new Input(name: "options"));
            _part.Metadata.Settings = new { };
            _part.Metadata.Asset = new Asset()
            {
                IdInputName = "ComponentId",
                Type = "ApplicationInsights"
            };
            return _part;
        }
    }
}