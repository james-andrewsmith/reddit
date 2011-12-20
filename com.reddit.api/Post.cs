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
    /// 
    /// </summary>
    public sealed class Post
    {

        #region // Properties //

        public string Domain
        {
            get;
            set;
        }

        public object MediaEmbed
        {
            get;
            set;
        }

        public object LevenShtein
        {
            get;
            set;
        }

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

        public object Media
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

        internal static PostListing FromJsonList(JToken children)
        {
            var list = new PostListing();

            foreach (var child in children.Children().Select(post => post["data"]))            
                list.Add(FromJson(child));

            return list;
        }

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

        public static PostListing GetRelated(Session session, Post post)
        {

        }

        public static void Submit(Session session, Post post)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// https://github.com/reddit/reddit/wiki/API:-hide
        /// </summary>
        /// <param name="session"></param>
        /// <param name="id"></param>
        public static void Hide(Session session, string id)
        {
            // http://www.reddit.com/api/hide
            Thing.Hide(session, id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="id"></param>
        /// <see cref="https://github.com/reddit/reddit/wiki/API:-report"/>
        public static void Report(Session session, string id)
        {
            Thing.Report(session, id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="id"></param>
        /// <see cref="https://github.com/reddit/reddit/wiki/API:-save"/>
        public static void Save(Session session, string id)
        {
            Thing.Save(session, id);
        }


        public static void UnSave(Session session, string id)
        {

            Thing.UnSave(session, id);
        }

        public static void UnHide(Session session, string id)
        {

            Thing.UnHide(session, id);
        }

        public static void VoteUp(Session session, string id)
        {

            Thing.VoteUp(session, id);
        }

        public static void VoteDown(Session session, string id)
        {

            Thing.VoteDown(session, id);
        }

        public static void VoteNull(Session session, string id)
        {

            Thing.VoteNull(session, id);
        }

        #endregion

    }
}
