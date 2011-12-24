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
        [TestMethod]
        public void GetFlairForSub()
        {
            var sub = "pics";
            
            // login using regular creds
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));

            // get the flair
            var flairs = Flair.GetFlair(session, sub);

        }
    }
}
