using EDGW.Logging;
using OMCCore.Model.Data;
using System;
using System.Linq;

namespace OMCCore.Core.User
{
    /// <summary>
    /// Supported Extensions:
    /// IUISupported
    /// IAddSupported
    /// </summary>
    public class UserRegistry : Selector<IUserManager>
    {
        private UserRegistry() { }
        public static UserRegistry Current { get; } = new UserRegistry();
        protected override Logger Logger => new Logger("Users Core Mgr", "ucm-8dsah72dbsaygdsah82");
        public UserInfo CreateUserInfo()
        {
            //todo:
            return new UserInfo("Steve", "uuid", "offline", "token", null);
        }
    }
}
