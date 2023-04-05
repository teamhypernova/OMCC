using OMCCore.Model.Data;

namespace OMCC.Plugins.GameManager
{
    public class MinecraftVersionConfig : ConfigFile
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public VersionIsolation Isolation { get; set; } = VersionIsolation.Global;
        public VersionBoolean HasCustomResolution { get; set; } = VersionBoolean.Global;
        public int ResWidth { get; set; } = 800;
        public int ResHeight { get; set; } = 600;
        public VersionBoolean IsDemoUser { get; set;} = VersionBoolean.Global;
    }
}
