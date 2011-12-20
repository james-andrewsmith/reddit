using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.reddit.api
{
    public sealed class Comment
    {

        #region // Properties //

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

        public string Replies
        {
            get;
            set;
        }

        #endregion

        #region // Actions //

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

        public static void Hide(Session session, string id)
        {
            Thing.Hide(session, id);
        }

        public static void UnHide(Session session, string id)
        {
            Thing.UnHide(session, id);
        }

        public static void Save(Session session, string id)
        {
            Thing.Save(session, id);
        }

        public static void UnSave(Session session, string id)
        {
            Thing.UnSave(session, id);
        }

        public static void Report(Session session, string id)
        {
            Thing.Report(session, id);
        }

        #endregion 

        #region // Data Access //

        public static void Submit(Session session, string id, Comment comment)
        {
            // http://www.reddit.com/api/comment

            var request = new Request
            {

            };

        }

        public static CommentListing GetCommentsForPost(Session session, Post post)
        {

        }
        #endregion 

        // Get Comment Tree 

    }
}
