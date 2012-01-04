using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.reddit.api.tests
{
    [TestClass]
    public class MessageTestClass
    {

        [TestMethod]
        public void GetUnread()
        {
            // login using regular creds
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));
            var messages = Message.Unread(session);

            Assert.IsNotNull(messages);
            Assert.IsTrue(messages.Count > 0);
        }

        [TestMethod]
        public void GetSent()
        {
            // login using regular creds
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));
            var messages = Message.Sent(session);

            Assert.IsNotNull(messages);
            Assert.IsTrue(messages.Count > 0);
        }

        [TestMethod]
        public void MakeAsRead()
        {
            // login using regular creds
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));
            
            // get unread messages
            var unread = Message.Unread(session);
            
            // check if there are any unread messages
            if (unread.Count == 0)
                Assert.Fail("There are no unread messages so there is no way for us to test if we can mark messages as read");

            // mark the first one as unread
            Message.Read(session, unread[0].ID);
            
            // check we have less unread messages
            var unread2 = Message.Unread(session);

            // make sure there are less menus
            Assert.IsTrue(unread.Count > unread2.Count);
        }


        [TestMethod]
        public void GetSendForm()
        {
            // login using regular creds
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));

            string iden, captcha;
            Message.GetSendForm(session, out iden, out captcha);

            Assert.IsFalse(string.IsNullOrEmpty(iden));
            Assert.IsFalse(string.IsNullOrEmpty(captcha));
        }

        [TestMethod]
        public void SendMessage()
        {
            // login using regular creds
            var session = User.Login(Configuration.GetKey("username"), Configuration.GetKey("password"));

            string iden, captcha;
            Message.GetSendForm(session, out iden, out captcha);

            if (string.IsNullOrEmpty(iden) ||
                string.IsNullOrEmpty(captcha))
                Assert.Fail("Iden or Captcha not retreieved");

            var message = new Message
            {
                Destination = "pressf12",
                Subject = "Unit Test",
                Body = "Send from C# Client"
            };

            // to pass this test you must insert a breakpoint here
            // manually view the capture in this URL, then
            // overwrite the manual captcha variable with the text
            var captchaUrl = "http://www.reddit.com" + captcha;
            var manualCaptcha = "";
            if (string.IsNullOrEmpty(manualCaptcha))
                Assert.Fail("You have not viewed & then entered the captcha");

            Message.Send(session, message, iden, manualCaptcha);
        }
    }
}
