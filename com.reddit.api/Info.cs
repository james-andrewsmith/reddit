using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace com.reddit.api
{
    public sealed class Info
    {
        #region // Data Access //

        /// <summary>
        /// Get information about a URL or domain, find all the stories and comments 
        /// associated within.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static PostListing FromUrl(Session session, string url)
        {
            // build the request
            var request = new Request
            {
                Url = "http://www.reddit.com/api/info.json?url=" + url,
                Method = "GET",
                Cookie = session.Cookie
            };

            // execute the request
            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new Exception(json);

            // convert to a post listing
            var o = JObject.Parse(json);
            return PostListing.FromJson(o);
        }

        #endregion 

    }
}
