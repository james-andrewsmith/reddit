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
    /// <see cref="https://github.com/reddit/reddit/wiki/API:-me.json"/>
    /// </summary>
    public sealed class User
    {

        #region // Properties //

        /// <summary>
        /// Comments which the user has made
        /// </summary>
        public List<Comment> Comments
        {
            get;
            set;
        }

        /// <summary>
        /// Submissions the user has made
        /// </summary>
        public List<Post> Posts
        {
            get;
            set;
        }


        public bool HasMail
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public DateTime Created
        {
            get;
            set;
        }

        public string ModHash
        {
            get;
            set;
        }

        public DateTime CreatedUtc
        {
            get;
            set;
        }

        public int LinkKarma
        {
            get;
            set;
        }

        public int CommentKarma
        {
            get;
            set;
        }

        /// <summary>
        /// Says whether the user is a reddit gold member.
        /// </summary>
        public bool IsGold
        {
            get;
            set;
        }

        /// <summary>
        /// Says whether the user is a moderator.
        /// </summary>
        public bool IsMod
        {
            get;
            set;
        }

        /// <summary>
        /// The user's ID. This is only used internally, right?.
        /// </summary>
        public string ID
        {
            get;
            set;
        }

        /// <summary>
        /// Says whether the user has unread moderator mail.
        /// </summary>
        public bool HasModMail
        {
            get;
            set;
        }

        #endregion

        public static User FromJson(JToken token)
        {
            return new User
            {
                ID = token["id"].ToString(),
                Name = token["name"].ToString()
            };
        }

        public static Session Login(string username, string password)
        {
            var request = new Request
            {
                Url = "https://ssl.reddit.com/api/login/" + username,
                Method = "POST",
                Content = "api_type=json&user=" + username + "&passwd=" + password
            };

            // get the modhash 
            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(json);
            }

            /* // Expected Json
            {
                "json": {
                    "errors": [],
                    "data": {
                        "modhash": "u4abc21302316ad40013feb16cfccb0b11b786596e5194de14",
                        "cookie": "1234567,2011-07-12T14:53:59,0200b365fa02c61f9532ab244b214bd481941492"
                    }
                }
            }
            */

            // Failure
            // {"json": {"errors": [["WRONG_PASSWORD", "invalid password"]]}}

            var o = JObject.Parse(json);

            // Create a session which can be used on further requests
            return new Session
            {
                Username = username,
                Password = password,
                ModHash = o["json"]["data"]["modhash"].ToString(),
                // reddit_session=8010059%2C2011-01-26T06%3A16%3A49%2C8e77c04fb74713f923c6ed7f44ae8374300db499;
                Cookie = "reddit_session=" + System.Web.HttpUtility.UrlEncode(o["json"]["data"]["cookie"].ToString())
            };
        }

        public static void Logout(Session session)
        {
            var request = new Request
            {
                Url = "http://www.reddit.com/logout?uh=" + session.ModHash,
                Method = "POST",
                Cookie = session.Cookie,
                Content = "uh=" + session.ModHash + "&top=off"
            };
            
            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);           
        }

        #region // Get Saved //

        public static PostListing GetSaved(Session session)
        {
            return GetSaved(session, string.Empty);
        }

        public static PostListing GetSaved(Session session, string after)
        {
            return GetSaved(session, string.Empty, string.Empty);
        }

        public static PostListing GetSaved(Session session, string after, string before)
        {
            // 
            var request = new Request
            {
                Url = "http://www.reddit.com/saved/.json",
                Method = "GET",
                Cookie = session.Cookie
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new Exception(json);

            var o = JObject.Parse(json);
            return PostListing.FromJson(o);
        }
        #endregion

        public static User Get(Session session, string username)
        {
            // build a request
            var request = new Request
            {
                Url = "http://www.reddit.com/user/" + username + "/about.json",
                Method = "GET",
                Cookie = session.Cookie
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new Exception(json);

            var o = JObject.Parse(json);
            return User.FromJson(o["data"]);
        }

        public static void GetSubmissionsAndComments(Session session, out PostListing posts, out CommentListing comments)
        {
            posts = null;
            comments = null;
        }

        public static PostListing GetLiked(Session session)
        {
            return GetLiked(session, session.Username);
        }

        public static PostListing GetLiked(Session session, string username)
        {
            // 
            var request = new Request
            {
                Url = "http://www.reddit.com/user/" + username + "/liked/.json",
                Method = "GET",
                Cookie = session.Cookie
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);

            var o = JObject.Parse(json);
            return PostListing.FromJson(o);
        }

        
        public static PostListing GetDisliked(Session session)
        {
            return GetDisliked(session, session.Username);
        }

        public static PostListing GetDisliked(Session session, string username)
        {
            // 
            var request = new Request
            {
                Url = "http://www.reddit.com/user/" + username + "/disliked/.json",
                Method = "GET",
                Cookie = session.Cookie
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);

            var o = JObject.Parse(json);
            return PostListing.FromJson(o);
        }


    }
}
