using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace OMCC.Plugins.GameManager
{
    public class Rules : List<Rule>
    {
        public bool IsAllow(MinecraftFeatures features)
        {
            RuleAction? act = null;
            foreach(var rule in this)
            {
                if (rule.IsMatch(features))
                {
                    if (act == null)
                    {
                        act = rule.Action;
                    }
                    else
                    {
                        act &= rule.Action;
                    }
                }
            }
            return act == RuleAction.Allow;
        }
        public static Rules Parse(JArray json)
        {
            Rules rules = new Rules();
            foreach(JObject obj in json)
            {
                rules.Add(Rule.Parse(obj));
            }
            return rules;
        }
    }
}
