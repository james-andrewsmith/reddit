using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.reddit.api
{
    public sealed class PostListing : List<Post>
    {
        #region // Properties //
        public string Before
        {
            get;
            set;
        }

        public string After
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
