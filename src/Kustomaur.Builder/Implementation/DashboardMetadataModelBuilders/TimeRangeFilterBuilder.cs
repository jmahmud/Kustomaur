using System;
using System.Collections;
using System.Collections.Generic;
using Kustomaur.Models;
using Kustomaur.Models.Filters;

namespace Kustomaur.Dashboard.Implementation.DashboardMetadataModelBuilders
{
    public class TimeRangeFilterBuilder : IDashboardMetadataModelBuilder
    {
        private MsPortalFxTimeRange _timeRangeFiler = new MsPortalFxTimeRange();

        public void Build(Models.Dashboard dashboard)
        {
            if (!dashboard.Properties.Metadata.Model.ContainsKey("filters"))
            {
                dashboard.Properties.Metadata.Model.Add("filters", new DashboardPropertiesMetadataModel() { Value = new Dictionary<string, object>()});
            }
            DashboardPropertiesMetadataModel filtersModel = dashboard.Properties.Metadata.Model["filters"];
            ((Dictionary<string, object>)filtersModel.Value).Add("MsPortalFx_TimeRange", _timeRangeFiler);
        }

        public TimeRangeFilterBuilder WithFormat(MsPortalFxTimeRangeModelFormat format)
        {
            _timeRangeFiler.Model.Format = format;
            return this;
        }
        
        public TimeRangeFilterBuilder WithGranularity()
        {
            throw new NotImplementedException();
            return this;
        }


        
        
    }
}