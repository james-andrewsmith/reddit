using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.reddit.api
{
    public sealed class FlairListing : List<Flair>
    {
        public string Next
        {
            get;
            set;
        }

        public string Prev
        {
            get;
            set;
        }
    }
}
