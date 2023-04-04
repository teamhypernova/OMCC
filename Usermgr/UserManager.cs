using CommunityToolkit.Mvvm.Messaging;
using EDGW.Logging;
using Newtonsoft.Json.Linq;
using OMCC.Plugins.UserManager.UI.Add;
using OMCCore.Core;
using OMCCore.Core.User;
using OMCCore.Cryptography;
using OMCCore.Globalization;
using OMCCore.Model.Data;
using OMCCore.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMCC.Plugins.UserManager
{
    public class UserManager : Registry<UserType>, IUserManager , IUISupported , IAddSupported
    {
        public static UserManager Current { get; } = new UserManager();
        private UserManager()
        {

            Register(OfflineUserType.OFFLINE);
            Register(OfflineUserType.OFFLINE2);
        }
        protected override Logger Logger { get; } = new Logger("Official UserManager", "officialusermgr");
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
            WeakReferenceMessenger.Default.Send(new SelectedUserChangedMessage());

        }
        string GetKey() => Base64.EncodeBase64(Hash.CalcShax512(Encoding.UTF8.GetBytes(Configs.GetConfig<UserManagerConfig>().CryptoKey.ToString()))).PadRight(8).Substring(0, 8);
        string GetIv() => Base64.EncodeBase64(Hash.CalcMd5x16(Encoding.UTF8.GetBytes(Configs.GetConfig<UserManagerConfig>().CryptoKey.ToString()))).PadRight(8).Substring(0, 8);
        public IImmediateUser[] GetUsers()
        {
            var key = GetKey();
            var iv = GetIv();
            var result = new List<IImmediateUser>();
            var config = Configs.GetConfig<UserManagerConfig>();
            foreach (JObject obj in config.UserObjects)
            {
                try
                {
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
                    Logger.error("Cannot load user object.\n" + obj?.ToString() ?? "", ex);
                }
            }
            return result.ToArray();
        }
        public IImmediateUser? GetUser(JObject obj)
        {
            string typeid = obj["id"]?.ToString() ?? "";
            if (ValueDic.ContainsKey(typeid))
            {
                return ValueDic[typeid].Parse(obj["data"] as JObject ?? new JObject());
            }
            else
            {
                Logger.error($"Unknown UserType({typeid})");
                return null;
            }
        }

        public IImmediateUser? GetSelectedUser()
        {
            var config = Configs.GetConfig<UserManagerConfig>();
            var users = GetUsers();
            foreach (var u in users)
            {
                if(u is User us)
                {
                    if (us.UniqueName == config.SelectedId)
                    {
                        return us;
                    }
                }
            }
            return users.FirstOrDefault(defaultValue: null);
        }

        OPage IUISupported.CreatePage()
        {
            return new UI.UserManager();
        }

        (OPage page, bool createWindow) IAddSupported.CreatePage()
        {
            return (new AddUserPage(), true);
        }
    }
}
