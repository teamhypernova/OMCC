using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMCCore.Globalization
{
    public class DictionaryLanguageInfo : ILanguageInfo
    {
        public DictionaryLanguageInfo(string id)
        {
            Id = id;
        }
        public Dictionary<string, string> KeyValuePairs { get; } = new Dictionary<string, string>();

        public string Id { get; set; }

        public virtual string? GetString(string key)
        {
            if (key != null)
            {
                if (KeyValuePairs.ContainsKey(key)) return KeyValuePairs[key];
            }
            return null;
        }
        public static DictionaryLanguageInfo FromJson(JObject json, string id)
        {
            var info = new DictionaryLanguageInfo(id);
            foreach (var kv in GetKeys(json))
            {
                info.KeyValuePairs[kv.key] = kv.value;
            }
            return info;
        }
        public static List<(string key, string value)> GetKeys(JObject json, string prev = "")
        {
            var result = new List<(string key, string value)>();
            foreach (var kv in json)
            {
                string key = kv.Key;
                if (prev != "") key = prev + "." + key;
                if (kv.Value?.Type == JTokenType.Object)
                {
                    foreach (var c in GetKeys((JObject)kv.Value, key))
                    {
                        result.Add(c);
                    }
                }
                else
                {
                    result.Add((key, kv.Value?.ToString() ?? kv.Key));
                }
            }
            return result;
        }
    }
}
