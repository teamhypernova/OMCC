using OMCC.Plugins.UserManager;
using OMCCore.Globalization;
using OMCCore.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMCC.Plugins
{
    public class Descriptor : OMCCore.Plugins.IDescriptor
    {
        public Text Name => new Text("plugin.official.usermgr.name");

        public string Id => "plugin.official.usermgr";

        public Text Description => new Text("plugin.official.usermgr.desc");

        public Plugin Plugin { get; } = new UserManagerPlugin();
    }
}
