using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Core.Common.Extentions
{
    public static class DateTimeExtentions
    {
        public static double ToJavaScriptDate(this DateTime datetime)
        {
            return datetime
                .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                .TotalMilliseconds;
        }
    }
}
