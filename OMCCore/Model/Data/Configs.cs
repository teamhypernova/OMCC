using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OMCCore.Model.Data
{
    public static class Configs
    {
        static Dictionary<Type, ConfigFile> ConfigDic { get; } = new Dictionary<Type, ConfigFile>();
        public static T GetConfig<T>()
            where T : ConfigFile
        {
            lock (ConfigDic)
            {

                if (ConfigDic.ContainsKey(typeof(T)))
                {
                    return (T)ConfigDic[typeof(T)];
                }
                else
                {
                    var t = JsonConvert.DeserializeObject<T>(ConfigFileManager.ReadJson("Configs\\" + typeof(T).FullName));
                    ConfigDic[typeof(T)] = t;
                    return t;
                }
            }
        }
        public static void SaveAll()
        {
            lock (ConfigDic)
            {
                foreach (var t in ConfigDic.Values)
                {
                    ConfigFileManager.WriteFile("Configs\\" + t.GetType().FullName, t.Serialize());
                }
                ConfigDic.Clear();
            }
        }
    }
}
