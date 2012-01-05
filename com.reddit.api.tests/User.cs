using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.reddit.api.tests 
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UserTestClass
    {         
        [TestMethod]
        public void Login()
        {
            // login using regular creds
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));

            // check we got the expected data back
            Assert.IsFalse(string.IsNullOrEmpty(session.Username));
            Assert.IsFalse(string.IsNullOrEmpty(session.Password));
            Assert.IsFalse(string.IsNullOrEmpty(session.ModHash));
            Assert.IsFalse(string.IsNullOrEmpty(session.Cookie));
        }

        [TestMethod]
        [ExpectedException(typeof(RedditException))]
        public void Logout()
        {
            // login using regular creds
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));

            // logout the session
            User.Logout(session);

            // attempt to get user saved posts (should result in an error)
            var posts = User.GetSaved(session);
            if (posts.Count > 0)
                Assert.Fail("Getting saved posts should of resulted in an exception, we have not logged out.");
        }

        [TestMethod]
        public void ListSaved()
        {
            // login using regular creds
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));
            var posts = User.GetSaved(session);
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count > 0);
        }

        [TestMethod]
        public void ListSubmitted()
        {
            // login using regular creds
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));

            PostListing submissions;
            CommentListing comments;
            User.GetSubmissionsAndComments(session, out submissions, out comments);
            
        }
        
        [TestMethod]
        public void ListComments()
        {
            // login using regular creds
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));

            PostListing submissions;
            CommentListing comments;
            User.GetSubmissionsAndComments(session, out submissions, out comments);
            
        }
        
        [TestMethod]
        public void GetLiked()
        {
            // login using regular creds
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));
            var liked = User.GetLiked(session);

            Assert.IsNotNull(liked);
            Assert.IsTrue(liked.Count > 0);
        }        
            
        [TestMethod]
        public void GetDisliked()
        {
            // login using regular creds
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));
            var disliked = User.GetDisliked(session);

            Assert.IsNotNull(disliked);
            Assert.IsTrue(disliked.Count > 0);
        }
            
    }
}
