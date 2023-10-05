using System.Collections.Generic;
using Kustomaur.Dashboard;
using Kustomaur.Dashboard.DashboardParts.Implementations;
using Kustomaur.Dashboard.DashboardParts.Implementations.SubParts;
using Kustomaur.Dashboard.Implementation;
using Xunit;

namespace Kustomaur.Builder.Tests.DashboardParts
{
    public class MonitorChartPartTests
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
                    .AddPart(new MonitorChartPart()
                        .WithTitle("A Title")
                        .WithTitleKind(1)
                        .WithMetric(new ChartInputValueChartMetric
                        {
                            ResourceMetadata = new ChartInputValueChartMetricResourceMetadata
                            {
                                Id =
                                    $"/subscriptions/{subscriptionId}/resourceGroups/{resourceGroup}/providers/Microsoft.Web/serverFarms/{name}"
                            },
                            Name = "GraphName",
                            AggregationType = 4,
                            Namespace = "microsoft.web/serverfarms",
                            MetricVisualization = new MetricVisualisation
                            {
                                DisplayName = "A Better Graph Name"
                            }
                        })
                        .WithVisualisation(new ChartInputValueChartVisualisation
                        {
                            ChartType = 2,
                            LegendVisualization = new LegendVisualisation
                            {
                                Position = 2,
                                IsVisible = true,
                                HideSubtitle = false
                            },
                            AxisVisualization = new AxisVisualisation
                            {
                                X = new AxisVisualisationAxis { AxisType = 2, IsVisible = true },
                                Y = new AxisVisualisationAxis { AxisType = 1, IsVisible = true }
                            }
                        })
                        .WithGrouping(new ChartInputValueChartGrouping
                        {
                            Dimension = "Instance",
                            Sort = 2,
                            Top = 2
                        })
                        .WithTimespan(new ChartInputValueChartTimespan
                        {
                            Relative = new ChartInputValueChartTimespanRelative
                            {
                                Duration = 86400000
                            },
                            ShowUTCTime = false,
                            Grain = 1
                        })
                    ))
                .Build();

            // Assert
            Assert.Equal(
                "{\"lenses\":{\"0\":{\"order\":0,\"parts\":{\"0\":{\"position\":{\"x\":0,\"y\":0,\"colSpan\":3,\"rowSpan\":3},\"metadata\":{\"type\":\"Extension/HubsExtension/PartType/MonitorChartPart\",\"inputs\":[{\"name\":\"sharedTimeRange\",\"isOptional\":true},{\"name\":\"options\",\"isOptional\":true,\"value\":{\"chart\":{\"titleKind\":1,\"title\":\"A Title\",\"timespan\":{\"grain\":1,\"showUTCTime\":false,\"relative\":{\"duration\":86400000}},\"grouping\":{\"dimension\":\"Instance\",\"sort\":2,\"top\":2},\"visualization\":{\"chartType\":2,\"legendVisualization\":{\"isVisible\":true,\"position\":2,\"hideSubtitle\":false},\"axisVisualization\":{\"x\":{\"isVisible\":true,\"axisType\":2},\"y\":{\"isVisible\":true,\"axisType\":1}},\"disablePinning\":true},\"metrics\":[{\"resourceMetadata\":{\"id\":\"/subscriptions/b42aaad0-2122-4826-aa7c-b49250f0c3f9/resourceGroups/testdashboards/providers/Microsoft.Web/serverFarms/ce043e69-f9f3-4618-bbe2-2a93e769b56d\"},\"name\":\"GraphName\",\"aggregationType\":4,\"namespace\":\"microsoft.web/serverfarms\",\"metricVisualization\":{\"displayName\":\"A Better Graph Name\"}}]}}}],\"asset\":{\"idInputName\":\"ComponentId\",\"type\":\"ApplicationInsights\"}}}}}},\"metadata\":{\"model\":{\"timeRange\":{\"value\":{\"relative\":{\"duration\":24,\"timeUnit\":1}},\"type\":\"MsPortalFx.Composition.Configuration.ValueTypes.TimeRange\"},\"filters\":{\"value\":{\"MsPortalFx_TimeRange\":{\"model\":{\"format\":\"utc\",\"granularity\":\"auto\",\"relative\":\"24h\"},\"displayCache\":{\"name\":\"UTC Time\",\"value\":\"Past 24 hours\"},\"filteredPartIds\":[]}}},\"filterLocale\":{\"value\":\"en-us\"}}}}",
                Generator.Generate(dashboard.Properties));
        }

        [Fact]
        public void CanCreateMonitorChartPartWithFilters()
        {
            // Arrange
            var subscriptionId = "b42aaad0-2122-4826-aa7c-b49250f0c3f9";
            var resourceGroup = "testdashboards";
            var name = "ce043e69-f9f3-4618-bbe2-2a93e769b56d";

            // Act
            var dashboard = new DashboardBuilder()
                .WithName(name)
                .WithBuilder(new DashboardPartsBuilder()
                    .AddPart(new MonitorChartPart()
                        .WithTitle("A Title")
                        .WithTitleKind(1)
                        .WithMetric(new ChartInputValueChartMetric
                        {
                            ResourceMetadata = new ChartInputValueChartMetricResourceMetadata
                            {
                                Id =
                                    $"/subscriptions/{subscriptionId}/resourceGroups/{resourceGroup}/providers/Microsoft.Web/serverFarms/{name}"
                            },
                            Name = "GraphName",
                            AggregationType = 2,
                            Namespace = "microsoft.web/serverfarms",
                            MetricVisualization = new MetricVisualisation
                            {
                                DisplayName = "A Better Graph Name"
                            }
                        })
                        .WithVisualisation(new ChartInputValueChartVisualisation
                        {
                            ChartType = 2,
                            LegendVisualization = new LegendVisualisation
                            {
                                Position = 2,
                                IsVisible = true,
                                HideSubtitle = false
                            },
                            AxisVisualization = new AxisVisualisation
                            {
                                X = new AxisVisualisationAxis { AxisType = 2, IsVisible = true },
                                Y = new AxisVisualisationAxis { AxisType = 1, IsVisible = true }
                            }
                        })
                        .WithGrouping(new ChartInputValueChartGrouping
                        {
                            Dimension = "EntityName",
                            Sort = 2,
                            Top = 50
                        })
                        .WithFilters(new Dictionary<string, List<string>>
                        {
                            { "EntityName", new List<string> { "value1", "value2" } },
                            { "AnotherFilter", new List<string> { "filterValue" } }
                        })
                        .WithTimespan(new ChartInputValueChartTimespan
                        {
                            Relative = new ChartInputValueChartTimespanRelative
                            {
                                Duration = 86400000
                            },
                            ShowUTCTime = false,
                            Grain = 1
                        }))
                )
                .Build();

            // Assert
            var expectedJson =
                "{\"lenses\":{\"0\":{\"order\":0,\"parts\":{\"0\":{\"position\":{\"x\":0,\"y\":0,\"colSpan\":11,\"rowSpan\":6},\"metadata\":{\"inputs\":[{\"name\":\"sharedTimeRange\",\"isOptional\":true},{\"name\":\"options\",\"value\":{\"chart\":{\"titleKind\":1,\"title\":\"A Title\",\"timespan\":{\"grain\":1,\"showUTCTime\":false,\"relative\":{\"duration\":86400000}},\"grouping\":{\"dimension\":\"EntityName\",\"sort\":2,\"top\":50},\"visualization\":{\"chartType\":2,\"legendVisualization\":{\"isVisible\":true,\"position\":2,\"hideSubtitle\":false},\"axisVisualization\":{\"x\":{\"isVisible\":true,\"axisType\":2},\"y\":{\"isVisible\":true,\"axisType\":1}},\"disablePinning\":true},\"metrics\":[{\"resourceMetadata\":{\"id\":\"/subscriptions/SUB_ID/resourceGroups/RESOURCE_GROUP/providers/Microsoft.Web/serverFarms/ce043e69-f9f3-4618-bbe2-2a93e769b56d\"},\"name\":\"GraphName\",\"aggregationType\":2,\"namespace\":\"microsoft.web/serverfarms\",\"metricVisualization\":{\"displayName\":\"A Better Graph Name\"}}]}}}],\"type\":\"Extension/HubsExtension/PartType/MonitorChartPart\",\"isOptional\":true}}},\"type\":\"Extension/HubsExtension/PartType/MonitorChartPart\",\"settings\":{\"content\":{\"options\":{\"chart\":{\"metrics\":[{\"resourceMetadata\":{\"id\":\"/subscriptions/SUB_ID/resourceGroups/RESOURCE_GROUP/providers/Microsoft.Web/serverFarms/ce043e69-f9f3-4618-bbe2-2a93e769b56d\"},\"name\":\"GraphName\",\"aggregationType\":2,\"namespace\":\"microsoft.web/serverfarms\",\"metricVisualization\":{\"displayName\":\"A Better Graph Name\"}}],\"title\":\"A Title\",\"titleKind\":1,\"visualization\":{\"chartType\":2,\"legendVisualization\":{\"isVisible\":true,\"position\":2,\"hideSubtitle\":false},\"axisVisualization\":{\"x\":{\"isVisible\":true,\"axisType\":2},\"y\":{\"isVisible\":true,\"axisType\":1}}},\"grouping\":{\"dimension\":\"EntityName\",\"sort\":2,\"top\":50},\"timespan\":{\"relative\":{\"duration\":86400000},\"showUTCTime\":false,\"grain\":1}}}}},\"metadata\":{\"model\":{\"timeRange\":{\"value\":{\"relative\":{\"duration\":24,\"timeUnit\":1}},\"type\":\"MsPortalFx.Composition.Configuration.ValueTypes.TimeRange\"},\"filters\":{\"value\":{\"EntityName\":{\"model\":{\"operator\":\"equals\",\"values\":[\"value1\",\"value2\"]}},\"AnotherFilter\":{\"model\":{\"operator\":\"equals\",\"values\":[\"filterValue\"]}}}},\"filterLocale\":{\"value\":\"en-us\"}}}}}},\"name\":\" - Dead Letters Clone\",\"type\":\"Microsoft.Portal/dashboards\",\"location\":\"INSERT LOCATION\",\"tags\":{\"hidden-title\":\" - Dead Letters Clone\"},\"apiVersion\":\"2015-08-01-preview\"}}";

            var actualJson = Generator.Generate(dashboard.Properties);

            Assert.Equal(expectedJson, actualJson);
        }
    }
}