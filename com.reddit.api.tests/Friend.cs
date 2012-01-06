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
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));

            var about = User.Get(session, session.Username);

            Friend.Add(session, Configuration.GetKey("second-username"), "t2_" + about.ID, session.ModHash);

            // check the friend list
            var list = Friend.List(session);
            if (list.Where(f => f.Name == Configuration.GetKey("second-username")).Count() != 1)
                Assert.Fail("The user was not successfully added to the friend list");

            // now remove the friend
            Friend.Remove(session, Configuration.GetKey("second-username"), "t2_" + about.ID, session.ModHash);

            // refresh the list
            list = Friend.List(session);
            if (list.Where(f => f.Name == Configuration.GetKey("second-username")).Count() != 0)
                Assert.Fail("The user was not successfully removed from the friend list");            
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
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));

            var friends = Friend.List(session);

            // add some friends
            if (friends.Count == 0)
            {
                var self = User.Get(session, session.Username);

                // some SC2 folks.
                Friend.Add(session, "zngelday9", "t2_" + self.ID, session.ModHash);
                Friend.Add(session, "neodestiny", "t2_" + self.ID, session.ModHash);
                Friend.Add(session, "dApollo", "t2_" + self.ID, session.ModHash);
                Friend.Add(session, "TotalBiscuit", "t2_" + self.ID, session.ModHash);                
            }

            // now check for comments
            var comments = Friend.GetComments(session);
            Assert.IsNotNull(comments);
            Assert.IsTrue(comments.Count > 0);
        }
    }
}
