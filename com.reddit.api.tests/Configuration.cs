using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.reddit.api.tests
{
    internal class Configuration
    {
        private static System.Configuration.AppSettingsReader asr = new System.Configuration.AppSettingsReader();         
        internal static string GetKey(string key)
        {
            return Convert.ToString(asr.GetValue(key, typeof(string)));
        }
    }
}
