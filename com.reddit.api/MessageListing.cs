using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.reddit.api
{
    public sealed class MessageListing
    {

        #region // Properties //
        public string ModHash
        {
            get;
            set;
        }

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
        #endregion 


    }
}
