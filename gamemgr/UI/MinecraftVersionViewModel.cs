using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace OMCC.Plugins.GameManager.UI
{
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
