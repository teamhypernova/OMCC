using System.Collections.Generic;
using System.Linq;

namespace OMCC.Plugins.GameManager
{
    public class RuledArgument : IArgument
    {
        public Rules Rules { get; set; } = new();
        public List<IArgument> Arguments { get; } = new();
        public string[] ToString(MinecraftSettings settings)
        {
            if (Rules.IsAllow(settings.Features))
            {
                return Arguments.SelectMany(x => x.ToString(settings)).ToArray();
            }
            else
            {
                return new string[0];
            }
        }
    }
}
