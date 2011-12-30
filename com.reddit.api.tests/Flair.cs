using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.reddit.api.tests
{
    [TestClass]
    public class FlairTestClass
    {
        /// <summary>
        /// This is the test reddit for this SDK
        /// </summary>
        private string SubRedditToTestModWith = Configuration.GetKey("moderated-subreddit");

        [TestMethod]
        public void GetFlairForSub()
        {
            // login using regular creds
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));

            // get the flair for a moderated subreddit? (why is this not public?)
            var flairs = Flair.GetFlair(session, SubRedditToTestModWith);

            // check there is some flair
            Assert.IsNotNull(flairs);
            Assert.IsTrue(flairs.Count > 0);
        }
    }
}
