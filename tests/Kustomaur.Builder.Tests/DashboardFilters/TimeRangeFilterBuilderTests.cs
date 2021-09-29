using System;
using Kustomaur.Dashboard;
using Kustomaur.Dashboard.Implementation.DashboardMetadataModelBuilders;
using Kustomaur.Models.Filters;
using Xunit;

namespace Kustomaur.Builder.Tests.DashboardFilters
{
    public class TimeRangeFilterBuilderTests
    {
        [Theory]
        [InlineData(MsPortalFxTimeRangeModelGranularity.Auto, "auto")]
        [InlineData(MsPortalFxTimeRangeModelGranularity.Hour1, "1h")]
        [InlineData(MsPortalFxTimeRangeModelGranularity.Hour6, "6h")]
        [InlineData(MsPortalFxTimeRangeModelGranularity.Hour12, "12h")]
        [InlineData(MsPortalFxTimeRangeModelGranularity.Minute1, "1m")]
        [InlineData(MsPortalFxTimeRangeModelGranularity.Minute5, "5m")]
        [InlineData(MsPortalFxTimeRangeModelGranularity.Minute15, "15m")]
        [InlineData(MsPortalFxTimeRangeModelGranularity.Minute30, "30m")]
        [InlineData(MsPortalFxTimeRangeModelGranularity.Month1, "1mo")]
        [InlineData(MsPortalFxTimeRangeModelGranularity.Week1, "1w")]
        public void CanCreateADashboardWithTimeRangeFilterAndSetGranularity(MsPortalFxTimeRangeModelGranularity granularity, string expectedString)
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
                .WithBuilder(new TimeRangeFilterBuilder()
                    .WithGranularity(granularity))
                .Build();

            // Assert
            Assert.Equal(
                "{\"properties\":{\"lenses\":null,\"metadata\":{\"model\":{\"filters\":{\"value\":{\"MsPortalFx_TimeRange\":{\"model\":{\"format\":\"utc\",\"granularity\":\"" + expectedString + "\",\"relative\":\"24h\"},\"displayCache\":{\"name\":\"UTC Time\",\"value\":\"Past 24 hours\"},\"filteredPartIds\":[]}}},\"timeRange\":{\"value\":{\"relative\":{\"duration\":24,\"timeUnit\":1}},\"type\":\"MsPortalFx.Composition.Configuration.ValueTypes.TimeRange\"},\"filterLocale\":{\"value\":\"en-us\"}}}},\"name\":\"ce043e69-f9f3-4618-bbe2-2a93e769b56d\",\"type\":\"Microsoft.Portal/dashboards\",\"location\":null,\"tags\":{\"hidden-title\":\"ce043e69-f9f3-4618-bbe2-2a93e769b56d\"}}",
                Dashboard.Generator.Generate(dashboard));
        }
        
        [Theory]
        [InlineData(MsPortalFxTimeRangeModelFormat.Local, "local", "Local Time")]
        [InlineData(MsPortalFxTimeRangeModelFormat.Utc, "utc", "UTC Time")]
        public void CanCreateADashboardWithTimeRangeFilterAndSetFormat(MsPortalFxTimeRangeModelFormat format, string expectedString, string expectedDisplayCache)
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
                .WithBuilder(new TimeRangeFilterBuilder()
                    .WithFormat(format))
                .Build();

