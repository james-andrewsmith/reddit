using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace com.reddit.api
{
    public sealed class Search
    {

        #region // Properties //

        #endregion 

        #region // Actions //

        public static PostListing Query(Session session, string qry)
        {
            return Query(session, qry, string.Empty);
        }

        public static PostListing Query(Session session, string qry, string after)
        {
            return Query(session, qry, after, string.Empty);
        }

        public static PostListing Query(Session session, string qry, string after, string before)
        {
            var request = new Request
            {
                Url = "http://www.reddit.com/search.json?q=" + qry,
                Method = "GET",
                Cookie = session.Cookie
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);

            var o = JObject.Parse(json);
            return PostListing.FromJson(o);
        }

        #endregion

    }
}
