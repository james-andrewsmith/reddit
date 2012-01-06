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
    /// Story, Post, Link - really we could use any of these terms, post 
    /// seemed to make sense because we 'post' to reddit / a subreddit. 
    /// </summary>
    public sealed class Post
    {

        #region // Properties //

        public string Domain
        {
            get;
            set;
        }

        public MediaEmbed MediaEmbed
        {
            get;
            set;
        }

        // public object LevenShtein
        // {
        //     get;
        //     set;
        // }

        public string SubReddit
        {
            get;
            set;
        }

        public string SelfTextHtml
        {
            get;
            set;
        }

        public string SelfText
        {
            get;
            set;
        }

        public object Likes
        {
            get;
            set;
        }

        public bool Saved
        {
            get;
            set;
        }
        public string ID
        {
            get;
            set;
        }

        public bool Clicked 
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public Media Media
        {
            get;
            set;
        }

        public int Score
        {
            get;
            set;
        }

        public bool Over18 
        {
            get;
            set;
        }

        public bool Hidden
        {
            get;
            set;
        }

        public string Thumbnail
        {
            get;
            set;
        }

        public string SubRedditID
        {
            get;
            set;
        }

        public object AuthorFlairCssClass
        {
            get;
            set;
        }

        public int Downs
        {
            get;
            set;
        }

        public bool IsSelf
        {
            get;
            set;
        }

        public string PermaLink 
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

        public string Url
        {
            get;
            set;
        }       
        
        public string AuthorFlairText
        {
            get;
            set;
        }

        public string Author 
        {
            get;
            set;
        }

        public DateTime CreatedUtc
        {
            get;
            set;
        }

        public int NumComments
        {
            get;
            set;
        }

        public int Ups
        {
            get;
            set;
        }
        
        public List<Comment> Comments
        {
            get;
            set;
        }

        #endregion 

        #region // Conversion //        

        internal static Post FromJson(JToken data)
        {
            return new Post
            {
                Domain = data["domain"].ToString(),
                // "media_embed": {},
                // "levenshtein": null,
                SubReddit = data["subreddit"].ToString(),
                SelfTextHtml = data["selftext_html"].ToString(),
                SelfText = data["selftext"].ToString(),
                // "likes": null,
                Saved = Convert.ToBoolean(data["saved"].ToString()),
                ID = data["id"].ToString(),
                Clicked = Convert.ToBoolean(data["clicked"].ToString()),
                Title = data["title"].ToString(),
                // "media": null,
                Score = Convert.ToInt32(data["score"].ToString()),
                Over18 = Convert.ToBoolean(data["over_18"].ToString()),
                Hidden = Convert.ToBoolean(data["hidden"].ToString()),
                Thumbnail = data["thumbnail"].ToString(),
                SubRedditID = data["subreddit_id"].ToString(),
                // "author_flair_css_class": null,
                Downs = Convert.ToInt32(data["downs"].ToString()),
                IsSelf = Convert.ToBoolean(data["is_self"].ToString()),
                PermaLink = data["permalink"].ToString(),
                Name = data["name"].ToString(),
                Created = Convert.ToInt32(data["created"].ToString()).ToDateTime(),
                Url = data["url"].ToString(),
                // "author_flair_text": null,
                Author = data["author"].ToString(),
                CreatedUtc = Convert.ToInt32(data["created_utc"].ToString()).ToDateTime(),
                NumComments = Convert.ToInt32(data["num_comments"].ToString()),
                Ups = Convert.ToInt32(data["ups"].ToString())
            };
        }

        internal static string ToJson(Post post)
        {
            return JsonConvert.SerializeObject(post, Formatting.Indented);
        }

        #endregion

        #region // Actions //

        public static PostListing Get(Session session, string id)
        {
            // check this is a link (in the correct format)
            if (!id.StartsWith("t3_"))
                throw new RedditException("ID is not of a link/post");

            // build a request
            var request = new Request
            {
                Url = "http://www.reddit.com/by_id/" + id + ".json",
                Cookie = session.Cookie
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);

            return JsonConvert.DeserializeObject<PostListing>(json);
        }

        public static CommentListing GetComments(Session session, string id)
        {
            // Make sure we process the request with the type removed, so 
            // we just pass the base-36 ID
            id = id.Replace("t3_", string.Empty);

            // build a request
            var request = new Request
            {
                Url = "http://www.reddit.com/comments/" + id + ".json",
                Cookie = session.Cookie
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new Exception(json);

            return JsonConvert.DeserializeObject<CommentListing>(json);
        }

        public static PostListing GetRelated(Session session, string id)
        {
            // Make sure we process the request with the type removed, so 
            // we just pass the base-36 ID
            id = id.Replace("t3_", string.Empty);

            // build a request
            var request = new Request
            {
                Url = "http://www.reddit.com/related/" + id + ".json",
                Cookie = session.Cookie
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new Exception(json);

            return JsonConvert.DeserializeObject<PostListing>(json);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="post"></param>
        /// <see cref="https://github.com/reddit/reddit/wiki/API"/>
        public static void Submit(Session session, Post post, PostKind kind)
        {
            if (string.IsNullOrEmpty((kind == PostKind.Link ? post.Url : post.SelfText)))
                throw new Exception("No link or self text added to the new post");

            if (string.IsNullOrEmpty(post.SubReddit))
                throw new Exception("No subreddit set");

            if (string.IsNullOrEmpty(post.Title))
                throw new Exception("No title provided");

            var request = new Request
            {
                Url = "http://www.reddit.com/api/submit",
                Method = "POST",
                Cookie = session.Cookie,
                Content = "uh=" + session.ModHash + 
                          "&kind=" + (kind == PostKind.Link ? "link" : "self") + 
                          "&url=" + (kind == PostKind.Link ? post.Url : post.SelfText) + 
                          "&sr=" + post.SubReddit + 
                          "&title=" + post.Title + 
                          "&r=" + post.SubReddit + 
                          "&renderstyle=html"
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new Exception(json);

            var o = JObject.Parse(json);

            // Capcha
            // o["jquery"][10][3].ToString()

            // Error Message
            // o["jquery"][12][3].ToString()
        }

        /// <summary>
        /// https://github.com/reddit/reddit/wiki/API:-hide
        /// </summary>
        /// <param name="session"></param>
        /// <param name="id"></param>
        public static void Hide(Session session, string id, string modhash)
        {
            // http://www.reddit.com/api/hide
            Thing.Hide(session, id, modhash);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="id"></param>
        /// <see cref="https://github.com/reddit/reddit/wiki/API:-report"/>
        public static void Report(Session session, string id, string modhash)
        {
            Thing.Report(session, id, modhash);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="id"></param>
        /// <see cref="https://github.com/reddit/reddit/wiki/API:-save"/>
        public static void Save(Session session, string id, string modhash)
        {
            Thing.Save(session, id, modhash);
        }


        public static void UnSave(Session session, string id, string modhash)
        {

            Thing.UnSave(session, id, modhash);
        }

        public static void UnHide(Session session, string id, string modhash)
        {

            Thing.UnHide(session, id, modhash);
        }

        public static void VoteUp(Session session, string id, string modhash)
        {

            Thing.VoteUp(session, id, modhash);
        }

        public static void VoteDown(Session session, string id, string modhash)
        {

            Thing.VoteDown(session, id, modhash);
        }

        public static void VoteNull(Session session, string id, string modhash)
        {

            Thing.VoteNull(session, id, modhash);
        }

        public static void Nsfw(Session session, string sub, string id, string modhash)
        {
            //  http://www.reddit.com/api/marknsfw
            var request = new Request
            {
                Url = "http://www.reddit.com/api/marknsfw",
                Method = "POST",
                Cookie = session.Cookie,
                Content = "id=" + id +
                          "&executed=marked" +
                          "&r=" + sub +
                          "&uh=" + modhash + 
                          "&renderstyle=html"
            };


            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);

            var o = JObject.Parse(json);

        }

        public static void UnNsfw(Session session, string sub, string id, string modhash)
        {

            var request = new Request
            {
                Url = "http://www.reddit.com/api/unmarknsfw",
                Method = "POST",
                Cookie = session.Cookie,
                Content = "id=" + id +
                          "&executed=unmarked" +
                          "&r=" + sub +
                          "&uh=" + modhash +
                          "&renderstyle=html"
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);

            var o = JObject.Parse(json);

        }


        public static void Approve(Session session, string sub, string id, string modhash)
        {

            var request = new Request
            {
                Url = "http://www.reddit.com/api/approve",
                Method = "POST",
                Cookie = session.Cookie,
                Content = "id=" + id +
                          "&executed=approved" +
                          "&r=" + sub +
                          "&uh=" + modhash +
                          "&renderstyle=html"
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);

            var o = JObject.Parse(json);

        }


        public static void Remove(Session session, string sub, string id, string modhash)
        {

            var request = new Request
            {
                Url = "http://www.reddit.com/api/remove",
                Method = "POST",
                Cookie = session.Cookie,
                Content = "id=" + id +
                          "&executed=removed" +
                          "&r=" + sub +
                          "&uh=" + modhash +
                          "&renderstyle=html"
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);

            var o = JObject.Parse(json);

        }


        public static void Distinguish(Session session, string sub, string id, string modhash)
        {

            var request = new Request
            {
                Url = "http://www.reddit.com/api/distinguish/yes",
                Method = "POST",
                Cookie = session.Cookie,
                Content = "id=" + id +
                          "&executed=distinguishing..." +
                          "&r=" + sub +
                          "&uh=" + modhash +
                          "&renderstyle=html"
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);

            var o = JObject.Parse(json);

        }

        public static void UnDistinguish(Session session, string sub, string id, string modhash)
        {

            var request = new Request
            {
                Url = "http://www.reddit.com/api/distinguish/no",
                Method = "POST",
                Cookie = session.Cookie,
                Content = "id=" + id +
                          "&executed=distinguishing..." +
                          "&r=" + sub +
                          "&uh=" + modhash +
                          "&renderstyle=html"
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);

            var o = JObject.Parse(json);

        }

        public static void Delete(Session session, string sub, string id, string modhash)
        {

            var request = new Request
            {
                Url = "http://www.reddit.com/api/del",
                Method = "POST",
                Cookie = session.Cookie,
                Content = "id=" + id +
                          "&executed=deleted" +
                          "&r=" + sub +
                          "&uh=" + modhash +
                          "&renderstyle=html"
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);

            var o = JObject.Parse(json);

        }

        #endregion

    }
}
