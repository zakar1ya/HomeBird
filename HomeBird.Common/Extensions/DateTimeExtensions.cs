using System;

namespace HomeBird.Common.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// format dd.MM.yyyy hh:mm
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToFriendlyString(this DateTimeOffset val)
        {
            return val.ToString("dd.MM.yyyy HH:mm");
        }

        /// <summary>
        /// format dd.MM.yyyy hh:mm
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToFriendlyString(this DateTimeOffset? val)
        {
            return val.HasValue ? val.Value.ToFriendlyString() : string.Empty;
        }

        /// <summary>
        /// ISO 8601
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToIsoString(this DateTime val)
        {
            return val.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
        }

        /// <summary>
        /// ISO 8601
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToIsoString(this DateTimeOffset val)
        {
            return val.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
        }
    }
}
