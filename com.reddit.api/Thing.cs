using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.reddit.api
{
    /// <summary>
    /// The reddit API provides all objects back as 'things' with a type, this class 
    /// is a consolidation of thing functions, which are then proxied by object 
    /// specific classes.
    /// </summary>
    internal sealed class Thing
    {
        #region // Actions //

        /// <summary>
        /// https://github.com/reddit/reddit/wiki/API:-hide
        /// </summary>
        /// <param name="session"></param>
        /// <param name="id"></param>
        internal static void Hide(Session session, string id)
        {
            // http://www.reddit.com/api/hide

            // POST

            // id = post thing id
            // uh = modhash

            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="id"></param>
        /// <see cref="https://github.com/reddit/reddit/wiki/API:-report"/>
        internal static void Report(Session session, string id)
        {

            // POST

            // id = post thing id
            // uh = modhash
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="id"></param>
        /// <see cref="https://github.com/reddit/reddit/wiki/API:-save"/>
        internal static void Save(Session session, string id)
        {
            //  http://www.reddit.com/api/save

        }

        internal static void UnSave(Session session, string id)
        {

        }

        internal static void UnHide(Session session, string id)
        {

        }

        internal static void VoteUp(Session session, string id)
        {
            Vote(session, id, 1);
        }

        internal static void VoteDown(Session session, string id)
        {
            Vote(session, id, -1);
        }

        internal static void VoteNull(Session session, string id)
        {
            Vote(session, id, 0);
        }

        private static void Vote(Session session, string id, int direction)
        {
            var request = new Request
            {
                Url = "http://www.reddit.com/api/vote",
                Method = "POST",
                Content = "uh=" + session.ModHash + "&id=" + id + "&dir=" + direction.ToString(),
                Cookie = session.Cookie
            };

            var json = string.Empty;
            if (request.Execute(out json) != System.Net.HttpStatusCode.OK)
                throw new Exception(json);

            if (json != "{}")
                throw new Exception(json);
        }

        #endregion

    }

}
