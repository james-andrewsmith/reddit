using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.reddit.api
{
    internal static class Extension
    {

        private static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        internal static int ToEpoch(this DateTime datetime)
        {
            return (int)(datetime - _epoch).TotalSeconds;
        }

        internal static DateTime ToDateTime(this int epoch)
        {
            return _epoch.AddSeconds(epoch);
        }

    }
}
