using CommunityToolkit.Mvvm.ComponentModel;

namespace OMCCore.Core.Game.UI
{
    public partial class GameViewModel : ObservableObject
    {
        [ObservableProperty] string? name;
        [ObservableProperty] string? description;
        [ObservableProperty] string? tooltip;
        [ObservableProperty] bool error;
        public GameViewModel(IGameVersion version)
        {
            Version = version;
            Name = version.GetDisplayName();
            Description = version.GetDisplayDescription();
            Tooltip = Description;
            var info = version.Validate();
            if (info.IsValidVersion)
            {
                Error = false;
            }
            else
            {
                Error = true;
                Description = info.SimpleInvalidMessage;
                Tooltip = info.InvalidMessage;
            }
        }
        public IGameVersion Version { get; }
    }
}
