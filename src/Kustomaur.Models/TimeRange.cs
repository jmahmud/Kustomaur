using System.Collections.Generic;

namespace Kustomaur.Models
{
    public class TimeRange
    {
        public TimeRangeRelative Relative { get; set; }

        public TimeRange()
        {
            Relative = new TimeRangeRelative();
        }
    }

    public class TimeRangeRelative
    {
        public int Duration { get; set; }
        public int TimeUnit { get; set; }
    }
}