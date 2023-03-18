using EDGW.Logging;
using OMCCore.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMCCore.Core.User
{
    public interface IImmediateUser
    {
        public string NameImmediate { get; }
        public string UuidImmediate { get; }
        public string TokenImmediate { get; }
        public Text UserType { get; }
    }
    public interface IUserManager
    {
        public string Id { get; }
        public Text Name { get; }
        public IImmediateUser[] GetUsers();
    }
    public static class Users
    {
        static Logger logger = new Logger("Users Core Mgr", "ucm-8dsah72dbsaygdsah82");
        static Dictionary<string, IUserManager> Usermgrs { get; } = new Dictionary<string, IUserManager>();
        public static void RegisterUserManager(IUserManager mgr)
        {
            Usermgrs[mgr.Id] = mgr;
            logger.info($"Registered UserManager({mgr.Id})");
        }
    }
}
