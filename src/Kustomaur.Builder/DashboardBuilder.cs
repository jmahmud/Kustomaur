using System;
using System.Collections.Generic;
using Kustomaur.Dashboard.Implementation.DashboardMetadataModelBuilders;
using Kustomaur.Models;

namespace Kustomaur.Dashboard
{
    public class DashboardBuilder
    {
        public string SubscriptionId { get; private set; }
        public string ResourceGroup { get; private set; }
        public string Name { get; private set; }

        public Models.Dashboard Dashboard { get; private set; }


        private IDashboardMetadataModelBuilder _timeRangeBuilder;

        private List<IBaseBuilder> _builders;
        public DashboardBuilder()
        {
            _timeRangeBuilder = new TimeRangeBuilder();
            _builders = new List<IBaseBuilder>();
            Dashboard = new Models.Dashboard();
            InitialiseDashboardPropertiesMetadataModel();

        }
        public DashboardBuilder WithSubscription(string subscriptionId)
        {
            SubscriptionId = subscriptionId;
            return this;
        }
        
        public DashboardBuilder WithResourceGroup(string resourceGroup)
        {
            ResourceGroup = resourceGroup;
            return this;
        }
        
        public DashboardBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public DashboardBuilder WithTimeRangeBuilder(TimeRangeBuilder builder)
        {
            _timeRangeBuilder = builder;
            return this;
        }

        // public DashboardBuilder WithPartsBuilder(IDashboardPartBuilder builder)
        // {
        //     _builders.Add(builder);
        //     return this;
        // }
        
        public DashboardBuilder WithBuilder(IBaseBuilder builder)
        {
            _builders.Add(builder);
            return this;
        }
        
        public DashboardBuilder WithFilterLocale(string locale)
        {
            Dashboard.Properties.Metadata.Model.Add("filterLocale", new DashboardPropertiesMetadataModel() { Value = locale });
            return this;
        }
        
        // public DashboardBuilder WithTimeRange(TimeRange timeRange)
        // {
        //     Dashboard.Properties.Metadata.Model.Add(nameof(TimeRange), timeRange);
        //     return this;
        // }

        private void InitialiseDashboardPropertiesMetadataModel()
        {
            if (Dashboard.Properties == null)
            {
                Dashboard.Properties = new DashboardProperties();
            }

            if (Dashboard.Properties.Metadata == null)
            {
                Dashboard.Properties.Metadata = new PropertiesMetadata();
            }

            if (Dashboard.Properties.Metadata.Model == null)
            {
                Dashboard.Properties.Metadata.Model = new Dictionary<string, DashboardPropertiesMetadataModel>();
            }
        }

        public Models.Dashboard Build()
        {
            Dashboard.Id = BuildId();
            Dashboard.Type = "Microsoft.Portal/dashboards";
            Dashboard.Name = Name;
            
            // Run each builder
            CombineAndRunBuilders();

            return Dashboard;
        }

        private void CombineAndRunBuilders()
        {
            var builders = new List<IBaseBuilder>();
            builders.AddRange(_builders);
            builders.Add(_timeRangeBuilder);
            builders.ForEach(b => b.Build(Dashboard));
        }

        private string BuildId()
        {
            return
                $"/subscriptions/{SubscriptionId}/resourceGroups/dashboards/providers/Microsoft.Portal/dashboards/{Name}";
        }
    }
}