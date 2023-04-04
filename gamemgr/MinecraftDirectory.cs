using OMCCore.Core;
using OMCCore.Model.Data;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace OMCC.Plugins.GameManager
{
    public class MinecraftDirectory : ISerializable , IReadOnlyCollection<MinecraftVersion>
    {
        public string DirectoryPath { get; set; } = "";
        public string VersionsPath => Path.Combine(DirectoryPath, "versions");
        public string AssetsPath => Path.Combine(DirectoryPath, "assets");
        public string AssetIndexesPath => Path.Combine(AssetsPath, "indexes");
        public string AssetObjectsPath => Path.Combine(AssetsPath, "objects");
        public string ConfigFilePath => Path.Combine(DirectoryPath, "omcl.config");
        public string Name => Config.Name;
        public string GetDisplayName()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return Path.GetFileName(DirectoryPath) ?? DirectoryPath;
            }
            else
            {
                return Name;
            }
        }
        public MinecraftDirectoryConfig Config => Configs.GetConfig<MinecraftDirectoryConfig>(ConfigFilePath);
        public int Count => GetVerPaths().Length;
        public void Deserialize(string data)
        {
            DirectoryPath = data;
        }
        public IEnumerator<MinecraftVersion> GetEnumerator()
        {
            foreach (var path in GetVerPaths())
            {
                var name = Path.GetFileName(path);
                yield return new MinecraftVersion(this, name);
            }
            yield break;
        }
        public string[] GetVerPaths()
        {
            Directory.CreateDirectory(VersionsPath);
            return Directory.GetDirectories(VersionsPath);
        }
        public MinecraftVersion[] GetVersions()
        {
            List<MinecraftVersion> versions = new List<MinecraftVersion>();
            var enume = this.GetEnumerator();
            while (enume.MoveNext())
            {
                var e= enume.Current;
                versions.Add(e);
            }
            enume.Dispose();
            return versions.ToArray();
        }
        public MinecraftVersion? GetSelectedVersion()
        {
            var enume = this.GetEnumerator();
            try
            {
                var config = this.Config;
                bool any = false;
                MinecraftVersion? first = null;
                while (enume.MoveNext())
                {
                    var e = enume.Current;
                    if (!any) first = e;
                    any = true;
                    if (e.LocalId == config.SelectionId)
                    {
                        return e;
                    }
                }
                if (any)
                {
                    return first;
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                enume.Dispose();
            }
        }
        public string Serialize()
        {
            return DirectoryPath;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
