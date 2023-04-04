using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace OMCC.Plugins.GameManager
{
    public class Rule
    {
        public Rule(RuleAction action, Dictionary<string, bool>? features, OsInfo? os)
        {
            Action = action;
            Features = features;
            Os = os;
        }
        public bool IsMatch(MinecraftFeatures features)
        {
            if (Os != null)
            {
                if (!Os.IsMatch()) return false;
            }
            if (Features != null)
            {
                if (Features.ContainsKey("has_custom_resolution"))
                {
                    if (Features["has_custom_resolution"] != features.HasCustomResolution) return false;
                }
                if (Features.ContainsKey("is_demo_user"))
                {
                    if (Features["is_demo_user"] != features.IsDemoUser) return false;
                }
            }
            return true;
        }
        public RuleAction Action { get; set; }
        public Dictionary<string,bool>? Features { get; set; }
        public OsInfo? Os { get; set; }
        public static Rule Parse(JObject json)
        {
            var actionstr = json["action"]?.ToString() ?? throw new ArgumentNullException("json[action]");
            OsInfo? os = null;
            Dictionary<string, bool>? features = null;
            var action = actionstr == "allow" ? RuleAction.Allow : RuleAction.Disallow;
            if (json["os"] != null)
            {
                os = OsInfo.Parse(json["os"] as JObject ?? throw new ArgumentException("'os' must be a object.", "json[os]"));
            }
            if (json["features"] != null)
            {
                JObject obj = json["features"] as JObject ?? throw new ArgumentException("'features' must be a object.", "json[features]");
                features = new Dictionary<string, bool>();
                int count = 0;
                foreach(var prop in obj)
                {
                    var val = bool.Parse(prop.Value?.ToString()??throw new ArgumentNullException($"json[features][{count}]"));
                    features[prop.Key] = val;
                    count++;
                }
            }
            return new Rule(action, features, os);
        }
    }
}
