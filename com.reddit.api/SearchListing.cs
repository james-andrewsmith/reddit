using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace com.reddit.api
{
    /// <summary>
    /// This class exists to future proof us against a reddit which
    /// indexes more than posts, but messages, users, and comments...
    /// </summary>
    public sealed class SearchListing : List<Search>
    {

        internal static SearchListing FromJson(JToken token)
        {
            var list = new SearchListing();

            return list;
        }

    }
}
