using Newtonsoft.Json.Linq;
using OMCCore.Globalization;
using System;

namespace OMCC.Plugins.UserManager
{
    public sealed class OfflineUserType:UserType
    {
        public static OfflineUserType OFFLINE = new OfflineUserType();

        public override Text Name => new Text("plugin.official.usermgr.offlineuser.name");

        public override string Id => "offline";

        public override User Parse(JObject data)
        {
            var name = data["name"];
            if (name != null)
            {
                return new OfflineUser(name.ToString());
            }
            else
            {
                throw new ArgumentNullException(nameof(name));
            }
        }
    }
}
