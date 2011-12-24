using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.reddit.api
{
    public sealed class CommentListing : List<Comment>
    {

        #region // Constructor //
        public CommentListing()
        {
            More = new List<string>();
        }
        #endregion

        #region // Properties //

        /// <summary>
        /// 
        /// </summary>
        public List<string> More
        {
            get;
            set;
        }

        public bool HasMore
        {
            get
            {
                return More.Count > 0;
            }
        }

        public string ModHash
        {
            get;
            set;
        }
        #endregion
    }
}
