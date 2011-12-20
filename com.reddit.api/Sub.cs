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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="sub"></param>
        /// <param name="after"></param>
        /// <param name="before"></param>
        /// <returns></returns>
        public static List<Sub> List(Session session, string sub, string after = "", string before = "")
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the currently subscribed sub reddit's of the user
        /// </summary>
        /// <returns></returns>
        public static List<Sub> GetMine(Session session)
        {
            var list = new List<Sub>();
            var request = new Request
            {
                Url = "http://www.reddit.com/reddits/mine.json",
                Method = "GET",                
                Cookie = session.Cookie
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
            {
                // oops.
                throw new Exception(json);
            }

            var o = JObject.Parse(json);

            foreach (var sub in o["data"]["children"].Children()
                                                     .Select(sub => sub["data"]))
            {
                list.Add(new Sub
                {
                    DisplayName = sub["display_name"].ToString(),
                    Name = sub["name"].ToString(),
                    Title = sub["title"].ToString(),
                    Url = sub["url"].ToString(),
                    Created = Convert.ToInt32(sub["created"].ToString()).ToDateTime(),
                    CreatedUtc = Convert.ToInt32(sub["created_utc"].ToString()).ToDateTime(),
                    Over18 = Convert.ToBoolean(sub["over18"].ToString()),
                    Subscribers = Convert.ToInt32(sub["subscribers"].ToString()),
                    ID = sub["id"].ToString(),
                    Description = sub["description"].ToString()
                });
            }

            return list;
        }

        /// <summary>
        /// A search limited to a specific sub reddit
        /// </summary>
        /// <param name="session"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static List<Sub> Search(Session session, string subID, string query)
        {
            // http://www.reddit.com/reddits/search.json?q=cats
            throw new NotImplementedException();

        }

    }
}
