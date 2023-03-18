using EDGW.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace OMCCore.Plugins
{
    public abstract class Plugin
    {

        public virtual void OnRegistering()
        {

        }
        public virtual void OnRegisteringLanguagePack()
        {

        }
        static Logger logger = new Logger("Plugin Loader", "pluginloader");
        static List<Descriptor> Plugins { get; } = new();
        public static void LoadPlugins()
        {
            Directory.CreateDirectory("plugins");
            foreach(var file in Directory.GetFiles("plugins"))
            {
                LoadPlugin(file);
            }
            foreach(var p in Plugins)
            {
                p.Plugin.OnRegistering();
            }
        }
        public static void LoadPlugin(string filename)
        {
            try
            {
                Assembly asm = Assembly.Load(File.ReadAllBytes(filename));
                var desc = asm.CreateInstance("OMCC.Plugins.Descriptor") as Descriptor;
                if(desc != null)
                {
                    desc.Plugin.OnRegisteringLanguagePack();
                    logger.info($"Loaded Plugin {desc.Name}({desc.Id})");
                    Plugins.Add(desc);
                }
                else
                {
                    logger.error("Cannot load class \"OMCC.Plugins.Descriptor\" at " + filename);
                }
            }catch(Exception ex)
            {
                logger.error("Cannot load plugin at " + filename, ex);
            }
        }
        
    }
}
