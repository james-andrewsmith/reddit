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
        [TestMethod]
        public void Submit()
        {
            // login
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));

            var post = new Post
            {
                
            };

            // attempt to save
            Post.Submit(session, post);
        }

        [TestMethod]
        public void Vote_UpDownNull()
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
            foreach(var post in list)
            {
                id = post.ID;
                break;
            }            

            // vote it up
            Post.VoteUp(session, id);

            // vote it down
            Post.VoteDown(session, id);

            // vote it null
            Post.VoteNull(session, id);

        }

        [TestMethod]
        public void Hide_UnHide()
        {

        }

        [TestMethod]
        public void Save_UnSave()
        {
            
            

        }

        // Not testing this because we don't want to report anyone!
        // [TestMethod]
        // public void Report()
        // {
        // 
        // }
    }
}
