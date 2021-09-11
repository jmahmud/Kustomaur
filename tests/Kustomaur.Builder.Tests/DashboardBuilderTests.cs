using System;
using System.Collections.Generic;
using System.IO;
using Kustomaur.Dashboard;
using Kustomaur.Dashboard.DashboardParts;
using Kustomaur.Dashboard.Implementation;
using Kustomaur.Models;
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
                "{\"properties\":{\"lenses\":null,\"metadata\":{\"model\":{\"timeRange\":{\"value\":{\"relative\":{\"duration\":24,\"timeUnit\":1}},\"type\":\"MsPortalFx.Composition.Configuration.ValueTypes.TimeRange\"},\"filters\":{\"value\":{\"MsPortalFx_TimeRange\":{\"model\":{\"format\":\"utc\",\"granularity\":\"auto\",\"relative\":\"24h\"},\"displayCache\":{\"name\":\"UTC Time\",\"value\":\"Past 24 hours\"},\"filteredPartIds\":[]}}},\"filterLocale\":{\"value\":\"en-us\"}}}},\"name\":\"ce043e69-f9f3-4618-bbe2-2a93e769b56d\",\"type\":\"Microsoft.Portal/dashboards\",\"location\":null,\"tags\":{\"hidden-title\":\"ce043e69-f9f3-4618-bbe2-2a93e769b56d\"}}",
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
                "{\"properties\":{\"lenses\":{\"0\":{\"order\":0,\"parts\":{\"0\":{\"position\":{\"x\":0,\"y\":0,\"colSpan\":3,\"rowSpan\":3},\"metadata\":{\"type\":\"Extension/HubsExtension/PartType/MarkdownPart\",\"inputs\":[],\"asset\":null,\"settings\":{\"content\":{\"settings\":{\"content\":\"My first thing\",\"title\":\"\",\"subtitle\":\"\",\"markdownSource\":1,\"markdownUri\":null}}}}}}}},\"metadata\":{\"model\":{\"timeRange\":{\"value\":{\"relative\":{\"duration\":24,\"timeUnit\":1}},\"type\":\"MsPortalFx.Composition.Configuration.ValueTypes.TimeRange\"},\"filters\":{\"value\":{\"MsPortalFx_TimeRange\":{\"model\":{\"format\":\"utc\",\"granularity\":\"auto\",\"relative\":\"24h\"},\"displayCache\":{\"name\":\"UTC Time\",\"value\":\"Past 24 hours\"},\"filteredPartIds\":[]}}},\"filterLocale\":{\"value\":\"en-us\"}}}},\"name\":\"ce043e69-f9f3-4618-bbe2-2a93e769b56d\",\"type\":\"Microsoft.Portal/dashboards\",\"location\":null,\"tags\":{\"hidden-title\":\"ce043e69-f9f3-4618-bbe2-2a93e769b56d\"}}",
                Dashboard.Generator.Generate(dashboard));
        }

        [Fact]
        public void CanCreateADashboardWithMultipleParts()
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
                    .AddPart(new MarkdownPart(content:"My first thing")
                        .WithColSpan(5)
                        .WithRowSpan(5)
                        .GeneratePart())
                    .AddPart(new MarkdownPart("My second thing")
                        .WithX(5)
                        .WithY(0)
                        .WithColSpan(7)
                        .WithRowSpan(7)
                        .GeneratePart()))
                .Build();
            
            Assert.Equal(
                "{\"properties\":{\"lenses\":{\"0\":{\"order\":0,\"parts\":{\"0\":{\"position\":{\"x\":0,\"y\":0,\"colSpan\":5,\"rowSpan\":5},\"metadata\":{\"type\":\"Extension/HubsExtension/PartType/MarkdownPart\",\"inputs\":[],\"asset\":null,\"settings\":{\"content\":{\"settings\":{\"content\":\"My first thing\",\"title\":\"\",\"subtitle\":\"\",\"markdownSource\":1,\"markdownUri\":null}}}}},\"1\":{\"position\":{\"x\":5,\"y\":0,\"colSpan\":7,\"rowSpan\":7},\"metadata\":{\"type\":\"Extension/HubsExtension/PartType/MarkdownPart\",\"inputs\":[],\"asset\":null,\"settings\":{\"content\":{\"settings\":{\"content\":\"My second thing\",\"title\":\"\",\"subtitle\":\"\",\"markdownSource\":1,\"markdownUri\":null}}}}}}}},\"metadata\":{\"model\":{\"timeRange\":{\"value\":{\"relative\":{\"duration\":24,\"timeUnit\":1}},\"type\":\"MsPortalFx.Composition.Configuration.ValueTypes.TimeRange\"},\"filters\":{\"value\":{\"MsPortalFx_TimeRange\":{\"model\":{\"format\":\"utc\",\"granularity\":\"auto\",\"relative\":\"24h\"},\"displayCache\":{\"name\":\"UTC Time\",\"value\":\"Past 24 hours\"},\"filteredPartIds\":[]}}},\"filterLocale\":{\"value\":\"en-us\"}}}},\"name\":\"ce043e69-f9f3-4618-bbe2-2a93e769b56d\",\"type\":\"Microsoft.Portal/dashboards\",\"location\":null,\"tags\":{\"hidden-title\":\"ce043e69-f9f3-4618-bbe2-2a93e769b56d\"}}",
                Dashboard.Generator.Generate(dashboard));
         }
        
         [Fact]
        public void CanCreateADashboardWithMultiplePartsAsARow()
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
                    .AddPartsAsRow(new List<Part>()
                    {
                        new MarkdownPart(content:"# my first part in 1st row")
                            .WithColSpan(3)
                            .GeneratePart(),
                        new MarkdownPart(content:"# my second part in 1st row")
                            .WithColSpan(3)
                            .GeneratePart()
                    }, out int firstRowMaxY) // firstRowMaxY is what we should set the new row Y value
                    .AddPartsAsRow(new List<Part>()
                    {
                        new MarkdownPart(content:"# my first part in 2nd row")
                            .WithColSpan(4)
                            .GeneratePart(),
                        new MarkdownPart(content:"# my second part in 2nd row")
                            .WithColSpan(4)
                            .GeneratePart()
                    }, out int secondRowMaxY, startYPos:firstRowMaxY)
                    .AddPartsAsRow(new List<Part>()
                    {
                        new MarkdownPart(content:"# my first part in 3rd row")
                            .WithColSpan(5)
                            .GeneratePart(),
                        new MarkdownPart(content:"# my second part in 3rd row")
                            .WithColSpan(5)
                            .GeneratePart()
                    }, out int thirdRowMaxY, startYPos:secondRowMaxY))
                .Build();
            
            Assert.Equal(
                "{\"properties\":{\"lenses\":{\"0\":{\"order\":0,\"parts\":{\"0\":{\"position\":{\"x\":0,\"y\":0,\"colSpan\":3,\"rowSpan\":3},\"metadata\":{\"type\":\"Extension/HubsExtension/PartType/MarkdownPart\",\"inputs\":[],\"asset\":null,\"settings\":{\"content\":{\"settings\":{\"content\":\"# my first part in 1st row\",\"title\":\"\",\"subtitle\":\"\",\"markdownSource\":1,\"markdownUri\":null}}}}},\"1\":{\"position\":{\"x\":3,\"y\":0,\"colSpan\":3,\"rowSpan\":3},\"metadata\":{\"type\":\"Extension/HubsExtension/PartType/MarkdownPart\",\"inputs\":[],\"asset\":null,\"settings\":{\"content\":{\"settings\":{\"content\":\"# my second part in 1st row\",\"title\":\"\",\"subtitle\":\"\",\"markdownSource\":1,\"markdownUri\":null}}}}},\"2\":{\"position\":{\"x\":0,\"y\":3,\"colSpan\":4,\"rowSpan\":3},\"metadata\":{\"type\":\"Extension/HubsExtension/PartType/MarkdownPart\",\"inputs\":[],\"asset\":null,\"settings\":{\"content\":{\"settings\":{\"content\":\"# my first part in 2nd row\",\"title\":\"\",\"subtitle\":\"\",\"markdownSource\":1,\"markdownUri\":null}}}}},\"3\":{\"position\":{\"x\":4,\"y\":3,\"colSpan\":4,\"rowSpan\":3},\"metadata\":{\"type\":\"Extension/HubsExtension/PartType/MarkdownPart\",\"inputs\":[],\"asset\":null,\"settings\":{\"content\":{\"settings\":{\"content\":\"# my second part in 2nd row\",\"title\":\"\",\"subtitle\":\"\",\"markdownSource\":1,\"markdownUri\":null}}}}},\"4\":{\"position\":{\"x\":0,\"y\":6,\"colSpan\":5,\"rowSpan\":3},\"metadata\":{\"type\":\"Extension/HubsExtension/PartType/MarkdownPart\",\"inputs\":[],\"asset\":null,\"settings\":{\"content\":{\"settings\":{\"content\":\"# my first part in 3rd row\",\"title\":\"\",\"subtitle\":\"\",\"markdownSource\":1,\"markdownUri\":null}}}}},\"5\":{\"position\":{\"x\":5,\"y\":6,\"colSpan\":5,\"rowSpan\":3},\"metadata\":{\"type\":\"Extension/HubsExtension/PartType/MarkdownPart\",\"inputs\":[],\"asset\":null,\"settings\":{\"content\":{\"settings\":{\"content\":\"# my second part in 3rd row\",\"title\":\"\",\"subtitle\":\"\",\"markdownSource\":1,\"markdownUri\":null}}}}}}}},\"metadata\":{\"model\":{\"timeRange\":{\"value\":{\"relative\":{\"duration\":24,\"timeUnit\":1}},\"type\":\"MsPortalFx.Composition.Configuration.ValueTypes.TimeRange\"},\"filters\":{\"value\":{\"MsPortalFx_TimeRange\":{\"model\":{\"format\":\"utc\",\"granularity\":\"auto\",\"relative\":\"24h\"},\"displayCache\":{\"name\":\"UTC Time\",\"value\":\"Past 24 hours\"},\"filteredPartIds\":[]}}},\"filterLocale\":{\"value\":\"en-us\"}}}},\"name\":\"ce043e69-f9f3-4618-bbe2-2a93e769b56d\",\"type\":\"Microsoft.Portal/dashboards\",\"location\":null,\"tags\":{\"hidden-title\":\"ce043e69-f9f3-4618-bbe2-2a93e769b56d\"}}",
                Dashboard.Generator.Generate(dashboard));
         }
        
    }
}