using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.reddit.api
{
    public sealed class Log
    {
        public DateTime Timestamp
        {
            get;
            set;
        }

        public string Moderator
        {
            get;
            set;
        }

        public string Action
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }
    }
}
