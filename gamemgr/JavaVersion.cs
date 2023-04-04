using Newtonsoft.Json.Linq;
using System;

namespace OMCC.Plugins.GameManager
{
    public class JavaVersion
    {
        public JavaVersion(Version max, Version min)
        {
            MaxVersion = max;
            MinVersion = min;
        }
        public bool IsMatch(Version version)
        {
            return (version >= MinVersion && version <= MaxVersion);
        }
        public Version MaxVersion { get; set; }
        public Version MinVersion { get; set; }
        public static JavaVersion Parse(JObject json)
        {
            int v = int.Parse((json["majorVersion"] ?? throw new ArgumentNullException("json[majorVersion]")).ToString());
            return new JavaVersion(new Version(999, 999, 999, 999), new Version(1, v));
        }
    }
}
