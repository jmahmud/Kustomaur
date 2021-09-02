using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Kustomaur.Models.Filters
{
    public class MsPortalFxTimeRange
    {
        public MsPortalFxTimeRangeModel Model { get; set; }
        
        public MsPortalFxTimeRangeDisplayCache DisplayCache => CalculateDisplayCache();
        public List<string> FilteredPartIds { get; set; }

        public MsPortalFxTimeRange()
        {
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
        public string Granularity { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Relative { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public MsPortalFxTimeRangeModelAbsolute Absolute { get; set; }

        public MsPortalFxTimeRangeModel()
        {
            Granularity = MsPortalFxTimeRangeModelGranularity.Auto;
            Relative = MsPortalFxTimeRangeModelRelative.Hours24;
        }
    }

    public enum MsPortalFxTimeRangeModelFormat
    {
        [JsonPropertyName("utc")]
        Utc,
        
        [JsonPropertyName("local")]
        Local
    }

    public static class MsPortalFxTimeRangeModelRelative
    { 
        public const string Minutes30 = "30m";
        public const string Hour1 = "1h";
        public const string Hours4 = "4h";
        public const string Hours12 = "12h";
        public const string Hours24 = "24h";
        public const string Hours48 = "48h";
        public const string Days3 = "3d";
        public const string Days7 = "7d";
        public const string Days30 = "30d";
    }
    
    public static class MsPortalFxTimeRangeModelGranularity
    {
        public static string Auto => "auto";
        public static string Minute1 => "1m";
        public static string Minute5 => "5m";
        public static string Minute15 => "15m";
        public static string Minute30 => "30m";
        public static string Hour1 => "1h";
        public static string Hour6 => "6h";
        public static string Hour12 => "12h";
        public static string Week1 => "1w";
        public static string Month1 => "1m";
    }
    
    public class  MsPortalFxTimeRangeModelAbsolute
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}