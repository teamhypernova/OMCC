using EDGW.Logging;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OMCC.Plugins.GameManager
{
    public sealed class ReplacementArgument : StringArgument
    {
        static Logger logger = new Logger("ArgumentBuilder", nameof(ReplacementArgument));
        public static Regex Regex { get; } = new Regex(@"\$\{([\S]+)\}");
        public ReplacementArgument(string value) : base(value)
        {
        }
        public override string[] ToString(MinecraftSettings settings)
        {
            List<string> res = new List<string>();
            foreach(var value in Value.Split(' '))
            {
                res.Add(Regex.Replace(value, (m) =>
                {
                    var key = m.Groups[1].Value;
                    if (settings.Dictionary.ContainsKey(key))
                    {
                        var s = settings.Dictionary[key];
                        if (s != null)
                        {
                            if (s.Contains(" "))
                            {
                                s = $"\"{s}\"";
                            }
                        }
                        return s ?? "\"\"";
                    }
                    else
                    {
                        logger.error($"Cannot replace argument {key} :null.");
                        return key;
                    }
                }));
            }
            return res.ToArray();
        }
    }
}
