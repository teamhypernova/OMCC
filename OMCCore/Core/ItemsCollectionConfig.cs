using Newtonsoft.Json.Linq;
using OMCCore.Model.Data;
using System.Collections.Generic;
using System.Windows.Input;

namespace OMCCore.Core
{
    public sealed class ItemsCollectionConfig:ConfigFile
    {
        public Dictionary<string, List<string>> Values { get; set; } = new Dictionary<string, List<string>>();
        public void AddValue(string key, string value)
        {
            if (!Values.ContainsKey(key))
            {
                Values[key] = new List<string>();
            }
            Values[key].Add(value);
        }
        public bool RemoveValue(string key, string value)
        {
            if (!Values.ContainsKey(key))
            {
                Values[key] = new List<string>();
            }
            return Values[key].Remove(value);
        }
        public void InsertValue(string key, int index, string value)
        {
            if (!Values.ContainsKey(key))
            {
                Values[key] = new List<string>();
            }
            Values[key].Insert(index, value);
        }
        public void RemoveValueAt(string key,int index)
        {
            if (!Values.ContainsKey(key))
            {
                Values[key] = new List<string>();
            }
            Values[key].RemoveAt(index);
        }
        public void Clear(string key)
        {
            if (!Values.ContainsKey(key))
            {
                Values[key] = new List<string>();
            }
            Values[key].Clear();
        }
    }
}
