using System.Linq;
using Kustomaur.Dashboard;
using Kustomaur.Dashboard.DashboardParts.Implementations;
using Kustomaur.Dashboard.Implementation;
using Xunit;

namespace Kustomaur.Builder.Tests.DashboardParts
{
    public class ApplicationMapPartTests
    {
        [Fact]
        public void CanCreateApplicationMapPart()
        {
            var subscriptionId = "b42aaad0-2122-4826-aa7c-b49250f0c3f9";
            var resourceGroup = "testdashboards";
            var name = "ce043e69-f9f3-4618-bbe2-2a93e769b56d";
            
            // Act
            var dashboard = new DashboardBuilder()
                .WithName(name)
                .WithBuilder(new DashboardPartsBuilder()
                    .AddPart(new ApplicationMapPart()
                        .WithComponentName("MyFirstApplicationMap")
                        .WithResourceGroup(resourceGroup)
                        .WithSubscriptionId(subscriptionId)
                    ))
                .Build();

            // Assert
            Assert.Equal("{\"lenses\":{\"0\":{\"order\":0,\"parts\":{\"0\":{\"position\":{\"x\":0,\"y\":0,\"colSpan\":3,\"rowSpan\":3},\"metadata\":{\"type\":\"Extension/AppInsightsExtension/PartType/AppMapGalPt\",\"inputs\":[{\"name\":\"ComponentId\",\"value\":\"/subscriptions/b42aaad0-2122-4826-aa7c-b49250f0c3f9/resourceGroups/testdashboards/providers/Microsoft.Insights/components/MyFirstApplicationMap\"}],\"asset\":{\"idInputName\":\"ComponentId\",\"type\":\"ApplicationInsights\"},\"settings\":{}}}}}},\"metadata\":{\"model\":{\"timeRange\":{\"value\":{\"relative\":{\"duration\":24,\"timeUnit\":1}},\"type\":\"MsPortalFx.Composition.Configuration.ValueTypes.TimeRange\"},\"filters\":{\"value\":{\"MsPortalFx_TimeRange\":{\"model\":{\"format\":\"utc\",\"granularity\":\"auto\",\"relative\":\"24h\"},\"displayCache\":{\"name\":\"UTC Time\",\"value\":\"Past 24 hours\"},\"filteredPartIds\":[]}}},\"filterLocale\":{\"value\":\"en-us\"}}}}",
                Generator.Generate(dashboard.Properties));
        }
    }
}