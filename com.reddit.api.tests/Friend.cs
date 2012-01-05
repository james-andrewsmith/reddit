using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.reddit.api.tests
{
    [TestClass]
    public class FriendTestClass
    {
        [TestMethod]
        public void ListFriends()
        {
            // login using regular creds
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));
            var friends = Friend.List(session);

            Assert.IsNotNull(friends);
            Assert.IsTrue(friends.Count > 0, "No friends in list, ensure you have at least one reddit friend. If so, something went wrong with the friend list request");
        }
        
        [TestMethod]
        public void Add_Remove()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ListFriendPosts()
        {
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));
            var posts = Friend.GetPosts(session);

            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count > 0);

        }

        [TestMethod]
        public void ListFriendComments()
        {
            // http://www.reddit.com/r/friends/comments/
            Assert.Fail();
        }
    }
}