            // Assert
            Assert.Equal(
                "{\"properties\":{\"lenses\":null,\"metadata\":{\"model\":{\"filters\":{\"value\":{\"MsPortalFx_TimeRange\":{\"model\":{\"format\":\"" + expectedString + "\",\"granularity\":\"auto\",\"relative\":\"24h\"},\"displayCache\":{\"name\":\"" + expectedDisplayCache + "\",\"value\":\"Past 24 hours\"},\"filteredPartIds\":[]}}},\"timeRange\":{\"value\":{\"relative\":{\"duration\":24,\"timeUnit\":1}},\"type\":\"MsPortalFx.Composition.Configuration.ValueTypes.TimeRange\"},\"filterLocale\":{\"value\":\"en-us\"}}}},\"name\":\"ce043e69-f9f3-4618-bbe2-2a93e769b56d\",\"type\":\"Microsoft.Portal/dashboards\",\"location\":null,\"tags\":{\"hidden-title\":\"ce043e69-f9f3-4618-bbe2-2a93e769b56d\"}}",
                Dashboard.Generator.Generate(dashboard));
        }
        
        [Theory]
        [InlineData(MsPortalFxTimeRangeModelRelative.Days3, "3d", "Past 3 days")]
        [InlineData(MsPortalFxTimeRangeModelRelative.Days7, "7d", "Past 7 days")]
        [InlineData(MsPortalFxTimeRangeModelRelative.Days30, "30d", "Past 30 days")]
        [InlineData(MsPortalFxTimeRangeModelRelative.Hour1, "1h", "Past hour")]
        [InlineData(MsPortalFxTimeRangeModelRelative.Hours4, "4h", "Past 4 hours")]
        [InlineData(MsPortalFxTimeRangeModelRelative.Hours12, "12h", "Past 12 hours")]
        [InlineData(MsPortalFxTimeRangeModelRelative.Hours24, "24h","Past 24 hours")]
        [InlineData(MsPortalFxTimeRangeModelRelative.Hours48, "48h", "Past 48 hours")]
        [InlineData(MsPortalFxTimeRangeModelRelative.Minutes30, "30m","Past 30 minutes")]
        public void CanCreateADashboardWithTimeRangeFilterAndSetRelativeTime(MsPortalFxTimeRangeModelRelative relative, string expectedString, string expectedDisplayCache)
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
                .WithBuilder(new TimeRangeFilterBuilder()
                    .WithRelative(relative))
                .Build();

            // Assert
            Assert.Equal(
                "{\"properties\":{\"lenses\":null,\"metadata\":{\"model\":{\"filters\":{\"value\":{\"MsPortalFx_TimeRange\":{\"model\":{\"format\":\"utc\",\"granularity\":\"auto\",\"relative\":\""+ expectedString + "\"},\"displayCache\":{\"name\":\"UTC Time\",\"value\":\"" + expectedDisplayCache + "\"},\"filteredPartIds\":[]}}},\"timeRange\":{\"value\":{\"relative\":{\"duration\":24,\"timeUnit\":1}},\"type\":\"MsPortalFx.Composition.Configuration.ValueTypes.TimeRange\"},\"filterLocale\":{\"value\":\"en-us\"}}}},\"name\":\"ce043e69-f9f3-4618-bbe2-2a93e769b56d\",\"type\":\"Microsoft.Portal/dashboards\",\"location\":null,\"tags\":{\"hidden-title\":\"ce043e69-f9f3-4618-bbe2-2a93e769b56d\"}}",
                Dashboard.Generator.Generate(dashboard));
        }
        
        [Fact]
        public void CanCreateADashboardWithTimeRangeFilterAndSetAbsoluteTime()
        {
            //arrange
            var subscriptionId = "6b062fa3-dbe5-4e4a-8d54-ce7286ec9ef6";
            var resourceGroup = Guid.NewGuid().ToString();
            var name = "ce043e69-f9f3-4618-bbe2-2a93e769b56d";

            var now = DateTime.UtcNow;
            var then = now.Subtract(TimeSpan.FromHours(1));
            
            // Act
            var dashboard = new DashboardBuilder()
                .WithSubscription(subscriptionId)
                .WithResourceGroup(resourceGroup)
                .WithName(name)
                .WithBuilder(new TimeRangeFilterBuilder()
                    .WithAbsolute(then, now))
                .Build();

            // Assert
            Assert.Equal(
                "{\"properties\":{\"lenses\":null,\"metadata\":{\"model\":{\"filters\":{\"value\":{\"MsPortalFx_TimeRange\":{\"model\":{\"format\":\"utc\",\"granularity\":\"auto\",\"absolute\":{\"fromDate\":\"" + then.ToString("yyyy-MM-ddTHH:mm:ss.ffffZ") + "\",\"toDate\":\"" + now.ToString("yyyy-MM-ddTHH:mm:ss.ffffZ") + "\"}},\"displayCache\":{\"name\":\"UTC Time\",\"value\":null},\"filteredPartIds\":[]}}},\"timeRange\":{\"value\":{\"relative\":{\"duration\":24,\"timeUnit\":1}},\"type\":\"MsPortalFx.Composition.Configuration.ValueTypes.TimeRange\"},\"filterLocale\":{\"value\":\"en-us\"}}}},\"name\":\"ce043e69-f9f3-4618-bbe2-2a93e769b56d\",\"type\":\"Microsoft.Portal/dashboards\",\"location\":null,\"tags\":{\"hidden-title\":\"ce043e69-f9f3-4618-bbe2-2a93e769b56d\"}}",
                Dashboard.Generator.Generate(dashboard));
        }
    }
}