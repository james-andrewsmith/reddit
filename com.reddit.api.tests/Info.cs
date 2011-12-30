using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.reddit.api.tests
{
    [TestClass]
    public class InfoTestClass
    {
        [TestMethod]
        public void GetInfo_Imgur()
        {
            // login using regular creds
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));

            var posts = Info.FromUrl(session, "http://imgur.com/");

            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count > 0);
        }

        [TestMethod]
        public void GetInfo_Github()
        {
            // login using regular creds
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));

            var posts = Info.FromUrl(session, "https://github.com/");

            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count > 0);
        }
    }
}
