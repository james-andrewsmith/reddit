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
            Assert.Fail();
        }

        [TestMethod]
        public void GetSent()
        {
            Assert.Fail();
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
                To = "pressf12",
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
