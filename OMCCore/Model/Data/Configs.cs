using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OMCCore.Model.Data
{
    public static class Configs
    {
        static Dictionary<string, ConfigFile> ConfigDic { get; } = new Dictionary<string, ConfigFile>();
        public static T GetConfig<T>()
            where T : ConfigFile
        {
            return GetConfig<T>("Configs\\" + typeof(T).FullName);
        }
        public static T GetConfig<T>(string filepath)
            where T : ConfigFile
        {
            lock (ConfigDic)
            {

                if (ConfigDic.ContainsKey(filepath))
                {
                    return (T)ConfigDic[filepath];
                }
                else
                {
                    try
                    {
                        var t = JsonConvert.DeserializeObject<T>(ConfigFileManager.ReadJson(filepath));
                        ConfigDic[filepath] = t;
                        return t;
                    }catch(Exception ex)
                    {
                        return default(T);
                    }
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
