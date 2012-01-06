using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace com.reddit.api
{
    public sealed class CommentListing : List<Comment>
    {

        #region // Constructor //
        public CommentListing()
        {
            More = new List<string>();
        }
        #endregion

        #region // Properties //

        /// <summary>
        /// 
        /// </summary>
        public List<string> More
        {
            get;
            set;
        }

        public bool HasMore
        {
            get
            {
                return More.Count > 0;
            }
        }

        public string ModHash
        {
            get;
            set;
        }
        #endregion

        internal static CommentListing FromJson(JToken token)
        {
            var list = new CommentListing();
            list.ModHash = token["data"]["modhash"].ToString();

            var comments = token["data"]["children"]; //.Select(t => t["data"]);
            foreach (var comment in comments)
            {
                switch (comment["kind"].ToString())
                {
                    case "t1":
                        list.Add(new Comment(comment["data"]));
                        break;
                    case "more":
                        var test = comment["data"]["children"].Values().Select(t => t.ToString()).ToList();
                        list.More.AddRange(test);
                        break;
                }
            }

            return list;
        }
    }
}
