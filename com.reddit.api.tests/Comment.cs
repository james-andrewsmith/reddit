using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.reddit.api.tests
{
    [TestClass]
    public class CommentTestClass
    {

        /// <summary>
        /// This is the test reddit for this SDK
        /// </summary>
        private string SubRedditToTestModWith = Configuration.GetKey("moderated-subreddit");

        [TestMethod]
        public void GetCommentsForPost()
        {
            // login using regular creds
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));

            // a story about a nice guy who donated his tickets to a child at christmas
            var postID = "nndrb";

            // get a post with comments
            var comments = Comment.GetCommentsForPost(session, postID);

            // list all them
            Assert.IsNotNull(comments);
            Assert.IsTrue(comments.Count > 0);
        }


        [TestMethod]
        public void SubmitComment()
        {
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));

            // find post in the subreddit we mod
            var posts = Sub.GetListing(session, SubRedditToTestModWith);

            if (posts.Count == 0)
                Assert.Fail("There are no posts in " + SubRedditToTestModWith + " which we can comment on");

            // comment on the post
            Comment.Submit(session, posts[0].Name, "This is a test comment made by the CSharp API Unit Tests");

        }

        [TestMethod]
        public void Vote_UpDownNull()
        {
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));

            // get comment
            var id = "nndrb";
            var comment = Comment.GetCommentsForPost(session, id);
                
            Comment.VoteUp(session, comment[0].Name, comment.ModHash);

            Comment.VoteDown(session, comment[0].Name, comment.ModHash);

            Comment.VoteNull(session, comment[0].Name, comment.ModHash); 

        }
         

        [TestMethod]
        public void Hide_UnHide()
        {
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));

            // get comment
            var id = "nndrb";
            var comment = Comment.GetCommentsForPost(session, id);

            
            Comment.Hide(session, comment[0].ID, comment.ModHash);

            // check the comment is hidden
            Comment.UnHide(session, comment[0].ID, comment.ModHash);

            // check the comment is visible
        }
        
        [TestMethod]
        public void Save_UnSave()
        {
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));

            // get comment
            var id = "nndrb";
            var comment = Comment.GetCommentsForPost(session, id);

            var saved = User.GetSaved(session); 

            Comment.Save(session, comment[0].ID, comment.ModHash);

            // check the comment is saved
            if (saved.Where(c => c.ID == comment[0].ID).Count() != 1)
                Assert.Fail("The comemnt was not saved");

            Comment.UnSave(session, comment[0].ID, comment.ModHash);

            // check the comment is not saved
            if (saved.Where(c => c.ID == comment[0].ID).Count() != 0)
                Assert.Fail("The comemnt was not unsaved");
        }
         
    }
}
