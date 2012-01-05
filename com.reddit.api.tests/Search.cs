using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.reddit.api.tests
{
    [TestClass]
    public class SearchTestClass
    {
        [TestMethod]
        public void BasicSearchQuery()
        {
            // login using regular creds
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));

            // all logic is within the search query really...

            var posts = Search.Query(session, "IAMA");
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count > 0);
        }
    }
}
