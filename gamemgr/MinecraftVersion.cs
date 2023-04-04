using OMCCore.Core.Game;
using OMCCore.Model.Data;
using System;
using System.Collections.ObjectModel;

namespace OMCC.Plugins.GameManager
{
    public class MinecraftLauncher : ILauncher
    {
        public MinecraftVersion Version { get; set; }

        public MinecraftLauncher(MinecraftVersion version)
        {
            Version = version;
        }

        public void CompleteFiles()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }
    }
    public class MinecraftVersion : IGameVersion
    {
        public MinecraftVersion(MinecraftDirectory directory, string localId)
        {
            Directory = directory;
            LocalId = localId;
        }
        public MinecraftVersionConfig Config => Configs.GetConfig<MinecraftVersionConfig>(ConfigFilePath);
        public MinecraftDirectory Directory { get; }
        public string LocalId { get; private set; }
        public string Path => System.IO.Path.Combine(Directory.VersionsPath, LocalId);
        public string ConfigFilePath => System.IO.Path.Combine(Path, "omcl.config");
        public string JsonPath => System.IO.Path.Combine(Path, LocalId + ".json");
        public string JarPath => System.IO.Path.Combine(Path, LocalId + ".jar");
        public string Name => Config.Name;
        public string GetDisplayName()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return LocalId;
            }
            else
            {
                return Name;
            }
        }
        public string Description => Config.Description;
        public string GetDisplayDescription()
        {

            if (!string.IsNullOrWhiteSpace(Description))
            {
                return Description;
            }
            else
            {
                var n = GetDisplayName();
                if (n == LocalId)
                {
                    return n;
                }
                else
                {
                    return $"{n}({LocalId})";
                }
            }
        }
        public ILauncher GetLauncher()
        {
            throw new NotImplementedException();
        }

        public GameValidationInfo Validate()
        {
            return MinecraftValidationExtend.Validate(this);
        }
    }
}
