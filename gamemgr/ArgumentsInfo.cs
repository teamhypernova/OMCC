using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace OMCC.Plugins.GameManager
{
    public class ArgumentsInfo
    {
        private ArgumentsInfo()
        {

        }

        public ArgumentsInfo(List<IArgument> java, List<IArgument>? game, IArgument? additional)
        {
            Java = java;
            Game = game;
            Additional = additional;
        }

        public List<IArgument> Java { get; set; }
        public List<IArgument>? Game { get; set; }
        public IArgument? Additional { get; set; }
        public static ArgumentsInfo Parse(JObject verjson)
        {
            ArgumentsInfo info = new();
            var j = verjson["arguments"]?["jvm"] as JArray;
            var g = verjson["arguments"]?["game"] as JArray;
            var a = verjson["minecraftArguments"];
            if (j != null)
            {
                info.Java = IArgument.ParseList(j);
            }
            else
            {
                info.Java = IArgument.ParseList(JArray.Parse(Resources.Resources.defjvmarg));
            }
            if (g != null)
            {
                info.Game = IArgument.ParseList(g);
            }
            if (a != null)
            {
                info.Additional = IArgument.Parse(a);
            }
            return info;
        }
    }
}
