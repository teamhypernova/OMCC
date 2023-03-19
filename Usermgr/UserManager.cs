using EDGW.Logging;
using Newtonsoft.Json.Linq;
using OMCCore.Core.User;
using OMCCore.Cryptography;
using OMCCore.Globalization;
using OMCCore.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMCC.Plugins.UserManager
{
    public class UserManager : IUserManager
    {
        public UserManager()
        {

        }
        public static Logger logger = new Logger("Official UserManager", "officialusermgr");
        public string Id => "plugin.official.usermgr";

        public Text Name => new Text("plugin.official.usermgr.class.name");
        public void AddUser(User user)
        {
            var key = GetKey();
            var iv = GetIv();
            var config = Configs.GetConfig<UserManagerConfig>();
            JObject obj = new JObject();
            obj["id"] = user.Type.Id;
            obj["data"] = user.Serialize();
            if (!user.IsCrypted)
            {
                config.UserObjects.Add(obj);
            }
            else
            {
                JObject crypto = new JObject();
                string crycon = DES.Encrypt(obj.ToString(), GetKey(), GetIv());
                crypto["crypted"] = crycon;
                config.UserObjects.Add(crypto);
            }
            Configs.SaveAll();

        }
        string GetKey() => Base64.EncodeBase64(Hash.CalcShax512(Encoding.UTF8.GetBytes(Configs.GetConfig<UserManagerConfig>().CryptoKey.ToString()))).PadRight(8).Substring(0, 8);
        string GetIv() => Base64.EncodeBase64(Hash.CalcMd5x16(Encoding.UTF8.GetBytes(Configs.GetConfig<UserManagerConfig>().CryptoKey.ToString()))).PadRight(8).Substring(0, 8);
        public IImmediateUser[] GetUsers()
        {
            var key = GetKey();
            var iv = GetIv();
            var result = new List<IImmediateUser>();
            var config = Configs.GetConfig<UserManagerConfig>();
            foreach (var prop in config.UserObjects)
            {
                try
                {
                    JObject obj = (JObject?)prop.Value ?? new JObject();
                    var id = obj["id"];
                    if (id != null)
                    {
                        var u = GetUser(obj);
                        if (u != null)
                        {
                            result.Add(u);
                        }
                    }
                    else
                    {
                        var cryp = obj["crypted"]?.ToString() ?? "";
                        cryp = DES.Decrypt(cryp, key, iv);
                        var u = GetUser(JObject.Parse(cryp));
                        if (u != null)
                        {
                            result.Add(u);
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.error("Cannot load user object.\n" + prop.Value?.ToString() ?? "", ex);
                }
            }
            return result.ToArray();
        }
        public IImmediateUser? GetUser(JObject obj)
        {
            string typeid = obj["id"]?.ToString() ?? "";
            if (UserTypes.ContainsKey(typeid))
            {
                return UserTypes[typeid].Parse(obj["data"] as JObject ?? new JObject());
            }
            else
            {
                logger.error($"Unknown UserType({typeid})");
                return null;
            }
        }
        static UserManager()
        {
            RegisterUserType(OfflineUserType.OFFLINE);
        }
        static Dictionary<string, UserType> UserTypes { get; } = new Dictionary<string, UserType>();
        public static void RegisterUserType(UserType type)
        {
            UserTypes[type.Id] = type;
            logger.info($"Registered UserType({type.Id})");
        }

        public IImmediateUser GetSelectedUser()
        {
            throw new NotImplementedException();
        }
    }
}
