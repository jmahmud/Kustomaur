using System.Collections.Generic;
using System.Linq;
using Kustomaur.Dashboard;
using Kustomaur.Dashboard.DashboardParts.Implementations;
using Kustomaur.Dashboard.DashboardParts.Implementations.SubParts;
using Kustomaur.Dashboard.Implementation;
using Kustomaur.Models;
using Xunit;

namespace Kustomaur.Builder.Tests.DashboardParts
{
    public class LogsDashboardPartTests
    {
        [Fact]
        public void CanCreateLogsDashboardPart()
        {
            //arrange
            // var subscriptionId = "b42aaad0-2122-4826-aa7c-b49250f0c3f9";
            // var resourceGroup = Guid.NewGuid().ToString();
            var subscriptionId = "b42aaad0-2122-4826-aa7c-b49250f0c3f9";
            var resourceGroup = "testdashboards";
            var name = "ce043e69-f9f3-4618-bbe2-2a93e769b56d";
            
            // Act
            var dashboard = new DashboardBuilder()
                .WithName(name)
                .WithBuilder(new DashboardPartsBuilder()
                    .AddPart(new LogsDashboardPart()
                        .WithTitle("My First Logs Dashboard Part")
                        .WithSubTitle("My First Subtitle")
                        .WithInsightsComponentName("HwEgWebAppJosh")
                        .WithQuery("requests\n| where success == false\n| render barchart\n")
                        .WithSubscriptionId(subscriptionId)
                        .WithResourceGroup(resourceGroup)
                        .GeneratePart()))
                .Build();

            var expectedPartId = dashboard.Properties.Lenses[0].Parts[0].Metadata.Inputs.First(x => x.Name == "PartId").Value;
            
            // Assert
            Assert.Equal(
                "{\"lenses\":{\"0\":{\"order\":0,\"parts\":{\"0\":{\"position\":{\"x\":0,\"y\":0,\"colSpan\":3,\"rowSpan\":3},\"metadata\":{\"type\":\"Extension/Microsoft_OperationsManagementSuite_Workspace/PartType/LogsDashboardPart\",\"inputs\":[{\"name\":\"resourceTypeMode\",\"isOptional\":true},{\"name\":\"ComponentId\",\"isOptional\":true},{\"name\":\"Scope\",\"isOptional\":true,\"value\":{\"resourceIds\":[\"/subscriptions/b42aaad0-2122-4826-aa7c-b49250f0c3f9/resourceGroups/testdashboards/providers/microsoft.insights/components/HwEgWebAppJosh\"]}},{\"name\":\"PartId\",\"isOptional\":true,\"value\":\"" + expectedPartId + "\"},{\"name\":\"Version\",\"isOptional\":true,\"value\":\"2.0\"},{\"name\":\"TimeRange\",\"isOptional\":true,\"value\":\"P1D\"},{\"name\":\"DashboardId\",\"isOptional\":true},{\"name\":\"DraftRequestParameters\",\"isOptional\":true},{\"name\":\"Query\",\"isOptional\":true,\"value\":\"requests\\n| where success == false\\n| render barchart\\n\"},{\"name\":\"SpecificChart\",\"isOptional\":true},{\"name\":\"ControlType\",\"isOptional\":true,\"value\":\"AnalyticsGrid\"},{\"name\":\"LegendOptions\",\"isOptional\":true},{\"name\":\"Dimensions\",\"isOptional\":true},{\"name\":\"PartTitle\",\"isOptional\":true,\"value\":\"My First Logs Dashboard Part\"},{\"name\":\"PartSubTitle\",\"isOptional\":true,\"value\":\"My First Subtitle\"},{\"name\":\"IsQueryContainTimeRange\",\"isOptional\":true,\"value\":false}],\"settings\":{}}}}}},\"metadata\":{\"model\":{\"timeRange\":{\"value\":{\"relative\":{\"duration\":24,\"timeUnit\":1}},\"type\":\"MsPortalFx.Composition.Configuration.ValueTypes.TimeRange\"},\"filters\":{\"value\":{\"MsPortalFx_TimeRange\":{\"model\":{\"format\":\"utc\",\"granularity\":\"auto\",\"relative\":\"24h\"},\"displayCache\":{\"name\":\"UTC Time\",\"value\":\"Past 24 hours\"},\"filteredPartIds\":[]}}},\"filterLocale\":{\"value\":\"en-us\"}}}}",
                Generator.Generate(dashboard.Properties));
        }
        
        
        
