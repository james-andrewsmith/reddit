using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace com.reddit.api
{
    public sealed class SubListing : List<Sub>
    {

        internal static SubListing FromJson(JToken token)
        {
            var list = new SubListing();

            foreach (var sub in token["data"]["children"].Children()
                                                         .Select(sub => sub["data"]))
            {
                list.Add(Sub.FromJson(sub));
            }

            return list;
        }
    }
}
