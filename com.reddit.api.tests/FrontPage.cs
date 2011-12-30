using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.reddit.api.tests
{
    [TestClass]
    public class FrontPageTestClass
    {
        [TestMethod]
        public void ListFrontPageStories()
        {
            // login using regular creds
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));

            // Get posts from the front page
            var posts = FrontPage.List(session);

            // ensure we have posts on the front page
            Assert.IsTrue(posts.Count > 0);
        }

    }
}
