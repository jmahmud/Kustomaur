using System;
using Kustomaur.Dashboard;
using Kustomaur.Dashboard.DashboardParts;
using Kustomaur.Dashboard.Implementation;
using Kustomaur.Dashboard.Implementation.DashboardMetadataModelBuilders;
using Kustomaur.Models;
using Kustomaur.Models.Filters;
using Xunit;

namespace Kustomaur.Builder.Tests
{
    public class DashboardBuilderTests
    {
        [Fact]
        public void CanCreateADashboard()
        {
            //arrange
            var subscriptionId = "6b062fa3-dbe5-4e4a-8d54-ce7286ec9ef6";
            var resourceGroup = Guid.NewGuid().ToString();
            var name = "ce043e69-f9f3-4618-bbe2-2a93e769b56d";

            // Act
            var dashboard = new DashboardBuilder()
                .WithSubscription(subscriptionId)
                .WithResourceGroup(resourceGroup)
                .WithName(name)
                .Build();

            // Assert
            Assert.Equal(
                "{\"properties\":{\"lenses\":null,\"metadata\":{\"model\":{\"timeRange\":{\"value\":{\"type\":\"MsPortalFx.Composition.Configuration.ValueTypes.TimeRange\",\"relative\":{\"duration\":24,\"timeUnit\":1}}},\"filterLocale\":{\"value\":\"en-us\"}}}},\"id\":\"/subscriptions/6b062fa3-dbe5-4e4a-8d54-ce7286ec9ef6/resourceGroups/dashboards/providers/Microsoft.Portal/dashboards/ce043e69-f9f3-4618-bbe2-2a93e769b56d\",\"name\":\"ce043e69-f9f3-4618-bbe2-2a93e769b56d\",\"type\":\"Microsoft.Portal/dashboards\",\"location\":null,\"tags\":{\"hidden-title\":\"ce043e69-f9f3-4618-bbe2-2a93e769b56d\"}}",
                Dashboard.Generator.Generate(dashboard));
        }
        
        [Fact]
        public void CanCreateADashboardWithPartsBuilder()
        {
            //arrange
            var subscriptionId = "6b062fa3-dbe5-4e4a-8d54-ce7286ec9ef6";
            var resourceGroup = Guid.NewGuid().ToString();
            var name = "ce043e69-f9f3-4618-bbe2-2a93e769b56d";
            
            // Act
            var dashboard = new DashboardBuilder()
                .WithSubscription(subscriptionId)
                .WithResourceGroup(resourceGroup)
                .WithName(name)
                .WithBuilder(new DashboardPartsBuilder()
                    .AddPart(new MarkdownPart(content:"My first thing")))
                .Build();

            // Assert
            Assert.Equal(
                "{\"properties\":{\"lenses\":{\"0\":{\"order\":0,\"parts\":{\"0\":{\"position\":{\"x\":0,\"y\":0,\"colSpan\":0,\"rowSpan\":0},\"metadata\":{\"type\":\"Extension/HubsExtension/PartType/MarkdownPart\",\"inputs\":[],\"asset\":null,\"settings\":{\"content\":{\"settings\":{\"content\":\"My first thing\",\"title\":\"\",\"subtitle\":\"\",\"markdownSource\":1,\"markdownUri\":null}}}}}}}},\"metadata\":{\"model\":{\"timeRange\":{\"value\":{\"type\":\"MsPortalFx.Composition.Configuration.ValueTypes.TimeRange\",\"relative\":{\"duration\":24,\"timeUnit\":1}}},\"filterLocale\":{\"value\":\"en-us\"}}}},\"id\":\"/subscriptions/6b062fa3-dbe5-4e4a-8d54-ce7286ec9ef6/resourceGroups/dashboards/providers/Microsoft.Portal/dashboards/ce043e69-f9f3-4618-bbe2-2a93e769b56d\",\"name\":\"ce043e69-f9f3-4618-bbe2-2a93e769b56d\",\"type\":\"Microsoft.Portal/dashboards\",\"location\":null,\"tags\":{\"hidden-title\":\"ce043e69-f9f3-4618-bbe2-2a93e769b56d\"}}",
                Dashboard.Generator.Generate(dashboard));
        }
    }
}