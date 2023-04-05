using OMCCore.Core;
using OMCCore.Core.User;
using System.Collections.Generic;

namespace OMCC.Plugins.GameManager
{
    public class MinecraftSettings
    {
        public MinecraftSettings(MinecraftFeatures features, Dictionary<string, string?> dictionary, string? username, string? versionName,
            string? gameDirectory, string? assetsDir, string? assetsName, string? uuid, string? token, string? userType, string? versionType,
            string? xuid, int resolutionWidth, int resolutionHeight, string? nativesDir, string? launcherName, string? launcherVersion,
            string? classpath, bool isDemoUser, bool hasCustomResolution, string clientId)
        {

            Features = features;                Dictionary = dictionary;
            Username = username;                VersionName = versionName;
            GameDirectory = gameDirectory;      AssetsDir = assetsDir;
            AssetsName = assetsName;            Uuid = uuid;
            Token = token;                      UserType = userType;
            VersionType = versionType;          Xuid = xuid;
            ResolutionWidth = resolutionWidth;  ResolutionHeight = resolutionHeight;
            NativesDir = nativesDir;            LauncherName = launcherName;
            LauncherVersion = launcherVersion;  Classpath = classpath;
            IsDemoUser = isDemoUser;            HasCustomResolution = hasCustomResolution;
            ClientId = clientId;
        }

        public static MinecraftSettings Create(string? username, string? versionName, string? gameDirectory, string? assetsDir, string? assetsName,
            string? uuid, string? token, string? userType, string? versionType, string? xuid, int resolutionWidth, int resolutionHeight,
            string? nativesDir, string? launcherName, string? launcherVersion, string? classpath, bool isDemoUser, bool hasCustomResolution, string clientId)
        {
            return new MinecraftSettings(new MinecraftFeatures(), new Dictionary<string, string?>(), username, versionName, gameDirectory,
                assetsDir, assetsName, uuid, token, userType, versionType, xuid, resolutionWidth, resolutionHeight, nativesDir, launcherName,
                launcherVersion, classpath, isDemoUser, hasCustomResolution, clientId);
        }
        public static MinecraftSettings Create(MinecraftFeatures features,UserInfo user, MinecraftProfile prof, string[] classpath, ResolutionInfo resolution)
        {
            var version = prof.Version;
            return Create(user.Username, version.LocalId, version.GameDirectory, version.Directory.AssetsPath, prof.AssetIndex.Id,
                user.Uuid, user.Token, user.Usertype, prof.Type, user.Xuid, resolution.Width, resolution.Height, version.NativesDirectory,
                Starter.Instance.LauncherName, Starter.Instance.LauncherVersion,
                string.Join(";", classpath), features.IsDemoUser, features.HasCustomResolution, prof.Version.LocalId);
        }
        public MinecraftFeatures Features { get; set; } = new MinecraftFeatures();
        public Dictionary<string, string?> Dictionary { get; } = new Dictionary<string, string?>();
        public string? Username { get => Dictionary.GetValueOrDefault("auth_player_name", null); set => Dictionary["auth_player_name"] = value; }
        public string? VersionName { get => Dictionary.GetValueOrDefault("version_name", null); set => Dictionary["version_name"] = value; }
        public string? GameDirectory { get => Dictionary.GetValueOrDefault("game_directory", null); set => Dictionary["game_directory"] = value; }
        public string? AssetsDir { get => Dictionary.GetValueOrDefault("assets_root", null); set => Dictionary["assets_root"] = value; }
        public string? AssetsName { get => Dictionary.GetValueOrDefault("assets_index_name", null); set => Dictionary["assets_index_name"] = value; }
        public string? Uuid { get => Dictionary.GetValueOrDefault("auth_uuid", null); set => Dictionary["auth_uuid"] = value; }
        public string? Token { get => Dictionary.GetValueOrDefault("auth_access_token", null); set => Dictionary["auth_access_token"] = Dictionary["auth_session"] = value; }
        public string? UserType { get => Dictionary.GetValueOrDefault("user_type", null); set => Dictionary["user_type"] = value; }
        public string? VersionType { get => Dictionary.GetValueOrDefault("version_type", null); set => Dictionary["version_type"] = value; }
        public string? Xuid { get => Dictionary.GetValueOrDefault("auth_xuid", null); set => Dictionary["auth_xuid"] = value; }
        public int ResolutionWidth { get => int.Parse(Dictionary.GetValueOrDefault("resolution_width", null) ?? "0"); set => Dictionary["resolution_width"] = value.ToString(); }
        public int ResolutionHeight { get => int.Parse(Dictionary.GetValueOrDefault("resolution_height", null) ?? "0"); set => Dictionary["resolution_height"] = value.ToString(); }
        public string? NativesDir { get => Dictionary.GetValueOrDefault("natives_directory", null); set => Dictionary["natives_directory"] = value; }
        public string? LauncherName { get => Dictionary.GetValueOrDefault("launcher_name", null); set => Dictionary["launcher_name"] = value; }
        public string? LauncherVersion { get => Dictionary.GetValueOrDefault("launcher_version", null); set => Dictionary["launcher_version"] = value; }
        public string? Classpath { get => Dictionary.GetValueOrDefault("classpath", null); set => Dictionary["classpath"] = value; }
        public string? ClientId { get => Dictionary.GetValueOrDefault("clientid", null); set => Dictionary["clientid"] = value; }
        public bool IsDemoUser { get => Features.IsDemoUser; set => Features.IsDemoUser = value; }
        public bool HasCustomResolution { get => Features.HasCustomResolution; set => Features.HasCustomResolution = value; }
    }
}
