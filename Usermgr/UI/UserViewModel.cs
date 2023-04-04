using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Windows.Media;

namespace OMCC.Plugins.UserManager.UI
{
    public partial class UserViewModel : ObservableObject
    {
        public UserViewModel(UserManagerViewModel manager)
        {
            Manager = manager ?? throw new ArgumentNullException(nameof(manager));
        }
        public UserManagerViewModel Manager { get; set; }
        [ObservableProperty] string? name;
        [ObservableProperty] string? typeKey;
        [ObservableProperty] ImageSource? icon;
        [ObservableProperty] bool isSelected = false;
    }
}