        [Fact]
        public void CanCreateLogsDashboardPartAsStackedBar()
        {  
            var dashboard = new DashboardBuilder()
                .WithName("testdashboard")
                .WithBuilder(new DashboardPartsBuilder()
                    .AddPart(new LogsDashboardPart()
                        .WithTitle("My First Logs Dashboard Part")
                        .WithSubTitle("My First Subtitle")
                        .WithInsightsComponentName("HwEgWebAppJosh")
                        .WithQuery("requests\n | where success == true \n| summarize sum(itemCount) by url, bin(timestamp, 1m)\n| render columnchart\n")
                        .WithSpecificChart(InputSpecificChart.StackedColumn)
                        .WithControlType(InputControlType.FrameControlChart)
                        .WithDimension(new DimensionsInput()
                        {
                            Name = "Dimensions",
                            Value = new DimensionsInputValue()
                            {
                                XAxis = new DimensionsInputValueAxis { Name = "timestamp", Type = "datetime" },
                                YAxis = new List<DimensionsInputValueAxis>() { new DimensionsInputValueAxis { Name = "sum_itemCount", Type = "long" }},
                                SplitBy = new List<object>(){ new DimensionsInputValueAxis { Name = "url", Type = "string" } },
                                Aggregation = DimensionsInputValueAggregation.Sum
                            }
                        })
                        .WithRowSpan(6)
                        .WithColSpan(6)
                        .WithSubscriptionId("b42aaad0-2122-4826-aa7c-b49250f0c3f9")
                        .WithResourceGroup("testdashboards")
                        .GeneratePart()))
                .Build();

            
            var expectedPartId = dashboard.Properties.Lenses[0].Parts[0].Metadata.Inputs.First(x => x.Name == "PartId").Value;
            
            // Assert
            Assert.Equal(
                   "{\"lenses\":{\"0\":{\"order\":0,\"parts\":{\"0\":{\"position\":{\"x\":0,\"y\":0,\"colSpan\":6,\"rowSpan\":6},\"metadata\":{\"type\":\"Extension/Microsoft_OperationsManagementSuite_Workspace/PartType/LogsDashboardPart\",\"inputs\":[{\"name\":\"resourceTypeMode\",\"isOptional\":true},{\"name\":\"ComponentId\",\"isOptional\":true},{\"name\":\"Scope\",\"isOptional\":true,\"value\":{\"resourceIds\":[\"/subscriptions/b42aaad0-2122-4826-aa7c-b49250f0c3f9/resourceGroups/testdashboards/providers/microsoft.insights/components/HwEgWebAppJosh\"]}},{\"name\":\"PartId\",\"isOptional\":true,\"value\":\"" + expectedPartId + "\"},{\"name\":\"Version\",\"isOptional\":true,\"value\":\"2.0\"},{\"name\":\"TimeRange\",\"isOptional\":true,\"value\":\"P1D\"},{\"name\":\"DashboardId\",\"isOptional\":true},{\"name\":\"DraftRequestParameters\",\"isOptional\":true},{\"name\":\"Query\",\"isOptional\":true,\"value\":\"requests\\n | where success == true \\n| summarize sum(itemCount) by url, bin(timestamp, 1m)\\n| render columnchart\\n\"},{\"name\":\"SpecificChart\",\"isOptional\":true,\"value\":\"StackedColumn\"},{\"name\":\"ControlType\",\"isOptional\":true,\"value\":\"FrameControlChart\"},{\"name\":\"LegendOptions\",\"isOptional\":true,\"value\":{\"isEnabled\":true,\"position\":\"Bottom\"}},{\"name\":\"Dimensions\",\"isOptional\":true,\"value\":{\"xAxis\":{\"name\":\"timestamp\",\"type\":\"datetime\"},\"yAxis\":[{\"name\":\"sum_itemCount\",\"type\":\"long\"}],\"aggregation\":\"Sum\",\"splitBy\":[{\"name\":\"url\",\"type\":\"string\"}]}},{\"name\":\"PartTitle\",\"isOptional\":true,\"value\":\"My First Logs Dashboard Part\"},{\"name\":\"PartSubTitle\",\"isOptional\":true,\"value\":\"My First Subtitle\"},{\"name\":\"IsQueryContainTimeRange\",\"isOptional\":true,\"value\":false}],\"settings\":{}}}}}},\"metadata\":{\"model\":{\"timeRange\":{\"value\":{\"relative\":{\"duration\":24,\"timeUnit\":1}},\"type\":\"MsPortalFx.Composition.Configuration.ValueTypes.TimeRange\"},\"filters\":{\"value\":{\"MsPortalFx_TimeRange\":{\"model\":{\"format\":\"utc\",\"granularity\":\"auto\",\"relative\":\"24h\"},\"displayCache\":{\"name\":\"UTC Time\",\"value\":\"Past 24 hours\"},\"filteredPartIds\":[]}}},\"filterLocale\":{\"value\":\"en-us\"}}}}",
                Generator.Generate(dashboard.Properties));
        }
    }
}