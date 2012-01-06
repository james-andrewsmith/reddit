using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace com.reddit.api
{
    /// <summary>
    /// Represents a subreddit
    /// </summary>
    public sealed class Sub
    {
        #region // Properties //

        public string DisplayName
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Url
        {
            get;
            set;
        }

        public DateTime Created
        {
            get;
            set;
        }

        public DateTime CreatedUtc
        {
            get;
            set;
        }

        public bool Over18
        {
            get;
            set;
        }

        public int Subscribers
        {
            get;
            set;
        }

        public string ID
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        #endregion

        internal static Sub FromJson(JToken token)
        {
            return new Sub
            {
                DisplayName = token["display_name"].ToString(),
                Name = token["name"].ToString(),
                Title = token["title"].ToString(),
                Url = token["url"].ToString(),
                Created = Convert.ToInt32(token["created"].ToString()).ToDateTime(),
                CreatedUtc = Convert.ToInt32(token["created_utc"].ToString()).ToDateTime(),
                Over18 = Convert.ToBoolean(token["over18"].ToString()),
                Subscribers = Convert.ToInt32(token["subscribers"].ToString()),
                ID = token["id"].ToString(),
                Description = token["description"].ToString()
            };
        }

        public static Sub Get(Session session, string sub)
        {
            // 
            var request = new Request
            {
                Url = "http://www.reddit.com/r/" + sub + "/about/.json",
                Method = "GET",
                Cookie = session.Cookie
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);

            var o = JObject.Parse(json);
            return Sub.FromJson(o["data"]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="sub"></param>
        /// <param name="after"></param>
        /// <param name="before"></param>
        /// <returns></returns>
        public static SubListing List(Session session, string after = "", string before = "")
        {

            var request = new Request
            {
                Url = "http://www.reddit.com/reddits/.json",
                Method = "GET",
                Cookie = session.Cookie
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);

            var o = JObject.Parse(json);
            return SubListing.FromJson(o);
        }

        /// <summary>
        /// Get the currently subscribed sub reddit's of the user
        /// </summary>
        /// <returns></returns>
        public static List<Sub> GetMine(Session session)
        {
            var request = new Request
            {
                Url = "http://www.reddit.com/reddits/mine.json",
                Method = "GET",                
                Cookie = session.Cookie
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);
            
            var o = JObject.Parse(json);
            return SubListing.FromJson(o);
        }

        public static SubListing GetMineModerated(Session session)
        {
            var request = new Request
            {
                Url = "http://www.reddit.com/reddits/mine/moderator.json",
                Method = "GET",
                Cookie = session.Cookie
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);
            
            var o = JObject.Parse(json);
            return SubListing.FromJson(o);
        }

        private const int Limit = 100;

        public static PostListing GetListing(Session session, string sub)
        {
            return GetListing(session, sub, SubSortBy.Hot, string.Empty, string.Empty);
        }

        public static PostListing GetListing(Session session, string sub, SubSortBy sort)
        {
            return GetListing(session, sub, sort, string.Empty, string.Empty);
        }

        public static PostListing GetListing(Session session, string sub, string after)
        {
            return GetListing(session, sub, SubSortBy.Hot, after, string.Empty);
        }

        public static PostListing GetListing(Session session, string sub, string after, string before)
        {
            return GetListing(session, sub, SubSortBy.Hot, after, before);
        }

        public static PostListing GetListing(Session session, string sub, SubSortBy sort, string after)
        {
            return GetListing(session, sub, sort, after, string.Empty);
        }

        public static PostListing GetListing(Session session, string sub, SubSortBy sort, string after, string before)
        {
            var url = "http://www.reddit.com/r/" + sub + "/";
            switch (sort)
            {
                case SubSortBy.Hot:
                    url += ".json?limit=" + Limit;
                    break;

                case SubSortBy.New:
                    url += "new/.json?sort=new&limit=" + Limit;
                    break;

                case SubSortBy.Rising:
                    url += "new/.json?sort=rising&limit=" + Limit;
                    break;

                case SubSortBy.TopAllTime:
                    url += "top/.json?sort=top&t=all&limit=" + Limit;
                    break;

                case SubSortBy.TopYear:
                    url += "top/.json?sort=top&t=year&limit=" + Limit;
                    break;

                case SubSortBy.TopMonth:
                    url += "top/.json?sort=top&t=month&limit=" + Limit;
                    break;

                case SubSortBy.TopWeek:
                    url += "top/.json?sort=top&t=week&limit=" + Limit;
                    break;

                case SubSortBy.TopToday:
                    url += "top/.json?sort=top&t=day&limit=" + Limit;
                    break;

                case SubSortBy.TopHour:
                    url += "top/.json?sort=top&t=hour&limit=" + Limit;
                    break;

                case SubSortBy.ControversalAllTime:
                    url += "controversial/.json?sort=controversial&t=all&limit=" + Limit;
                    break;

                case SubSortBy.ControversalYear:
                    url += "controversial/.json?sort=controversial&t=year&limit=" + Limit;
                    break;

                case SubSortBy.ControversalMonth:
                    url += "controversial/.json?sort=controversial&t=month&limit=" + Limit;
                    break;

                case SubSortBy.ControversalWeek:
                    url += "controversial/.json?sort=controversial&t=week&limit=" + Limit;
                    break;

                case SubSortBy.ControversalToday:
                    url += "controversial/.json?sort=controversial&t=day&limit=" + Limit;
                    break;

                case SubSortBy.ControversalHour:
                    url += "controversial/.json?sort=controversial&t=hour&limit=" + Limit;
                    break;
            }

            if (!string.IsNullOrEmpty(after))
                url += "&after=" + after;

            if (!string.IsNullOrEmpty(before))
                url += "&before=" + before;

            var request = new Request
            {
                Method = "GET",
                Cookie = session.Cookie,
                Url = url                 
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new Exception(json);

            var o = JObject.Parse(json);
            return PostListing.FromJson(o);

        }

        public static UserListing GetModerators(Session session, string sub)
        {
            // 

            var request = new Request
            {
                Method = "GET",
                Cookie = session.Cookie,
                Url = "http://www.reddit.com/r/" + sub + "/about/moderators.json"
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new Exception(json);

            var o = JObject.Parse(json);
            return UserListing.FromJson(o);            
        }

        public static UserListing GetContributors(Session session, string sub)
        {
            var request = new Request
            {
                Method = "GET",
                Cookie = session.Cookie,
                Url = "http://www.reddit.com/r/" + sub + "/about/contributors.json"
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);

            var o = JObject.Parse(json);
            return UserListing.FromJson(o);  
        }

        public static PostListing GetReportedPosts(Session session, string sub)
        {
            // 

            var request = new Request
            {
                Url = "http://www.reddit.com/r/" + sub + "/about/reports/.json",
                Method = "GET",
                Cookie = session.Cookie
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new Exception(json);

            var o = JObject.Parse(json);
            return PostListing.FromJson(o);
        }

        public static TrafficListing GetTrafficStats(Session session, string sub)
        {

            var request = new Request
            {
                Url = "http://www.reddit.com/r/" + sub + "/about/traffic/.json",
                Method = "GET",
                Cookie = session.Cookie
            };


            // Permission error is not thrown, just a 404
            // {"error": 404}

            throw new NotImplementedException();
        }

        public static PostListing GetSpam(Session session, string sub)
        {
            var request = new Request
            {
                Url = "http://www.reddit.com/r/" + sub + "/about/spam/.json",
                Method = "GET",
                Cookie = session.Cookie
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);

            var o = JObject.Parse(json);
            return PostListing.FromJson(o);
        }

        public static LogListing GetModerationLog(Session session, string sub)
        {

            var request = new Request
            {
                Url = "http://www.reddit.com/r/" + sub + "/about/log/",
                Method = "GET",
                Cookie = session.Cookie
            };

            var html = string.Empty;
            if (request.Execute(out html) != System.Net.HttpStatusCode.OK)
                throw new RedditException(html);

            // build JSON?
            
            return LogListing.FromHtml(html);
        }

        public static UserListing GetBannedUsers(Session session, string sub)
        {
            var request = new Request
            {
                Url = "http://www.reddit.com/r/" + sub + "/about/banned/.json",
                Method = "GET",
                Cookie = session.Cookie
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);

            var o = JObject.Parse(json);
            return UserListing.FromJson(o);
        }

        /// <summary>
        /// Search for a subreddit based on it's title and description
        /// </summary>
        /// <param name="session"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static SubListing Search(Session session, string subID, string query)
        {
            // http://www.reddit.com/reddits/search.json?q=cats
            throw new NotImplementedException();

        }

        public static void Create(Session session, Sub sub)
        {
            var subreddit_type = "public";
            var language = "en";
            var content_options = "any";
            var over_18 = false;
            var default_set = true;
            var show_media = false;
            var domain = "";

            var request = new Request
            {
                Url = "",                
                Method = "POST", 
                Cookie = session.Cookie,
                Content = "name=" + sub.Name +
                          "&title=" + sub.Title +
                          "&description=" + sub.Description +
                          "&lang=" + language +
                          "&type=" + subreddit_type + 
                          "&link_type=" + content_options + 
                          "&over_18=" + (over_18 ? "on" : "off") + 
                          "&allow_top=" + (default_set ? "on" : "off") + 
                          "&show_media=" + (show_media ? "on" : "off") + 
                          "&domain=" + domain + 
                          "&uh=" + session.ModHash + 
                          "&id=#sr-form&api_type=json"
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);

            var o = JObject.Parse(json);
        }

        public static void BanUser(Session session, string sub, string sub_id, string username, string modhash)
        {
            var request = new Request
            {
                Url = "http://www.reddit.com/api/friend",
                Method = "POST",
                Cookie = session.Cookie,
                Content = "action=add" + 
                          "&container=" + sub_id + 
                          "&type=banned" +
                          "&name=" + username +
                          "&id=#banned" + 
                          "&r=" + sub + 
                          "&uh=" + modhash +
                          "&renderstyle=html"
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);

            var o = JObject.Parse(json);
        }

        public static void UnBanUser(Session session, string sub, string sub_id, string user_id, string modhash)
        {
            var request = new Request
            {
                Url = "http://www.reddit.com/api/unfriend",
                Method = "POST",
                Cookie = session.Cookie,
                Content = "id=" + user_id + 
                          "&executed=removed" + 
                          "&container=" + sub_id +
                          "&type=banned" + 
                          "&r=" + sub + 
                          "&uh=" + modhash + 
                          "&renderstyle=html"
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);

            var o = JObject.Parse(json);
        }

    }
}
