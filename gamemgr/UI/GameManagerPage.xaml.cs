using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OMCCore.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OMCC.Plugins.GameManager.UI
{
    /// <summary>
    /// GameManagerPage.xaml 的交互逻辑
    /// </summary>
    public partial class GameManagerPage : OPage
    {
        public GameManagerPage()
        {
            InitializeComponent();
        }
    }
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
    public partial class MinecraftVersionViewModel : ObservableObject
    {
        MinecraftVersion Version { get; set; }
        [ObservableProperty] bool isValidating = false;
        [ObservableProperty] bool isError = false;
        [ObservableProperty] string? description;
        [ObservableProperty] string? tooltip;
        [ObservableProperty] string? name;

        public MinecraftVersionViewModel(MinecraftVersion version)
        {
            Version = version;
            IsValidating = true;
            IsError = false;
            Description = version.GetDisplayDescription();
            Name = version.GetDisplayName();
            Tooltip = null;
        }

        [RelayCommand]
        public async Task LoadAsync()
        {
            await Task.Run(Load);
        }
        public void Load()
        {
            IsValidating = true;
            IsError = false;
            try
            {
                var info = Version.Validate();
                if (!info.IsValidVersion)
                {
                    IsError = true;
                    Description = info.SimpleInvalidMessage;
                    Tooltip = info.InvalidMessage;
                }
            }
            finally
            {
                IsValidating = false;
            }
        }
    }
}
