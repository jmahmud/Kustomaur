using System;
using System.Collections;
using System.Collections.Generic;
using Kustomaur.Models;
using Kustomaur.Models.Filters;

namespace Kustomaur.Dashboard.Implementation.DashboardMetadataModelBuilders
{
    /// <summary>
    /// Adds a MsPortalFx_TimeRange type to the dashboard
    /// </summary>
    public class TimeRangeFilterBuilder : IDashboardMetadataModelBuilder
    {
        private MsPortalFxTimeRange _timeRangeFiler = new MsPortalFxTimeRange();
        private const string MS_PORTAL_FX_TIMERANGE_NAME = "MsPortalFx_TimeRange";
        public void Build(Models.Dashboard dashboard)
        {
            if (!dashboard.Properties.Metadata.Model.ContainsKey("filters"))
            {
                dashboard.Properties.Metadata.Model.Add("filters", new DashboardPropertiesMetadataModel() { Value = new Dictionary<string, object>()});
            }
            
            DashboardPropertiesMetadataModel filtersModel = dashboard.Properties.Metadata.Model["filters"];

            if (!filtersModel.ValueAs<Dictionary<string, object>>().ContainsKey(MS_PORTAL_FX_TIMERANGE_NAME))
            {
                filtersModel.ValueAs<Dictionary<string, object>>().Add(MS_PORTAL_FX_TIMERANGE_NAME, _timeRangeFiler);
            }
        }

        public void WithSubscription(string subscriptionId) {}
        
        public void WithResourceGroup(string resourceGroup) {}

        public TimeRangeFilterBuilder WithFormat(MsPortalFxTimeRangeModelFormat format)
        {
            _timeRangeFiler.Model.Format = format;
            return this;
        }
        
        public TimeRangeFilterBuilder WithGranularity(MsPortalFxTimeRangeModelGranularity granularity)
        {
            _timeRangeFiler.Model.Granularity = granularity;
            return this;
        }

        public TimeRangeFilterBuilder WithRelative(MsPortalFxTimeRangeModelRelative relative)
        {
            _timeRangeFiler.Model.Relative = relative;
            _timeRangeFiler.Model.Absolute = null;
            return this;
        }
        
        public TimeRangeFilterBuilder WithAbsolute(DateTime from, DateTime to)
        {
            _timeRangeFiler.Model.Relative = null;
            _timeRangeFiler.Model.Absolute = new MsPortalFxTimeRangeModelAbsolute(from, to);
            return this;
        }
    }
}