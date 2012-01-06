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

        public static void Add(Session session, string username, string id, string modhash)
        {
            id = id.StartsWith("t2_") ? id : "t2_" + id;

            var request = new Request
            {
                Url = "http://www.reddit.com/api/friend?note=",
                Method = "POST",
                Cookie = session.Cookie,
                Content = "name=" + username +
                          "&container=" + id +
                          "&type=friend" +
                          "&uh=" + modhash +
                          "&renderstyle=html"
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);

            var o = JObject.Parse(json);
            // o["jquery"][20][3][0].ToString()
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="username"></param>
        /// <param name="id">The ID of the current user (with the prepended t2_)</param>
        /// <param name="modhash"></param>
        public static void Remove(Session session, string username, string id, string modhash)
        {
            id = id.StartsWith("t2_") ? id : "t2_" + id;

            // http://www.reddit.com/api/unfriend
            var request = new Request
            {
                Url = "http://www.reddit.com/api/unfriend",
                Method = "POST",
                Cookie = session.Cookie,
                Content = "name=" + username + 
                          "&container=" + id +
                          "&type=friend" + 
                          "&uh=" + modhash + 
                          "&renderstyle=html"
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);

            var o = JObject.Parse(json);
            
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
