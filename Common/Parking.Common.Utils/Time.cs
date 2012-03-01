using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sieena.Parking.Common.Utils
{
    /// <summary>
    /// Basic Time functions
    /// </summary>
    public static class Time
    {
        /// <summary>
        /// Converts the time to a predefined timezone
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime ToCommonTime(this DateTime time)
        {
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
            return TimeZoneInfo.ConvertTimeFromUtc(time.ToUniversalTime(), timeZoneInfo); 
        }

        /// <summary>
        /// Converts the time to a predefined timezone
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime? ToCommonTime(this DateTime? time)
        {
            if (time.HasValue)
            {
                return time.Value.ToCommonTime();
            }
            return null;
        }
    }
}
