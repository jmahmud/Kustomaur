using System;
using System.Collections.Generic;
using Kustomaur.Dashboard.Implementation.DashboardMetadataModelBuilders;
using Kustomaur.Models;

namespace Kustomaur.Dashboard
{
    /// <summary>
    /// This is the main class that generates the object model for a dashboard.  Call Build() to produce the Dashboard object.
    /// </summary>
    public class DashboardBuilder
    {
        /// <summary>
        /// Set to the Azure Subscription Id that the dashboard would be created in
        /// </summary>
        public string SubscriptionId { get; private set; }
        
        /// <summary>
        /// Set to the Azure Resource Group name that the dashboard would be created in
        /// </summary>
        public string ResourceGroup { get; private set; }
        
        /// <summary>
        /// The name of the dashboard used in the Azure Portal
        /// </summary>
        public string Name { get; private set; }

        private Models.Dashboard _dashboard;

        private IDashboardMetadataModelBuilder _timeRangeBuilder;

        private IDashboardMetadataModelBuilder _timeRangeFilterBuilder;

        private readonly List<IBaseBuilder> _builders;
        
        private string _filterLocale = "en-us";
        
        public DashboardBuilder()
        {
            _timeRangeBuilder = new TimeRangeBuilder();
            _timeRangeFilterBuilder = new TimeRangeFilterBuilder();
            _builders = new List<IBaseBuilder>();
            _dashboard = new Models.Dashboard();
            InitialiseDashboardPropertiesMetadataModel();

        }
        /// <summary>
        /// Sets the Azure Subscription Id that the dashboard would be created in
        /// </summary>
        /// <param name="subscriptionId">Should be a GUID</param>
        /// <returns></returns>
        public DashboardBuilder WithSubscription(string subscriptionId)
        {
            SubscriptionId = subscriptionId;
            return this;
        }

        /// <summary>
        /// Sets the Azure Resource Group that the dashboard would be created in
        /// </summary>
        /// <param name="resourceGroup">Should be the name of the resource group</param>
        /// <returns></returns>
        public DashboardBuilder WithResourceGroup(string resourceGroup)
        {
            ResourceGroup = resourceGroup;
            return this;
        }
        
        /// <summary>
        /// To set the name of the dashboard
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DashboardBuilder WithName(string name)
        {
            Name = name;
            if (_dashboard.Tags == null)
            {
                _dashboard.Tags = new Dictionary<string, string>();
            }
            _dashboard.Tags.Add("hidden-title", name);
            return this;
        }

        /// <summary>
        /// To set the builder for TimeRange 
        /// </summary>
        /// <param name="builder">See <see cref="TimeRangeBuilder"/></param>
        /// <returns></returns>
        public DashboardBuilder WithTimeRangeBuilder(TimeRangeBuilder builder)
        {
            _timeRangeBuilder = builder;
            return this;
        }
        
        /// <summary>
        /// To set any builder to further manipulate the dashboard.  The DashboardBuilder will call all of the builders that are set sequentially
        /// </summary>
        /// <param name="builder">See <see cref="IBaseBuilder"/></param>
        /// <returns></returns>
        public DashboardBuilder WithBuilder(IBaseBuilder builder)
        {
            _builders.Add(builder);
            return this;
        }
        
        /// <summary>
        /// To set the locale.  Default is 'en-us'
        /// </summary>
        /// <param name="locale"></param>
        /// <returns></returns>
        public DashboardBuilder WithFilterLocale(string locale)
        {
            _filterLocale = locale;
            return this;
        }
        
        private void InitialiseDashboardPropertiesMetadataModel()
        {
            if (_dashboard.Properties == null)
            {
                _dashboard.Properties = new DashboardProperties();
            }

            if (_dashboard.Properties.Metadata == null)
            {
                _dashboard.Properties.Metadata = new PropertiesMetadata();
            }

            if (_dashboard.Properties.Metadata.Model == null)
            {
                _dashboard.Properties.Metadata.Model = new Dictionary<string, DashboardPropertiesMetadataModel>();
            }
        }

        /// <summary>
        /// Combines and runs all builders to generate a Models.Dashboard which is of type: Microsoft.Portal/dashboards
        /// </summary>
        /// <returns></returns>
        public Models.Dashboard Build()
        {
            _dashboard.Type = "Microsoft.Portal/dashboards";
            _dashboard.Name = Name;

            // Run each builder
            CombineAndRunBuilders();
            
            //Set filter locale
            _dashboard.Properties.Metadata.Model.Add("filterLocale", new DashboardPropertiesMetadataModel() { Value = _filterLocale });
            
            // Set Filters

            return _dashboard;
        }

        private void CombineAndRunBuilders()
        {
            var builders = new List<IBaseBuilder>();
            builders.AddRange(_builders);
            builders.Add(_timeRangeBuilder);
            builders.Add(_timeRangeFilterBuilder);
            builders.ForEach(b =>
            {
                b.Build(_dashboard);
            });
        }

        private string BuildId()
        {
            return
                $"/subscriptions/{SubscriptionId}/resourceGroups/dashboards/providers/Microsoft.Portal/dashboards/{Name}";
        }
    }
}