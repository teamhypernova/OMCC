namespace OMCC.Plugins.GameManager
{
    public class ResolutionInfo
    {
        private ResolutionInfo(bool hasCustomResolution, int width, int height)
        {
            HasCustomResolution = hasCustomResolution;
            Width = width;
            Height = height;
        }
        public static ResolutionInfo Auto() => new ResolutionInfo(false, 0, 0);
        public static ResolutionInfo Create(int width, int height) => new ResolutionInfo(true, width, height);
        public bool HasCustomResolution { get; set; }
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
    }
}
