using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace OMCC.Plugins.GameManager
{
    public interface IArgument
    {
        public string[] ToString(MinecraftSettings settings);
        public static List<IArgument> ParseList(JArray arr)
        {
            var result = new List<IArgument>();
            foreach(var t in arr)
            {
                result.Add(Parse(t));
            }
            return result;
        }
        public static IArgument Parse(JToken token)
        {
            if(token is JObject obj)
            {
                var rulesarr = obj["rules"] as JArray;
                if (rulesarr != null)
                {
                    RuledArgument args = new RuledArgument();
                    args.Rules = Rules.Parse(rulesarr);
                    if (obj["value"] is JArray arr)
                    {
                        foreach(var t in arr)
                        {
                            args.Arguments.Add(Parse(t));
                        }
                    }
                    else if(obj["value"] is JValue val)
                    {
                        args.Arguments.Add(Parse(val));
                    }
                    else
                    {
                        throw new ArgumentException("token[value] must be an array or a value.");
                    }
                    return args;
                }
                else
                {
                    throw new ArgumentException("'token[rules]' must be an array");
                }
            }
            else
            {
                var s = token.ToString();
                if (ReplacementArgument.Regex.IsMatch(s))
                {
                    return new ReplacementArgument(s);
                }
                else
                {
                    return new StringArgument(s);
                }
            }
        }
    }
}
