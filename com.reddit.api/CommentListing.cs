using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.reddit.api
{
    public sealed class CommentListing : List<Comment>
    {
        #region // Properties //
        public string After
        {
            get;
            set;
        }

        public string Before
        {
            get;
            set;
        }

        public string ModHash
        {
            get;
            set;
        }
        #endregion
    }
}
