using System;
using Kustomaur.Dashboard;
using Kustomaur.Dashboard.DashboardParts;
using Kustomaur.Dashboard.Implementation;
using Xunit;

namespace Kustomaur.Builder.Tests
{
    public class DashboardBuilderTests
    {
        [Fact]
        public void CanCreateADashboard()
        {
            //arrange
            var subscriptionId = Guid.NewGuid().ToString();
            var resourceGroup = Guid.NewGuid().ToString();
            var name = Guid.NewGuid().ToString();

            var dashboard = new DashboardBuilder()
                .WithSubscription(subscriptionId)
                .WithResourceGroup(resourceGroup)
                .WithName(name)
                .Build();
                 
        }
        
        [Fact]
        public void CanCreateADashboardWithPartsBuilder()
        {
            //arrange
            var subscriptionId = Guid.NewGuid().ToString();
            var resourceGroup = Guid.NewGuid().ToString();
            var name = Guid.NewGuid().ToString();


            var dashboard = new DashboardBuilder()
                .WithSubscription(subscriptionId)
                .WithResourceGroup(resourceGroup)
                .WithName(name)
                .Build();
            
            var chartsBuilder = new DashboardPartsBuilder(dashboard);
            chartsBuilder.AddPart(new MarkdownPart(title:"My first thing").GeneratePart());

                 
        }
    }
}