namespace OMCCore.Core.Game
{
    public interface IGameVersion
    {
        public string GetDisplayName();
        public string GetDisplayDescription();
        public GameValidationInfo Validate();
        public ILauncher GetLauncher();
    }
}
