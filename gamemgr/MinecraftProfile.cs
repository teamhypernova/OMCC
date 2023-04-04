using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace OMCC.Plugins.GameManager
{
    public class MinecraftProfile
    {
        public MinecraftProfile(AssetIndexDownloadInfo assetIndex, ClientDownloadInfo client, string id, JavaVersion javaVersion, List<Library> libraries, string mainClass, string type)
        {
            AssetIndex = assetIndex;
            Client = client;
            Id = id;
            JavaVersion = javaVersion;
            Libraries = libraries;
            MainClass = mainClass;
            Type = type;
        }

        public AssetIndexDownloadInfo AssetIndex { get; set; }
        public ClientDownloadInfo Client { get; set; }
        public string Id { get; set; }
        public JavaVersion JavaVersion { get; set; }
        public List<Library> Libraries { get; set; }
        public string MainClass { get; set; }
        public string Type { get; set; }
        //TODO:Arguments
        public static MinecraftProfile Parse(MinecraftVersion version)
        {
            var json = JObject.Parse(File.ReadAllText(version.JsonPath));
            var assets = AssetIndexDownloadInfo.Parse(version, json["assetIndex"] as JObject ?? throw new ArgumentException("'json[assetIndex]' must be a object."));
            var client = ClientDownloadInfo.Parse(version, json["downloads"]?["client"] as JObject ?? throw new ArgumentException("'json[downloads][client]' must be a object."));
            var id = json["id"]?.ToString() ?? throw new ArgumentNullException("json[id]");
            var javaVersion = JavaVersion.Parse(json["javaVersion"] as JObject ?? throw new ArgumentException("'json[javaVersion]' must be a object."));
            var libs = Library.ParseLibraries(version, json["libraries"] as JArray ?? throw new ArgumentException("'json[libraries]' must be an array."));
            var mainCls = json["mainClass"]?.ToString() ?? throw new ArgumentNullException("json[mainClass]");
            var type = json["type"]?.ToString() ?? throw new ArgumentNullException("json[type]");
            return new MinecraftProfile(assets, client, id, javaVersion, libs, mainCls, type);
        }
    }
}
