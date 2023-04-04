using OMCC.Plugins.GameManager;
using OMCCore.Globalization;
using OMCCore.Plugins;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OMCC.Plugins
{
    public class Descriptor : IDescriptor
    {
        public Text Name => new("plugin.official.gamemgr.name");

        public Text Description => new("plugin.official.gamemgr.desc");

        public Plugin Plugin { get; } = GameManagerPlugin.Current;

        public string Id => "plugin.official.gamemgr";
    }
}
