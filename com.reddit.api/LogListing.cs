using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace com.reddit.api
{
    public sealed class LogListing : List<Log>
    {

        internal static LogListing FromJson(JToken token)
        {
            return new LogListing
            {

            };
        }

    }
}
