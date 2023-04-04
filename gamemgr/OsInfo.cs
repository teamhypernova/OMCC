using Newtonsoft.Json.Linq;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace OMCC.Plugins.GameManager
{
    public class OsInfo
    {
        public OsInfo(Regex? name, Regex? version, Arch? arch)
        {
            Name = name;
            Version = version;
            Arch = arch;
        }

        public Regex? Name { get; set; }
        public Regex? Version { get; set; }
        public Arch? Arch { get; set; }
        public static OsInfo Parse(JObject json)
        {
            string? arch = json["arch"]?.ToString();
            Arch? a = null;
            if (arch != null)
            {
                if (arch == "x86")
                {
                    a = Plugins.GameManager.Arch.x86;
                }
                else
                {
                    a = Plugins.GameManager.Arch.x86_64;
                }
            }
            string? n = json["name"]?.ToString(), v = json["version"]?.ToString();
            Regex? name = null, ver = null;
            if (n != null)
            {
                name = new Regex(n);
            }
            if (v != null)
            {
                ver = new Regex(v);
            }
            return new OsInfo(name, ver, a);
        }
        public bool IsMatch()
        {
            bool m = true;
            if (Name != null)
            {
                if (!Name.IsMatch("windows")) m = false;
            }
            if (Version != null)
            {
                if (!Version.IsMatch(Environment.OSVersion.Version.Major.ToString())) m = false;
            }
            if (Arch != null)
            {
                var a = Arch;
                var b = Environment.Is64BitOperatingSystem ? Plugins.GameManager.Arch.x86_64 : Plugins.GameManager.Arch.x86;
                if (a != b) m = false;
            }
            return m;
        }
    }
}
