using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Kustomaur.Models.Filters
{
    public class MsPortalFxTimeRange
    {
        public MsPortalFxTimeRangeModel Model { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public bool DisplayCacheEnabled { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public MsPortalFxTimeRangeDisplayCache DisplayCache => DisplayCacheEnabled ? CalculateDisplayCache() : null;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string> FilteredPartIds { get; set; }

        public MsPortalFxTimeRange()
        {
            DisplayCacheEnabled = true;
            FilteredPartIds = new List<string>();
            Model = new MsPortalFxTimeRangeModel();
        }
        
        
        private MsPortalFxTimeRangeDisplayCache CalculateDisplayCache()
        {
            var displayCache = new MsPortalFxTimeRangeDisplayCache();

            displayCache.Name = CalculateDisplayCacheName(Model);
            displayCache.Value = CalculateDisplayCacheValue(Model);
            
            return displayCache;
        }

        private string CalculateDisplayCacheValue(MsPortalFxTimeRangeModel model)
        {
            switch (model.Relative)
            {
                case MsPortalFxTimeRangeModelRelative.Minutes30:
                    return MsPortalFxTimeRangeDisplayCacheValue.Minutes30;
                case MsPortalFxTimeRangeModelRelative.Days3:
                    return MsPortalFxTimeRangeDisplayCacheValue.Days3;
                case MsPortalFxTimeRangeModelRelative.Days7:
                    return MsPortalFxTimeRangeDisplayCacheValue.Days7;
                case MsPortalFxTimeRangeModelRelative.Days30:
                    return MsPortalFxTimeRangeDisplayCacheValue.Days30;
                case MsPortalFxTimeRangeModelRelative.Hour1:
                    return MsPortalFxTimeRangeDisplayCacheValue.Hour1;
                case MsPortalFxTimeRangeModelRelative.Hours4:
                    return MsPortalFxTimeRangeDisplayCacheValue.Hours4;
                case MsPortalFxTimeRangeModelRelative.Hours12:
                    return MsPortalFxTimeRangeDisplayCacheValue.Hours12;
                case MsPortalFxTimeRangeModelRelative.Hours24:
                    return MsPortalFxTimeRangeDisplayCacheValue.Hours24;
                case MsPortalFxTimeRangeModelRelative.Hours48:
                    return MsPortalFxTimeRangeDisplayCacheValue.Hours48;
            }

            return null;
        }

        private string CalculateDisplayCacheName(MsPortalFxTimeRangeModel model)
        {
            switch (model.Format)
            {
                case MsPortalFxTimeRangeModelFormat.Utc:
                    return MsPortalFxTimeRangeDisplayCacheName.UtcTime;
                default:
                    return MsPortalFxTimeRangeDisplayCacheName.LocalTime;
            }
        }
    }

    public class MsPortalFxTimeRangeDisplayCache
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public static class MsPortalFxTimeRangeDisplayCacheName
    {
        public const string UtcTime = "UTC Time";
        public const string LocalTime = "Local Time";
    }
    
    public static class MsPortalFxTimeRangeDisplayCacheValue
    {
        public const string Minutes30 = "Past 30 minutes";
        public const string Hour1 = "Past hour";
        public const string Hours4 = "Past 4 hours";
        public const string Hours12 = "Past 12 hours";
        public const string Hours24 = "Past 24 hours";
        public const string Hours48 = "Past 48 hours";
        public const string Days3 = "Past 3 days";
        public const string Days7 = "Past 7 days";
        public const string Days30 = "Past 30 days";
    }

    public class MsPortalFxTimeRangeModel
    {
        public MsPortalFxTimeRangeModelFormat Format { get; set; } 
        public MsPortalFxTimeRangeModelGranularity Granularity { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public MsPortalFxTimeRangeModelRelative? Relative { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public MsPortalFxTimeRangeModelAbsolute Absolute { get; set; }

        public MsPortalFxTimeRangeModel()
        {
            Granularity = MsPortalFxTimeRangeModelGranularity.Auto;
            Relative = MsPortalFxTimeRangeModelRelative.Hours24;
            Absolute = null;
        }
    }

    public enum MsPortalFxTimeRangeModelFormat
    {
        [EnumMember(Value = "utc")]
        Utc,
        
        [EnumMember(Value = "local")]
        Local
    }
    
    public enum MsPortalFxTimeRangeModelRelative
    {
        [EnumMember(Value = "30m")] Minutes30,
        [EnumMember(Value = "1h")] Hour1,
        [EnumMember(Value = "4h")] Hours4,
        [EnumMember(Value = "12h")] Hours12,
        [EnumMember(Value = "24h")] Hours24,
        [EnumMember(Value = "48h")] Hours48,
        [EnumMember(Value = "3d")] Days3,
        [EnumMember(Value = "7d")] Days7,
        [EnumMember(Value = "30d")] Days30
    }

    public enum MsPortalFxTimeRangeModelGranularity
    {
        [EnumMember(Value = "auto")] Auto,
        [EnumMember(Value = "1m")] Minute1,
        [EnumMember(Value = "5m")] Minute5,
        [EnumMember(Value = "15m")] Minute15,
        [EnumMember(Value = "30m")] Minute30,
        [EnumMember(Value = "1h")] Hour1,
        [EnumMember(Value = "6h")] Hour6,
        [EnumMember(Value = "12h")] Hour12,
        [EnumMember(Value = "1w")] Week1,
        [EnumMember(Value = "1mo")] Month1
    }

    public class  MsPortalFxTimeRangeModelAbsolute
    {
        public MsPortalFxTimeRangeModelAbsolute(DateTime from, DateTime to)
        {
            FromDate = from;
            ToDate = to;
        }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}