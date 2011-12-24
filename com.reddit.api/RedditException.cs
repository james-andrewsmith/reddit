using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.reddit.api
{
    public sealed class RedditException : Exception
    {
        public RedditException(string message)
            : base(message)
        { }
    }
}
