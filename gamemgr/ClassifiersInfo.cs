using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace OMCC.Plugins.GameManager
{
    public class ClassifiersInfo
    {
        public ClassifiersInfo(Dictionary<string, LibraryDownloadInfo> classifiers, Dictionary<string, string> natives)
        {
            Classifiers = classifiers;
            Natives = natives;
        }

        public Dictionary<string, LibraryDownloadInfo> Classifiers { get; set; }
        public Dictionary<string,string> Natives { get; set; }
        public LibraryDownloadInfo? GetWindows()
        {
            if (Natives.ContainsKey("windows"))
            {
                if (Classifiers.ContainsKey(Natives["windows"]))
                {
                    return Classifiers[Natives["windows"]];
                }
            }
            return null;
        }
        public static ClassifiersInfo? Parse(JObject json)
        {
            JToken? cls = json["downloads"]?["classifiers"], n = json["natives"];
            if (n != null && cls != null)
            {
                Dictionary<string, LibraryDownloadInfo> clfs = new();
                Dictionary<string, string> ntvs = new();
                foreach (var obj in cls as JObject ?? throw new ArgumentException("'json[downloads][classifiers]' must be a object."))
                {
                    clfs.Add(obj.Key, LibraryDownloadInfo.Parse(obj.Value as JObject ?? throw new ArgumentException($"'json[downloads][classifiers][{obj.Key}]' must be a object.")));
                }
                foreach(var nv in n as JObject ?? throw new ArgumentException("'json[natives]' must be a object."))
                {
                    ntvs[nv.Key] = nv.Value?.ToString() ?? throw new ArgumentNullException($"json[natives][{nv.Key}]");
                }
                return new ClassifiersInfo(clfs, ntvs);
            }
            return null;
        }
    }
}
