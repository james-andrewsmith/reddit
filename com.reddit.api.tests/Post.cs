using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.reddit.api.tests
{
    [TestClass]
    public class PostTestClass
    {
        /// <summary>
        /// This is the test reddit for this SDK
        /// </summary>
        private string SubRedditToTestModWith = Configuration.GetKey("moderated-subreddit");

        [TestMethod]
        public void Submit_Link()
        {
            // login
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));

            var post = new Post
            {
                Url = "https://github.com/pressf12/reddit",
                Title = "A fully featured C# reddit API client",
                SubReddit = "csharpredditclient"
            };

            // attempt to save
            Post.Submit(session, post, PostKind.Link);
        }

        [TestMethod]
        public void Submit_Self()
        {
            // login
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));

            var post = new Post
            {
                SelfText = "This post was generated as part of the unit testing for the " +
                           "C# client available here: https://github.com/pressf12/reddit",
                Title = "IAMA fully featured C# reddit API client, AMA.",
                SubReddit = "csharpredditclient"
            };

            // attempt to save
            Post.Submit(session, post, PostKind.Self);
        }

        [TestMethod]
        public void Vote_UpDownNull()
        {
            // login
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));

            // get a popular sub
            var list = Sub.GetListing(session, SubRedditToTestModWith);

            // no modhash
            if (string.IsNullOrEmpty(list.ModHash))
                Assert.Fail("No modhash");

            // no posts
            if (list.Count == 0)
                Assert.Fail("No items to vote on");

            // find the first story with no votes either way
            var id = string.Empty;
            foreach(var post in list)
            {
                id = post.ID;
                break;
            }            

            // vote it up
            Post.VoteUp(session, id, list.ModHash);

            // vote it down
            Post.VoteDown(session, id, list.ModHash);

            // vote it null
            Post.VoteNull(session, id, list.ModHash);

        }

        [TestMethod]
        public void Hide_UnHide()
        {
            // login
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));

            // get a popular sub
            var list = Sub.GetListing(session, "pics");
            session.ModHash = list.ModHash;

            // no modhash
            if (string.IsNullOrEmpty(list.ModHash))
                Assert.Fail();

            // no posts
            if (list.Count == 0)
                Assert.Fail();

            // find the first story with no votes either way
            var id = string.Empty;
            foreach (var post in list)
            {
                id = post.ID;
                break;
            }

            Post.Hide(session, id, list.ModHash);

            Post.UnHide(session, id, list.ModHash);

        }

        [TestMethod]
        public void Save_UnSave()
        {
            // login
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));

            // get a popular sub
            var list = Sub.GetListing(session, "pics");
            
            // no modhash
            if (string.IsNullOrEmpty(list.ModHash))
                Assert.Fail();

            // no posts
            if (list.Count == 0)
                Assert.Fail();

            // find the first story with no votes either way
            var id = string.Empty;
            foreach (var post in list)
            {
                id = post.ID;
                break;
            }

            Post.Save(session, id, list.ModHash);

            Post.UnSave(session, id, list.ModHash);
           
        }


        [TestMethod]
        public void Nsfw_Mark_Unmark()
        {
            // login
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));

            // get a popular sub
            var list = Sub.GetListing(session, SubRedditToTestModWith);

            // no modhash
            if (string.IsNullOrEmpty(list.ModHash))
                Assert.Fail("No modhash for this reddit");

            // no posts
            if (list.Count == 0)
                Assert.Fail("No items in subreddit");

            // find the first story with no votes either way
            var id = string.Empty;
            foreach (var post in list)
            {
                id = post.Name;
                break;
            }

            Post.Nsfw(session, SubRedditToTestModWith, id, list.ModHash);

            Post.UnNsfw(session, SubRedditToTestModWith, id, list.ModHash);

        }

        // Not testing this because we don't want to report anyone!
        // [TestMethod]
        // public void Report()
        // {
        // 
        // }
    }
}
