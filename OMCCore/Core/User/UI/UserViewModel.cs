using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Media;

namespace OMCCore.Core.User.UI
{
    public class UserViewModel : ObservableObject
    {
        public string? Name { get; set; }
        public string? AdditionalString { get; set; }
        public bool HasAdditionalString { get; set; } = false;
        public ImageSource? Icon { get; set; }

    }
}
