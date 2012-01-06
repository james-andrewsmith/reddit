using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace com.reddit.api
{
    public sealed class Comment
    {

        #region // Constructors //

        public Comment() 
        {
            Replies = new CommentListing();
        }

        internal Comment(JToken token)
        {
            Replies = new CommentListing();

            // convert from json
            Body = token["body"].ToString();
            SubID = token["subreddit_id"].ToString();
            AuthorFlairCssClass = token["author_flair_css_class"].ToString();
            Created = Convert.ToInt32(token["created"].ToString()).ToDateTime();
            AuthorFlairText = token["author_flair_text"].ToString();
            Downs = Convert.ToInt32(token["downs"].ToString());
            Author = token["author"].ToString();
            CreatedUtc = Convert.ToInt32(token["created_utc"].ToString()).ToDateTime();
            BodyHtml = token["body_html"].ToString();
            LinkID = token["link_id"].ToString();
            ParentID = token["parent_id"].ToString();
            Likes = token["likes"].ToString();
            ID = token["id"].ToString();
            SubReddit = token["subreddit"].ToString();
            Ups = Convert.ToInt32(token["ups"].ToString());
            Name = token["name"].ToString();

            // parse any child comments in
            if (token["replies"].HasValues)
            {
                // set the modhash etc 
                Replies.ModHash = token["replies"]["data"]["modhash"].ToString();
                
                // recursive reply logic
                foreach (var child in token["replies"]["data"]["children"].Children())
                {
                    // find any 'more' recrods and attach here
                    switch (child["kind"].ToString())
                    { 
                        case "t1":
                            Replies.Add(new Comment(child["data"]));
                            break;
                            
                        case "more":
                            var test = child["data"]["children"].Values().Select(t => t.ToString()).ToList();
                            Replies.More.AddRange(test);
                            break;
                    }

                }
            }
                
        }

        #endregion 

        #region // Properties //
        /*
        public string Content
        {
            get;
            set;
        }

        public string ContentHtml
        {
            get;
            set;
        }

        public string ContentText
        {
            get;
            set;
        }

        public string ID
        {
            get;
            set;
        }

        public string Link
        {
            get;
            set;
        }

        public string Parent
        {
            get;
            set;
        }
        */

        [JsonProperty("body")]
        public string Body 
        {
            get;
            set;
        }
        
        [JsonProperty("subreddit_id")]
        public string SubID 
        {
            get;
            set;
        }

        [JsonProperty("author_flair_css_class")]
        public string AuthorFlairCssClass 
        {
            get;
            set;
        }

        
        [JsonProperty("created")]
        public DateTime Created 
        {
            get;
            set;
        }
        
        [JsonProperty("author_flair_text")]
        public string AuthorFlairText 
        {
            get;
            set;
        } 
        
        [JsonProperty("downs")]
        public int Downs 
        {
            get;
            set;
        }
        
        [JsonProperty("author")]
        public string Author 
        {
            get;
            set;
        }
        
        [JsonProperty("created_utc")]
        public DateTime CreatedUtc 
        {
            get;
            set;
        }

        [JsonProperty("body_html")]
        public string BodyHtml 
        {
            get;
            set;
        }
        
        [JsonProperty("link_id")]
        public string LinkID 
        {
            get;
            set;
        }
        
        [JsonProperty("parent_id")]
        public string ParentID 
        {
            get;
            set;
        }
        
        [JsonProperty("likes")]
        public string Likes 
        {
            get;
            set;
        }
        
        [JsonProperty("id")]
        public string ID 
        {
            get;
            set;
        }
         
        [JsonProperty("subreddit")]
        public string SubReddit 
        {
            get;
            set;
        }
        
        [JsonProperty("ups")]
        public int Ups 
        {
            get;
            set;
        }
        
        [JsonProperty("name")]
        public string Name 
        {
            get;
            set;
        }

        public CommentListing Replies
        {
            get;
            set;
        }

        #endregion

        #region // Actions //

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

        public static void Hide(Session session, string id, string modhash)
        {
            Thing.Hide(session, id, modhash);
        }

        public static void UnHide(Session session, string id, string modhash)
        {
            Thing.UnHide(session, id, modhash);
        }

        public static void Save(Session session, string id, string modhash)
        {
            Thing.Save(session, id, modhash);
        }

        public static void UnSave(Session session, string id, string modhash)
        {
            Thing.UnSave(session, id, modhash);
        }

        public static void Report(Session session, string id, string modhash)
        {
            Thing.Report(session, id, modhash);
        }

        #endregion 

        #region // Data Access //

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="id">The FULLNAME (see glossary on the API page) of the thing or comment you are commenting on.</param>
        /// <param name="markdown">The markdown content of the comment you are posting.</param>
        public static Comment Submit(Session session, string id, string markdown)
        {
            // http://www.reddit.com/api/comment

            var request = new Request
            {
                Url = "http://www.reddit.com/api/comment",
                Cookie = session.Cookie,
                Method = "POST",
                Content = "uh=" + session.ModHash +
                          "&thing_id=" + id +
                          "&text=" + markdown
            };

            // jquery[18][3][0][0]
            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new Exception(json);

            var o = JObject.Parse(json);
            var comment = o["jquery"][18][3][0][0]["data"].ToString();
            // really we just want to get the ID of the comment here
            return new Comment();
        }

        #region // Get Comments For Post //

        public static CommentListing GetCommentsForPost(Session session, string id)
        {
            return GetCommentsForPost(session, id, CommentSortBy.Hot, string.Empty);
        }

        public static CommentListing GetCommentsForPost(Session session, string id, CommentSortBy sort)
        {
            return GetCommentsForPost(session, id, sort, string.Empty);
        }

        public static CommentListing GetCommentsForPost(Session session, string id, string more)
        {
            return GetCommentsForPost(session, id, CommentSortBy.Hot, more);
        }

        public static CommentListing GetCommentsForPost(Session session, string id, CommentSortBy sort, string more)
        {
            var url = "http://www.reddit.com/comments/" + id + ".json";

            var request = new Request
            {
                Url = url,
                Method = "GET",
                Cookie = session.Cookie
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);

            // use some tricky recursive linq to get the comment structure,
            // understanding this required the use of caffeine. I'm tired.
            var o = JArray.Parse(json);

            // skip / ignore the post
            // o[0]
            
            // loop over the top level comments
            var list = new CommentListing();
            var comments = o[1]["data"]["children"]; //.Select(t => t["data"]);
            list.ModHash = o[1]["data"]["modhash"].ToString();

            foreach (var comment in comments)
            {
                switch(comment["kind"].ToString())
                {
                    case "t1":
                        list.Add(new Comment(comment["data"]));
                        break;
                    case "more":
                        var test = comment["data"]["children"].Values().Select(t => t.ToString()).ToList();
                        list.More.AddRange(test);                        
                        break;
                }
            }

            return list;
        }

        #endregion

        #endregion

    }
}
