﻿using System;
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
            User.GetSaved(session); 
        }

        [TestMethod]
        public void ListSaved()
        {
            // login using regular creds
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));

            var posts = User.GetSaved(session);
            Assert.IsTrue(posts.Count > 0);
        }

        [TestMethod]
        public void ListComments()
        {

        }
    }
}
