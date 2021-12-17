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
    public class TimeRangeFilterBuilder : IDashboardMetadataModelBuilder, IBasePartsBuilder
    {
        private MsPortalFxTimeRange _timeRangeFilter = new MsPortalFxTimeRange();
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
                filtersModel.ValueAs<Dictionary<string, object>>().Add(MS_PORTAL_FX_TIMERANGE_NAME, _timeRangeFilter);
            }
        }

        public void Build(Part part)
        {
            // no display cache in a part
            WithDisplayCache(false);
            
            // no filtered part ids in a part
            _timeRangeFilter.FilteredPartIds = null;
            
            if (part.Metadata == null)
            {
                part.Metadata = new PartMetadata();
            }
            
            if (part.Metadata.Filters == null)
            {
                part.Metadata.Filters = new Dictionary<string, object>();
            }

            if (!part.Metadata.Filters.ContainsKey(MS_PORTAL_FX_TIMERANGE_NAME))
            {
                part.Metadata.Filters.Add(MS_PORTAL_FX_TIMERANGE_NAME, _timeRangeFilter);
            }
        }

        public void WithSubscription(string subscriptionId) {}
        
        public void WithResourceGroup(string resourceGroup) {}

        public TimeRangeFilterBuilder WithFormat(MsPortalFxTimeRangeModelFormat format)
        {
            _timeRangeFilter.Model.Format = format;
            return this;
        }
        
        public TimeRangeFilterBuilder WithGranularity(MsPortalFxTimeRangeModelGranularity granularity)
        {
            _timeRangeFilter.Model.Granularity = granularity;
            return this;
        }

        public TimeRangeFilterBuilder WithRelative(MsPortalFxTimeRangeModelRelative relative)
        {
            _timeRangeFilter.Model.Relative = relative;
            _timeRangeFilter.Model.Absolute = null;
            return this;
        }
        
        public TimeRangeFilterBuilder WithAbsolute(DateTime from, DateTime to)
        {
            _timeRangeFilter.Model.Relative = null;
            _timeRangeFilter.Model.Absolute = new MsPortalFxTimeRangeModelAbsolute(from, to);
            return this;
        }

        public TimeRangeFilterBuilder WithDisplayCache(bool withDisplayCache)
        {
            _timeRangeFilter.DisplayCacheEnabled = withDisplayCache;
            return this;
        }
    }
}