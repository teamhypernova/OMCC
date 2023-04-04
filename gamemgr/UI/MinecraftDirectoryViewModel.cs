using CommunityToolkit.Mvvm.ComponentModel;

namespace OMCC.Plugins.GameManager.UI
{
    public partial class MinecraftDirectoryViewModel : ObservableObject
    {
        public MinecraftDirectoryViewModel(MinecraftDirectory directory)
        {
            Directory = directory;
            Name = Directory.GetDisplayName();
            Path = Directory.DirectoryPath;
        }
        public MinecraftDirectory Directory { get; set; }
        [ObservableProperty] string? name;
        [ObservableProperty] string? path;
    }
}
