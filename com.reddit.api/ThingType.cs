using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.reddit.api
{
    /// <summary>
    /// A base-36 id of the form t[0-9]+_[a-z0-9]+ (e.g. t3_6nw57) that reddit associates with every Thing. In the example, the type prefix t3_ specifies that the fullname is for a Link, and the id 6nw57 specifies the Link's id36. The type IDs may vary among different reddit clones, but here are the possible values for reddit.com.
    /// </summary>
    internal enum ThingType
    {
        Comment = 1,
        Account = 2,
        Link = 3,
        Message = 4,
        SubReddit = 5
    }
}
