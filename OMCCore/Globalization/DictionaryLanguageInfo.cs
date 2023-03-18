using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace OMCCore.Globalization
{
    public class DictionaryLanguageInfo:ILanguageInfo
    {
        public DictionaryLanguageInfo(string id)
        {
            Id = id;
        }
        public Dictionary<string, string> KeyValuePairs { get; } = new Dictionary<string, string>();

        public string Id { get; set; }

        public virtual string? GetString(string key)
        {
            if (KeyValuePairs.ContainsKey(key)) return KeyValuePairs[key];
            return null;
        }
        public static DictionaryLanguageInfo FromJson(JObject json,string id)
        {
            var info = new DictionaryLanguageInfo(id);
            foreach (var kv in json)
            {
                info.KeyValuePairs[kv.Key] = kv.Value?.ToString() ?? kv.Key;
            }
            return info;
        }
    }
}
