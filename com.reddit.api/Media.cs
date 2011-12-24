using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.reddit.api
{
    public sealed class Media
    {
        /*
                    "type": "youtube.com",
                    "oembed": {
                        "provider_url": "http://www.youtube.com/",
                        "description": "Sneak preview of dark knight rises movie I dont own any copyright laws to this movie",
                        "title": "Beginning of Batman",
                        "url": "http://www.youtube.com/watch?v=qz1SL0W5kLc",
                        "author_name": "thebeastlyt",
                        "height": 338,
                        "width": 600,
                        "html": "&lt;iframe width=\"600\" height=\"338\" src=\"http://www.youtube.com/embed/qz1SL0W5kLc?fs=1&amp;feature=oembed\" frameborder=\"0\" allowfullscreen&gt;&lt;/iframe&gt;",
                        "thumbnail_width": 480,
                        "version": "1.0",
                        "provider_name": "YouTube",
                        "thumbnail_url": "http://i2.ytimg.com/vi/qz1SL0W5kLc/hqdefault.jpg",
                        "type": "video",
                        "thumbnail_height": 360,
                        "author_url": "http://www.youtube.com/user/thebeastlyt"
                    }
        */

        #region // Properties // 

        public string EmbedType
        {
            get;
            set;
        }

        public string ProviderUrl
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Url
        {
            get;
            set;
        }

        public string AuthorName
        {
            get;
            set;
        }

        public int Height
        {
            get;
            set;
        }

        public string Width
        {
            get;
            set;
        }

        public string Html
        {
            get;
            set;
        }

        public int ThumbWidth
        {
            get;
            set;
        }

        public string Version
        {
            get;
            set;
        }

        public string ProviderName
        {
            get;
            set;
        }

        public string ThurbnailUrl
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public int ThumbnailHeight
        {
            get;
            set;
        }

        #endregion 

    }
}
