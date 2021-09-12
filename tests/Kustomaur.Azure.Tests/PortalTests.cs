using System;
using System.Security.Authentication.ExtendedProtection;
using System.Threading.Tasks;
using Kustomaur.Azure.IoC;
using Kustomaur.Dashboard;
using Kustomaur.Dashboard.DashboardParts;
using Kustomaur.Dashboard.DashboardParts.Implementations;
using Kustomaur.Dashboard.Implementation;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Kustomaur.Azure.Tests
{
    public class PortalTests
    {
        private ServiceCollection _serviceCollection;
        private ServiceProvider _serviceProvider;
        private Portal _sut;
        public PortalTests()
        {
            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddKustomaurAzureServices();
            _serviceProvider = _serviceCollection.BuildServiceProvider();
            _sut = _serviceProvider.GetService<Portal>();
        }
        
        [Fact(Skip = "Just an example")]
        public async Task Test1()
        {
            //arrange
            var subscriptionId = "b42aaad0-2122-4826-aa7c-b49250f0c3f9";
            var resourceGroup = "TestDashboards";
            var dashboardName = "JoshDashboardFromCSharp";
            
            // Act
            var dashboard = new DashboardBuilder()
                .WithSubscription(subscriptionId)
                .WithResourceGroup(resourceGroup)
                .WithName(dashboardName)
                .WithBuilder(new DashboardPartsBuilder()
                    .AddPart(new MarkdownPart(content:"# My first thing")
                        .WithColSpan(5)
                        .WithRowSpan(5)))
                .Build();
                
            //TODO: set up authorisation
            await _sut.CreateOrUpdateDashboard(dashboard, subscriptionId, resourceGroup, dashboardName);
            
        }
    }
}