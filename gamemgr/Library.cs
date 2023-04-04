using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace OMCC.Plugins.GameManager
{
    public class Library : IMinecraftBased
    {
        public Library(MinecraftVersion version, PackageName name, Rules? rules, LibraryDownloadInfo? artifact, ClassifiersInfo? classifiers)
        {
            Version = version;
            Name = name;
            Rules = rules;
            Artifact = artifact;
            Classifiers = classifiers;
        }

        public MinecraftVersion Version { get; set; }
        public PackageName Name { get; set; }
        public Rules? Rules { get; set; }
        public LibraryDownloadInfo? Artifact { get; set; }
        public ClassifiersInfo? Classifiers { get; set; }
        public static Library Parse(MinecraftVersion version, JObject obj)
        {
            PackageName name = PackageName.Parse(obj["name"]?.ToString() ?? throw new ArgumentNullException("json[name]"));
            Rules? rules = null;
            LibraryDownloadInfo? artifact = null;
            if (obj["rules"] != null)
            {
                rules = Rules.Parse(obj["rules"] as JArray ?? throw new ArgumentException("'json[rules]' must be an array."));
            }
            if (obj["downloads"]?["artifact"] != null)
            {
                artifact = LibraryDownloadInfo.Parse(obj["downloads"]?["artifact"] as JObject ?? throw new ArgumentException("'json[downloads][artifact]' must be an object."));
            }
            ClassifiersInfo? cls = ClassifiersInfo.Parse(obj);
            return new Library(version, name, rules, artifact, cls);
        }
        public static List<Library> ParseLibraries(MinecraftVersion version,JArray arr)
        {
            var result = new List<Library>();
            foreach(JObject obj in arr)
            {
                result.Add(Parse(version, obj));
            }
            return result;
        }
    }
}
