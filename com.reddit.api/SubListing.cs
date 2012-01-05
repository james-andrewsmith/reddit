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
                list.Add(new Sub
                {
                    DisplayName = sub["display_name"].ToString(),
                    Name = sub["name"].ToString(),
                    Title = sub["title"].ToString(),
                    Url = sub["url"].ToString(),
                    Created = Convert.ToInt32(sub["created"].ToString()).ToDateTime(),
                    CreatedUtc = Convert.ToInt32(sub["created_utc"].ToString()).ToDateTime(),
                    Over18 = Convert.ToBoolean(sub["over18"].ToString()),
                    Subscribers = Convert.ToInt32(sub["subscribers"].ToString()),
                    ID = sub["id"].ToString(),
                    Description = sub["description"].ToString()
                });
            }

            return list;
        }
    }
}
