using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OMCCore.UI;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace OMCC.Plugins.GameManager.UI
{
    public partial class GameManagerPageViewModel : BasePageViewModel
    {
        public ObservableCollection<MinecraftDirectoryViewModel> Directories { get; } = new();
        public ObservableCollection<MinecraftVersionViewModel> Versions { get; } = new();
        [ObservableProperty] bool isNoDirectories = false;
        [ObservableProperty] bool isNoVersions = false;
        [RelayCommand]
        public async Task LoadAsync()
        {
            await Task.Run(Load);
        }
        public void Load()
        {
            invk(() => Directories.Clear());
            foreach(var dir in GameManager.Current.Directories)
            {
                var vm = new MinecraftDirectoryViewModel(dir);
                invk(() => Directories.Add(vm));
            }
            LoadVersions();
        }
        public void LoadVersions()
        {
            IsNoDirectories = false;
            IsNoVersions = false;
            invk(() => Versions.Clear());
            var dir = GameManager.Current.Selected;
            if (dir != null)
            {
                bool nov = true;
                foreach(var v in dir.GetVersions())
                {
                    nov = false;
                    var vm = new MinecraftVersionViewModel(v);
                    invk(() => Versions.Add(vm));
                }
                IsNoVersions = nov;
            }
            else
            {
                IsNoDirectories = true;
                IsNoVersions = true;
            }
        }
    }
}
