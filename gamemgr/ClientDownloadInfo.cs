using Newtonsoft.Json.Linq;
using System;

namespace OMCC.Plugins.GameManager
{
    public sealed class ClientDownloadInfo : IDownloadInfo, IMinecraftBased
    {
        public ClientDownloadInfo(MinecraftVersion version, long size, string url, string? sha1)
        {
            Version = version;
            Size = size;
            Url = url;
            Path = Version.JarPath;
            Sha1 = sha1;
        }
        public static ClientDownloadInfo Parse(MinecraftVersion version, JObject json)
        {
            return new ClientDownloadInfo(version, 
                (long?)json["size"] ?? throw new ArgumentNullException("json[size]"), 
                (string?)json["url"] ?? throw new ArgumentNullException("json[url]"), 
                (string?)json["sha1"]);
        }
        public MinecraftVersion Version { get; set; }

        public long Size { get; set; }

        public string Url { get; set; }

        public string Path { get; set; }

        public string? Sha1 { get; set; }
    }
}
