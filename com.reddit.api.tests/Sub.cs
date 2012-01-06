using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.reddit.api.tests
{
    [TestClass]
    public class SubTestClass
    {
                
        /// <summary>
        /// List all the subreddits
        /// </summary>
        [TestMethod]
        public void ListSubs()
        {
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));
            var list = Sub.List(session);

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count > 0);
        }

        /// <summary>
        /// Get the currently subscribed reddits of the user
        /// </summary>
        [TestMethod]
        public void GetMine()
        {
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));
            var subs = Sub.GetMine(session);
            Assert.IsTrue(subs.Count > 0);
        }

        /// <summary>
        /// Get the currently moderated subreddits
        /// </summary>
        [TestMethod]
        public void GetMineModerated()
        {
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));
            var subs = Sub.GetMineModerated(session);

            Assert.IsNotNull(subs);
            Assert.IsTrue(subs.Count > 0);
        }

        /// <summary>
        /// This is a pretty popular reddit to use.
        /// </summary>
        private const string SubRedditToTestWith = "pics";

        
        [TestMethod]
        public void GetModerators()
        {
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));
            var users = Sub.GetModerators(session, SubRedditToTestModWith);

            Assert.IsNotNull(users);
            Assert.IsTrue(users.Count > 0);
        }


        [TestMethod]
        public void GetContributors()
        {
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));
            var users = Sub.GetContributors(session, SubRedditToTestModWith);

            Assert.IsNotNull(users);
            Assert.IsTrue(users.Count > 0);
        }

        /// <summary>
        /// Test the paging of the PostListing object to get multiple pages of the same data
        /// </summary>
        [TestMethod]
        public void GetSubPosts_Paging()
        {
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));
            
            var page_00 = Sub.GetListing(session, SubRedditToTestWith);
            var page_01 = Sub.GetListing(session, SubRedditToTestWith, page_00.After);
            var page_02 = Sub.GetListing(session, SubRedditToTestWith, page_01.After);
            var page_03 = Sub.GetListing(session, SubRedditToTestWith, page_02.After);
            var page_04 = Sub.GetListing(session, SubRedditToTestWith, page_03.After);

            Assert.IsTrue(page_00.Count > 0);
            Assert.IsTrue(page_01.Count > 0);
            Assert.IsTrue(page_02.Count > 0);
            Assert.IsTrue(page_03.Count > 0);
            Assert.IsTrue(page_04.Count > 0);
        }

        [TestMethod]
        public void GetSubPosts_Paging_Loop()
        {
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));
            var list = new PostListing();
            PostListing page = null;

            do
            {
                page = Sub.GetListing(session, 
                                      SubRedditToTestWith, 
                                      page == null ? string.Empty : page.After);

                list.AddRange(page);

            } while (!string.IsNullOrEmpty(page.After));

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod]
        public void GetSubPosts_Hot()
        {
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));
            var posts = Sub.GetListing(session, SubRedditToTestWith);
            Assert.IsTrue(posts.Count > 0);
        }

        [TestMethod]
        public void GetSubPosts_New()
        {
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));
            var posts = Sub.GetListing(session, SubRedditToTestWith, SubSortBy.New);
            Assert.IsTrue(posts.Count > 0);
        }

        [TestMethod]
        public void GetSubPosts_Rising()
        {
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));
            var posts = Sub.GetListing(session, SubRedditToTestWith, SubSortBy.Rising);
            Assert.IsTrue(posts.Count > 0);
        }

        [TestMethod]
        public void GetSubPosts_TopAllTime()
        {
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));
            var posts = Sub.GetListing(session, SubRedditToTestWith, SubSortBy.TopAllTime);
            Assert.IsTrue(posts.Count > 0);
        }

        [TestMethod]
        public void GetSubPosts_TopHour()
        {
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));
            var posts = Sub.GetListing(session, SubRedditToTestWith, SubSortBy.TopHour);
            Assert.IsTrue(posts.Count > 0);
        }

        [TestMethod]
        public void GetSubPosts_TopMonth()
        {
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));
            var posts = Sub.GetListing(session, SubRedditToTestWith, SubSortBy.TopMonth);
            Assert.IsTrue(posts.Count > 0);
        }

        [TestMethod]
        public void GetSubPosts_TopToday()
        {
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));
            var posts = Sub.GetListing(session, SubRedditToTestWith, SubSortBy.TopToday);
            Assert.IsTrue(posts.Count > 0);
        }

        [TestMethod]
        public void GetSubPosts_TopWeek()
        {
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));
            var posts = Sub.GetListing(session, SubRedditToTestWith, SubSortBy.TopWeek);
            Assert.IsTrue(posts.Count > 0);
        }

        [TestMethod]
        public void GetSubPosts_TopYear()
        {
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));
            var posts = Sub.GetListing(session, SubRedditToTestWith, SubSortBy.TopYear);
            Assert.IsTrue(posts.Count > 0);
        }

        /// <summary>
        /// This is the test reddit for this SDK
        /// </summary>
        private string SubRedditToTestModWith = Configuration.GetKey("moderated-subreddit");

        [TestMethod]
        public void GetReportedPosts()
        {
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));
            var posts = Sub.GetReportedPosts(session, SubRedditToTestModWith);
            Assert.IsTrue(posts.Count > 0);
        }
        
        [TestMethod]
        public void GetTrafficStats()
        {
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));
            var traffic = Sub.GetTrafficStats(session, SubRedditToTestModWith);
            Assert.IsNotNull(traffic);
            Assert.IsTrue(traffic.Count > 0);
        }

        [TestMethod]
        public void GetSpam()
        {
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));
            var posts = Sub.GetSpam(session, SubRedditToTestModWith);
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count > 0);
        }

        [TestMethod]
        public void GetModerationLog()
        {
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));
            var log = Sub.GetModerationLog(session, SubRedditToTestModWith);
            Assert.IsNotNull(log);
            Assert.IsTrue(log.Count > 0);
        }
        
        /// <summary>
        /// Tests the listing, banning and unbanning of users in a subreddit
        /// </summary>
        [TestMethod]
        public void Ban_Unban_User()
        {
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));

            var sub = Sub.Get(session, SubRedditToTestModWith);

            Sub.BanUser(session, SubRedditToTestModWith, sub.Name, Configuration.GetKey("second-username"), session.ModHash);
            var list = Sub.GetBannedUsers(session, SubRedditToTestModWith);

            if (list.Where(u => u.Name == Configuration.GetKey("second-username")).Count() != 1)
                Assert.Fail("THe user was not banned");

            var user = User.Get(session, Configuration.GetKey("second-username"));
            Sub.UnBanUser(session, SubRedditToTestModWith, sub.Name, "t2_" + user.ID, session.ModHash);
            list = Sub.GetBannedUsers(session, SubRedditToTestModWith);

            if (list.Where(u => u.Name == Configuration.GetKey("second-username")).Count() != 0)
                Assert.Fail("The user was not unbanned");

        }

        [TestMethod]
        public void GrantFlair_DeleteFlair()
        {
            Assert.Fail("Not yet implemented");
        }
    }
}
