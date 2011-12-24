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

        }

        [TestMethod]
        public void ListUserComments()
        {
        }

        [TestMethod]
        public void SubmitComment()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void VoteUp()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void VoteDown()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void VoteNull()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Save()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void UnSave()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Hide()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void UnHide()
        {
            Assert.Fail();
        }
    }
}
