using OMCCore.Model.Data;

namespace OMCC.Plugins.GameManager
{
    public class MinecraftConfig : ConfigFile
    {
        public GlobalVersionIsolation Isolation { get; set; } = GlobalVersionIsolation.ModdedAndUnreleased;
        public bool HasCustomResolution { get; set; } = false;
        public bool IsDemoUser { get; set; } = false;
        public int ResWidth { get; set; } = 800;
        public int ResHeight { get; set; } = 600;
    }
}
