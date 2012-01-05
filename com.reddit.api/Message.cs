using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NSoup;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace com.reddit.api
{
    public sealed class Message
    {

        #region // Constructor //

        #endregion

        #region // Properties //

        [JsonProperty("body")]
        public string Body
        {
            get;
            set;
        }

        [JsonProperty("was_comment")]
        public bool WasComment
        {
            get;
            set;
        }

        [JsonProperty("first_message")]
        public object FirstMessage
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

        [JsonProperty("created")]
        public DateTime Created
        {
            get;
            set;
        }

        [JsonProperty("dest")]
        public string Destination
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

        [JsonProperty("subreddit")]
        public string SubReddit
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

        [JsonProperty("context")]
        public string Context
        {
            get;
            set;
        }

        [JsonProperty("replies")]
        public string Replies
        {
            get;
            set;
        }

        [JsonProperty("new")]
        public bool New
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

        [JsonProperty("subject")]
        public string Subject
        {
            get;
            set;
        }      

        #endregion 

        #region // Data Access //

        public static void Read(Session session, params string[] id)
        {
            var request = new Request
            {
                Url = "http://www.reddit.com/api/read_message",
                Method = "POST",
                Cookie = session.Cookie,
                Content = "id=" + string.Join(",", id) + "&uh=" + session.ModHash
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);
            
            var o = JObject.Parse(json);

        }

        public static MessageListing Unread(Session session)
        {
            // 
            var request = new Request
            {
                Method = "GET",
                Cookie = session.Cookie,
                Url = "http://www.reddit.com/message/unread/.json"
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new Exception(json);

            var o = JObject.Parse(json);
            return MessageListing.FromJson(o);
        }

        public static MessageListing Sent(Session session)
        {
            var request = new Request
            {
                Method = "GET",
                Cookie = session.Cookie,
                Url = "http://www.reddit.com/message/sent/.json"
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new RedditException(json);

            var o = JObject.Parse(json);
            return MessageListing.FromJson(o);
        }

        public static void Send(Session session, Message message, string iden, string captcha)
        {
            var request = new Request 
            {
                Url = "http://www.reddit.com/api/compose",
                Method = "POST",
                Cookie = session.Cookie,
                Content = "uh=" + session.ModHash + 
                          "&to=" + message.Destination + 
                          "&subject=" + message.Subject +
                          "&thing_id=" +
                          "&text=" + message.Body + 
                          "&iden=" + iden + 
                          "&captcha=" + captcha + 
                          "&id=#compose-message" + 
                          "&renderstyle=html"
            };


            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
            { 
            
            }


            /*
            {
                "jquery": [
                    [0, 1, "call", ["#compose-message"]],
                    [1, 2, "attr", "find"],
                    [2, 3, "call", [".status"]],
                    [3, 4, "attr", "hide"],
                    [4, 5, "call", []],
                    [5, 6, "attr", "html"],
                    [6, 7, "call", [""]],
                    [7, 8, "attr", "end"],
                    [8, 9, "call", []],
                    [1, 10, "attr", "find"],
                    [10, 11, "call", [".status"]],
                    [11, 12, "attr", "show"],
                    [12, 13, "call", []],
                    [13, 14, "attr", "html"],
                    [14, 15, "call", ["your message has been delivered"]],
                    [15, 16, "attr", "end"],
                    [16, 17, "call", []],
                    [1, 18, "attr", "find"],
                    [18, 19, "call", ["*[name=captcha]"]],
                    [19, 20, "attr", "attr"],
                    [20, 21, "call", ["value", ""]],
                    [21, 22, "attr", "end"],
                    [22, 23, "call", []],
                    [1, 24, "attr", "find"],
                    [24, 25, "call", ["*[name=to]"]],
                    [25, 26, "attr", "attr"],
                    [26, 27, "call", ["value", ""]],
                    [27, 28, "attr", "end"],
                    [28, 29, "call", []],
                    [1, 30, "attr", "find"],
                    [30, 31, "call", ["*[name=text]"]],
                    [31, 32, "attr", "attr"],
                    [32, 33, "call", ["value", ""]],
                    [33, 34, "attr", "end"],
                    [34, 35, "call", []],
                    [1, 36, "attr", "find"],
                    [36, 37, "call", ["*[name=subject]"]],
                    [37, 38, "attr", "attr"],
                    [38, 39, "call", ["value", ""]],
                    [39, 40, "attr", "end"],
                    [40, 41, "call", []]
                ]
            }
             */ 
        }

        public static void GetSendForm(Session session, out string iden, out string captcha)
        {
            var request = new Request 
            {
                Cookie = session.Cookie,
                Method = "GET",
                Url = "http://www.reddit.com/message/compose"
            };
            
            var html = string.Empty;
            if (request.Execute(out html) != System.Net.HttpStatusCode.OK)
                throw new Exception(html);

            // use NSOUP to get the iden string and captcha URL
            var client = NSoup.NSoupClient.Parse(html);
            
            iden = client.Select("#compose-message input[name=iden]").Val();
            captcha = client.Select("#compose-message img.capimage").Attr("src");
        }

        #endregion 

        #region // Conversion // 


        internal static Message FromJson(JToken token)
        {
            return new Message
            {

            };            
        }

        #endregion 

    }
}
