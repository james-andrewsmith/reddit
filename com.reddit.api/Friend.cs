using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace com.reddit.api
{
    public sealed class Friend
    {
        // List submissions from friends
        /// http://www.reddit.com/r/friends/.json
        /// 

        public static UserListing List(Session session)
        {
            var request = new Request
            {
                Url = "https://ssl.reddit.com/prefs/friends.json",
                Method = "GET",
                Cookie = session.Cookie
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new Exception(json);
            
            var o = JObject.Parse(json);
            var list = UserListing.FromJson(o);
            return list;
        }

        public static void Add()
        {

        }

        public static void Remove()
        {

        }

        public static PostListing GetPosts(Session session)
        {            
            var request = new Request
            {
                Url = "http://www.reddit.com/r/friends/.json",
                Method = "GET",
                Cookie = session.Cookie
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);

            var o = JObject.Parse(json);
            return PostListing.FromJson(o);            
        }

        public static CommentListing GetComments(Session session)
        {
            var request = new Request
            {
                Url = "http://www.reddit.com/r/friends/comments/.json",
                Method = "GET",
                Cookie = session.Cookie
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);

            var o = JObject.Parse(json);
            return CommentListing.FromJson(o);         
        }
    }
}
