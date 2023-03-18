using Newtonsoft.Json.Linq;
using OMCCore.Core.User;
using OMCCore.Globalization;
using OMCCore.Plugins;

namespace OMCC.Plugins.UserManager
{
    public class UserManagerPlugin : Plugin
    {
        public override void OnRegisteringLanguagePack()
        {
            LanguagePack def = new LanguagePack();
            def.Languages.Add("zh_cn", DictionaryLanguageInfo.FromJson(JObject.Parse(Resources.Languages.zh_cn), "zh_cn"));
            Globalization.AddLanguagePack(def);
        }
        public override void OnRegistering()
        {
            Users.RegisterUserManager(new UserManager());
        }
    }
}
