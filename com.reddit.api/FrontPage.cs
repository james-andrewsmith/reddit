using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace com.reddit.api
{
    public sealed class FrontPage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static PostListing List(Session session)
        {
            // GET
            // http://www.reddit.com/.json
            var request = new Request
            {
                Url = "http://www.reddit.com/.json",
                Method = "GET",
                Cookie = session.Cookie
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new Exception(json);

            var o = JObject.Parse(json);

            // convert to a post listing
            var list = Post.FromJsonList(o["data"]["children"]);
            list.ModHash = o["data"]["modhash"].ToString(); 
            list.Before = o["data"]["before"].ToString();
            list.After = o["data"]["after"].ToString();
            return list;
        }

    }
}
