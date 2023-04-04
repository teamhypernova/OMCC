using EDGW.Logging;
using OMCCore.Core;
using System;
using System.IO;
using System.Reflection;

namespace OMCCore.Plugins
{
    public class PluginRegistry : Registry<IDescriptor>
    {
        public static PluginRegistry Current { get; } = new PluginRegistry();
        protected override Logger Logger => new Logger("Plugin Loader", "pluginloader");

        public void LoadPlugins()
        {
            Directory.CreateDirectory("plugins");
            foreach (var file in Directory.GetFiles("plugins"))
            {
                LoadPlugin(file);
            }
            foreach (var p in Registered)
            {
                p.Plugin.OnRegistering();
            }
        }
        public void LoadPlugin(string filename)
        {
            try
            {
                Assembly asm = Assembly.Load(File.ReadAllBytes(filename));
                var desc = asm.CreateInstance("OMCC.Plugins.Descriptor") as IDescriptor;
                if (desc != null)
                {
                    desc.Plugin.OnRegisteringLanguagePack();
                    Logger.info($"Loaded Plugin {desc.Name}({desc.Id})");
                    Register(desc);
                }
                else
                {
                    Logger.error("Cannot load class \"OMCC.Plugins.Descriptor\" at " + filename);
                }
            }
            catch (Exception ex)
            {
                Logger.error("Cannot load plugin at " + filename, ex);
            }
        }
    }
}
