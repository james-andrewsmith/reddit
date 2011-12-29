using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            throw new NotImplementedException();
        }

        public static void Add()
        {

        }

        public static void Remove()
        {

        }
    }
}
