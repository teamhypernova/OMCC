namespace OMCC.Plugins.GameManager
{
    public class MinecraftFeatures
    {
        public MinecraftFeatures(bool hasCustomResolution, bool isDemoUser)
        {
            HasCustomResolution = hasCustomResolution;
            IsDemoUser = isDemoUser;
        }
        public MinecraftFeatures()
        {

        }
        public bool HasCustomResolution { get; set; }
        public bool IsDemoUser { get; set; }
    }
}
