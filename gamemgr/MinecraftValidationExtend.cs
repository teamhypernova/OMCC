using EDGW.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using OMCCore.Core.Game;
using OMCCore.Globalization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OMCC.Plugins.GameManager
{
    public static class MinecraftValidationExtend
    {

        static Logger Logger = new Logger("Version Validator", nameof(MinecraftValidationExtend));
        const string LANP = "plugin.official.gamemgr.validation.";
        public static bool HasJsonFile(this MinecraftVersion version)
        {
            return File.Exists(version.JsonPath);
        }
        public static bool HasJarFile(this MinecraftVersion version)
        {
            return File.Exists(version.JarPath);
        }
        public static void CheckJson(this MinecraftVersion version)
        {
            if (!HasJsonFile(version))
            {
                throw new ValidationException(new Text(LANP + "json_missing").Content);
            }
            else
            {
                var json = JObject.Parse(File.ReadAllText(version.JsonPath));
                JSchema schema = JSchema.Parse(Resources.Resources.schema);
                IList<string> outs;
                if (!json.IsValid(schema, out outs))
                {
                    throw new ValidationException(new Text(LANP + "json_schema_invalid", string.Join("\n", outs.ToArray())).Content);
                }
            }
        }
        public static void CheckJar(this MinecraftVersion version)
        {
            if (!HasJarFile(version))
            {
                throw new ValidationException(new Text(LANP + "jar_missing").Content, true);
            }
        }
        public static GameValidationInfo Validate(this MinecraftVersion version)
        {
            try
            {
                CheckJar(version);
                CheckJson(version);
                return new GameValidationInfo(true);
            }
            catch(ValidationException ex)
            {
                Logger.error("Invalid version:" + version.LocalId, ex);
                return new GameValidationInfo(false, ex.Message, ex.Message.Split('\n').FirstOrDefault(""), ex.Launchable);
            }
            catch(Exception ex)
            {
                Logger.error("Invalid version:" + version.LocalId, ex);
                return new GameValidationInfo(false, ex.Message, ex.Message.Split('\n').FirstOrDefault(""));
            }
        }
    }
}
