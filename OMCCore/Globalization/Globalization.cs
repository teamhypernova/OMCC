using CommunityToolkit.Mvvm.Messaging;
using EDGW.Logging;
using Newtonsoft.Json.Linq;
using OMCCore.Model.Data;
using System;
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
        public static Logger logger = new Logger("Globalization", nameof(Globalization));
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
                var lanInfos = languages[SelectedLanguage];
                foreach(var lanInfo in lanInfos)
                {
                    var lanString = lanInfo.GetString(key);
                    if (null != lanString)
                    {
                        return lanString;
                    }
                }
                logger.error($"Cannot translate key \"{key ?? "null"}\".");
                return key;
            }
        }
        public static string GetString(string key, params string[] parameters)
        {
            lock (SelectedLanguage)
            {
                try
                {
                    if (!languages.ContainsKey(SelectedLanguage)) return key;
                    var lanInfos = languages[SelectedLanguage];
                    foreach (var lanInfo in lanInfos)
                    {
                        var lanString = lanInfo.GetString(key);
                        if (null != lanString)
                        {
                            return string.Format(lanString, parameters);
                        }
                    }
                }
                catch(Exception ex)
                {
                    logger.error($"Cannot translate key \"{key ?? "null"}\".", ex);
                    return key;
                }
                logger.error($"Cannot translate key \"{key ?? "null"}\".");
                return key;
            }
        }
    }
}
