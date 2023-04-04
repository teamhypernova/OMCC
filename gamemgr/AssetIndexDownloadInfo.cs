using Newtonsoft.Json.Linq;
using System;

namespace OMCC.Plugins.GameManager
{
    public sealed class AssetIndexDownloadInfo : IDownloadInfo, IMinecraftBased
    {
        public AssetIndexDownloadInfo(MinecraftVersion version,long size, string url, string id, string? sha1)
        {
            Version = version;
            Size = size;
            Url = url;
            Path = System.IO.Path.Combine(version.Directory.AssetIndexesPath, id + ".json");
            Sha1 = sha1;
        }

        public static AssetIndexDownloadInfo Parse(MinecraftVersion version, JObject json)
        {
            return new AssetIndexDownloadInfo(version,
                (long?)json["size"] ?? throw new ArgumentNullException("json[size]"),
                (string?)json["url"] ?? throw new ArgumentNullException("json[url]"),
                (string?)json["id"] ?? throw new ArgumentNullException("json[id]"),
                (string?)json["sha1"]);
        }
        public long Size { get; set; }

        public string Url { get; set; }

        public string Path { get; set; }

        public string? Sha1 { get; set; }
        public MinecraftVersion Version { get; set; }
    }
}
