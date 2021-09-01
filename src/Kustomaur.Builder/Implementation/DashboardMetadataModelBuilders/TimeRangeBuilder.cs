using Kustomaur.Models;

namespace Kustomaur.Dashboard.Implementation.DashboardMetadataModelBuilders
{
    public class TimeRangeBuilder : IDashboardMetadataModelBuilder
    {
        private int _duration;
        private int _timeUnit;

        public TimeRangeBuilder()
        {
            // defaults
            _duration = 24;
            _timeUnit = 1;
        }
        public void Build(Models.Dashboard dashboard)
        {
            var timeRange = new TimeRange();
            timeRange.Relative.Duration = _duration;
            timeRange.Relative.TimeUnit = _timeUnit;
            dashboard.Properties.Metadata.Model.Add("timeRange", new DashboardPropertiesMetadataModel() { Value = timeRange });
        }

        public TimeRangeBuilder WithDuration(int duration)
        {
            _duration = duration;
            return this;
        }
        
        public TimeRangeBuilder WithTimeUnit(int timeUnit)
        {
            _timeUnit = timeUnit;
            return this;
        }
    }

    
}