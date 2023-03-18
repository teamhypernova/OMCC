using CommunityToolkit.Mvvm.Messaging;
using Newtonsoft.Json.Linq;
using OMCCore.Model.Data;
using System.Collections.Generic;

namespace OMCCore.Globalization
{
    public static class Globalization
    {
        static Globalization()
        {
            LanguagePack def = new LanguagePack();
            def.Languages.Add("zh_cn", DictionaryLanguageInfo.FromJson(JObject.Parse(Resources.Languages.zh_cn), "zh_cn"));
            AddLanguagePack(def);
        }

        static Dictionary<string, List<ILanguageInfo>> languages { get; } = new ();
        public static void AddLanguagePack(LanguagePack pack)
        {
            foreach(var language in pack.Languages)
            {
                if (!languages.ContainsKey(language.Key)) languages[language.Key] = new List<ILanguageInfo>();
                languages[language.Key].Add(language.Value);
            }
        }
        public static string SelectedLanguage
        {
            get => Configs.GetConfig<GlobalizationConfig>().SelectedLanguage; 
            set
            {
                Configs.GetConfig<GlobalizationConfig>().SelectedLanguage = value;
                WeakReferenceMessenger.Default.Send(new SelectedLanguageChangedMessage());
            }
        }
        public static string GetString(string key)
        {
            lock (SelectedLanguage)
            {
                if (!languages.ContainsKey(SelectedLanguage)) return key;
                var lans = languages[SelectedLanguage];
                foreach(var lan in lans)
                {
                    var s = lan.GetString(key);
                    if (null != s)
                    {
                        return s;
                    }
                }
                return key;
            }
        }
    }
}
