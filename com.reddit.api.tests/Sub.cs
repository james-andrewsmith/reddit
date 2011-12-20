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
        
        [TestMethod]
        public void GetMine()
        {

            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));
            var subs = Sub.GetMine(session);


            Assert.IsTrue(subs.Count > 0);

        }
    }
}
