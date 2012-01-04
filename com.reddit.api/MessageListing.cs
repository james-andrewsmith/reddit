using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace com.reddit.api
{
    public sealed class MessageListing : List<Message>
    {

        #region // Properties //
        public string ModHash
        {
            get;
            set;
        }

        public string After
        {
            get;
            set;
        }

        public string Before
        {
            get;
            set;
        }
        #endregion 

        #region // Conversion //

        internal static MessageListing FromJson(JToken token)
        {
            var list = new MessageListing();
            list.ModHash = token["data"]["modhash"].ToString();
            list.Before = token["data"]["before"].ToString();
            list.After = token["data"]["after"].ToString();

            foreach (var child in token["data"]["children"].Children().Select(post => post["data"]))
                list.Add(Message.FromJson(child));

            return list;
        }

        #endregion

    }
}
