using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.reddit.api
{
    /// <summary>
    /// Stores the auth cookie
    /// </summary>
    public sealed class Session
    {
        public string Username
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string ModHash
        {
            get;
            set;
        }

        public string Cookie
        {
            get;
            set;
        }

    }
}
