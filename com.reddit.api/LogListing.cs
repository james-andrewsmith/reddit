using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

using NSoup;

namespace com.reddit.api
{
    public sealed class LogListing : List<Log>
    {

        internal static LogListing FromHtml(string html)
        {
            var list = new LogListing();
            var client = NSoup.NSoupClient.Parse(html);
            var entries = client.Select("#siteTable table tr.modactions");

            foreach (var entry in entries)
            {
                // entry.Select(
                list.Add(new Log
                {
                    Timestamp = Convert.ToDateTime(entry.Select("td.timestamp time").Attr("datetime")),
                    Moderator = entry.Select("td a").First().Text(),
                    Action = entry.Select("td.button a").Attr("class").Replace("modactions", string.Empty).Trim(),
                    Description = entry.Select("td.description").Text
                }); 
            }
            // iden = client.Select("#compose-message input[name=iden]").Val();
            // captcha = client.Select("#compose-message img.capimage").Attr("src");

            return list;
        }

        internal static LogListing FromJson(JToken token)
        {
            return new LogListing
            {

            };
        }

    }
}
