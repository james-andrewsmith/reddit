using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace com.reddit.api
{
    public sealed class UserListing : List<User>
    {

        public static UserListing FromJson(JToken token)
        {
            var list = new UserListing();

            foreach (var t in token["data"]["children"].Children())
                list.Add(User.FromJson(t));

            return list;
        }

    }
}
