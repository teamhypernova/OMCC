using Newtonsoft.Json.Linq;
using System.IO;

namespace OMCCore.Model.Data
{
    public static class ConfigFileManager
    {
        public static string ReadFile(string path)
        {
            if (!File.Exists($"OMCL\\{path}.config"))
                return "";
            return File.ReadAllText($"OMCL\\{path}.config");
        }
        public static void WriteFile(string path, string value)
        {
            Directory.CreateDirectory(Path.GetDirectoryName($"OMCL\\{path}.config") ?? "");
            File.WriteAllText($"OMCL\\{path}.config", value);
        }
        public static string ReadJson(string path)
        {
            try
            {
                return JObject.Parse(ReadFile(path)).ToString();
            }
            catch
            {
                return new JObject().ToString();
            }
        }
    }
}
