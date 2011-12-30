using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace com.reddit.api
{
    public sealed class FlairListing : List<Flair>
    {
        public string Next
        {
            get;
            set;
        }

        public string Prev
        {
            get;
            set;
        }

        #region // Conversion //

        public static FlairListing FromJson(JToken token)
        {
            var list = new FlairListing();

            foreach (var child in token.Children())
                list.Add(Flair.FromJson(child));

            return list;

        }

        #endregion

    }
}
