using CommunityToolkit.Mvvm.Input;
using OMCCore.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OMCC.Plugins.UserManager.UI.Add
{
    public partial class AddUserPageViewModel : BasePageViewModel
    {
        public ObservableCollection<UserTypeViewModel> UserTypes { get; } = new ObservableCollection<UserTypeViewModel>();
        [RelayCommand]
        public void Load()
        {
            UserTypes.Clear();
            foreach (var type in Plugins.UserManager.UserManager.Current.Registered)
            {
                UserTypes.Add(new UserTypeViewModel(type) { AddPageCommand = AddUserCommand });
            }
        }
        [RelayCommand]
        public void AddUser(UserTypeViewModel type)
        {
            var pg = type.UserType.CreatePage();
            pg.Callback += OnAddUserPageCallback;
            AddPage(pg);
        }

        private void OnAddUserPageCallback(bool succeeded, User? user)
        {
            if (succeeded)
            {
                if (user != null)
                {
                    Plugins.UserManager.UserManager.Current.AddUser(user);
                    GoBack();
                }
            }
        }
    }
}
