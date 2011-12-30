using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace com.reddit.api
{
    public sealed class Flair
    {
        #region // Properties //
        public string User
        {
            get;
            set;
        }

        public string CssClass
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }
        #endregion

        #region // Data Access //

        public static FlairListing GetFlair(Session session, string sub)
        {
            return GetFlair(session, sub, string.Empty);
        }

        public static FlairListing GetFlair(Session session, string sub, string after)
        {
            return GetFlair(session, sub, string.Empty, string.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="sub"></param>
        /// <see cref="https://github.com/reddit/reddit/wiki/API%3A-flairlist"/>
        /// <returns></returns>
        public static FlairListing GetFlair(Session session, string sub, string after, string before)
        {            
            // var url = "http://www.reddit.com/api/flairlist?r=" + sub + "&limit=1000&uh=" + session.ModHash;
            var url = "http://www.reddit.com/r/" + sub + "/api/flairlist.json?uh=" + session.ModHash + "&limit=1000";
            if (!string.IsNullOrEmpty(after))
                url += "&after=" + after;

            if (!string.IsNullOrEmpty(before))
                url += "&before=" + before;

            var request = new Request
            {
                Url = url,
                Method = "GET",
                Cookie = session.Cookie
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);

            var o = JObject.Parse(json);

            // convert to a post listing
            var list = FlairListing.FromJson(o["users"]);
            list.Next = o["next"] == null ? string.Empty : o["next"].ToString();
            list.Prev = o["prev"] == null ? string.Empty : o["prev"].ToString();
            return list;

        }

        #endregion

        #region // Conversion //

        public static Flair FromJson(JToken token)
        {
            return new Flair
            {
                User = token["user"].ToString(),
                Text = token["flair_text"].ToString(),
                CssClass = token["flair_css_class"].ToString()
            };
        }

        #endregion
    }
}
