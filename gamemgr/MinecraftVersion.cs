using OMCCore.Core.Game;
using OMCCore.Model.Data;
using OMCCore.News;
using System;
using System.Collections.ObjectModel;

namespace OMCC.Plugins.GameManager
{
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
        public bool IsModded => false;//TODO:IsModded
        public bool IsUnreleased => false;//TODO:IsUnreleased
        public string GameDirectory
        {
            get
            {
                if (IsIsolated)
                {
                    return Path;
                }
                else
                {
                    return Directory.DirectoryPath;
                }
            }
        }
        public string NativesDirectory => System.IO.Path.Combine(Path, LocalId + "-natives");
        public bool IsIsolated
        {
            get
            {
                var isol = Config.Isolation;
                if(isol== VersionIsolation.Global)
                {
                    var gisol = Configs.GetConfig<MinecraftConfig>();
                    switch (gisol.Isolation)
                    {
                        case GlobalVersionIsolation.On:
                            {
                                return true;
                            }
                        case GlobalVersionIsolation.Off:
                            {
                                return false;
                            }
                        case GlobalVersionIsolation.Modded:
                            {
                                return IsModded;
                            }
                        case GlobalVersionIsolation.Unreleased:
                            {
                                return IsUnreleased;
                            }
                        case GlobalVersionIsolation.ModdedAndUnreleased:
                            {
                                return IsModded | IsUnreleased;
                            }
                        default:
                            {
                                return false;
                            }
                    }
                }
                else
                {
                    return isol == VersionIsolation.On;
                }
            }
        }
        public bool IsDemoUser
        {
            get
            {
                if(Config.IsDemoUser== VersionBoolean.Global)
                {
                    return Configs.GetConfig<MinecraftConfig>().IsDemoUser;
                }
                else
                {
                    return Config.IsDemoUser == VersionBoolean.True;
                }
            }
        }
        public ResolutionInfo Resolution
        {
            get
            {
                if (Config.HasCustomResolution == VersionBoolean.Global)
                {
                    var config = Configs.GetConfig<MinecraftConfig>();
                    if (config.HasCustomResolution)
                    {
                        return ResolutionInfo.Create(config.ResWidth, config.ResHeight);
                    }
                    else
                    {
                        return ResolutionInfo.Auto();
                    }
                }
                else
                {
                    if (Config.HasCustomResolution == VersionBoolean.True)
                    {
                        return ResolutionInfo.Create(Config.ResWidth, Config.ResHeight);
                    }
                    else
                    {
                        return ResolutionInfo.Auto();
                    }
                }
            }
        }
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
            return new MinecraftLauncher(this);
        }

        public GameValidationInfo Validate()
        {
            return MinecraftValidationExtend.Validate(this);
        }
    }
}
