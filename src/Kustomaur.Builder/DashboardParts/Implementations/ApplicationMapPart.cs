using Kustomaur.Models;

namespace Kustomaur.Dashboard.DashboardParts.Implementations
{
    public class ApplicationMapPart : DashboardPart
    {
        private string _componentName;
        public ApplicationMapPart()
        {
            WithRowSpan(3);
            WithColSpan(3);
        }

        public override Part GeneratePart()
        {
            _part.WithPosition(_x, _y, _rowSpan, _colSpan);
            if (_part.Metadata == null)
            {
                _part.Metadata = new PartMetadata();
            }
            _part.Metadata.WithType("Extension/AppInsightsExtension/PartType/AppMapGalPt");
            _part.Metadata.AddInput(new Input("ComponentId", value: BuildId(), isOptional: null));

            var settings = _part.Metadata.Settings as PartMetadataSettings;
            if (settings?.Content == null)
            {
                _part.Metadata.Settings = new { };                
            }

            _part.Metadata.Asset = new Asset()
            {
                IdInputName = "ComponentId",
                Type = "ApplicationInsights"
            };
            return _part;
        }

        public ApplicationMapPart WithComponentName(string componentName)
        {
            _componentName = componentName;
            return this;
        }

        private string BuildId()
        {
            return
                $"/subscriptions/{_subscriptionId}/resourceGroups/{_resourceGroup}/providers/Microsoft.Insights/components/{_componentName}";
        }
    }
}