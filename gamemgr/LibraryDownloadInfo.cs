using Newtonsoft.Json.Linq;
using System;

namespace OMCC.Plugins.GameManager
{
    public sealed class LibraryDownloadInfo : IDownloadInfo
    {
        public LibraryDownloadInfo(long size, string url, string path, string? sha1)
        {
            Size = size;
            Url = url;
            Path = path;
            Sha1 = sha1;
        }

        public long Size { get; set; }

        public string Url { get; set; }

        public string Path { get; set; }

        public string? Sha1 { get; set; }
        public static LibraryDownloadInfo Parse(JObject json)
        {
            return new LibraryDownloadInfo(
                (long)(json["size"] ?? throw new ArgumentNullException("json[size]")),
                json["url"]?.ToString() ?? throw new ArgumentNullException("json[url]"),
                json["path"]?.ToString() ?? throw new ArgumentNullException("json[path]"),
                json["sha1"]?.ToString());

        }
    }
}
