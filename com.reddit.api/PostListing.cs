using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace com.reddit.api
{
    public sealed class PostListing : List<Post>
    {
        #region // Properties //
        public string Before
        {
            get;
            set;
        }

        public string After
        {
            get;
            set;
        }

        public string ModHash
        {
            get;
            set;
        }
        #endregion

        internal static PostListing FromJson(JToken token)
        {
            var list = new PostListing();
            list.ModHash = token["data"]["modhash"].ToString();
            list.Before = token["data"]["before"].ToString();
            list.After = token["data"]["after"].ToString();
            
            foreach (var child in token["data"]["children"].Children().Select(post => post["data"]))
                list.Add(Post.FromJson(child));

            return list;
        }
    }
}
