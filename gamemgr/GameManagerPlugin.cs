using Newtonsoft.Json.Linq;
using OMCC.Plugins.GameManager.Resources;
using OMCCore.Core.Game;
using OMCCore.Globalization;
using OMCCore.Plugins;
using System.IO;

namespace OMCC.Plugins.GameManager
{
    public class GameManagerPlugin : Plugin
    {
        private GameManagerPlugin() { }
        public static GameManagerPlugin Current { get; } = new GameManagerPlugin();
        public override void OnRegisteringLanguagePack()
        {
            var lanpack = new LanguagePack();
            lanpack.Languages.Add("zh_cn", DictionaryLanguageInfo.FromJson(JObject.Parse(Lan.zh_cn), "zh_cn"));
            Globalization.AddLanguagePack(lanpack);
        }
        public override void OnRegistering()
        {
            var mems = new MemoryStream(Resources.Resources.Newtonsoft_Json_Schema);
            ExternHelper.AppaDominLoadlibrary(mems);
            GameRegistry.Current.Register(GameManager.Current);
        }
    }
}
