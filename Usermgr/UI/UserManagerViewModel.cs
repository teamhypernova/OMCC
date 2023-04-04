using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using EDGW.Logging;
using OMCC.Plugins.UserManager.Resources;
using OMCCore.Core.User;
using OMCCore.Model.Data;
using OMCCore.UI;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace OMCC.Plugins.UserManager.UI
{
    public partial class UserManagerViewModel : BasePageViewModel
    {
        public static Logger logger = new Logger("User Manager", nameof(UserManagerViewModel));
        public ObservableCollection<UserViewModel> Users { get; } = new();
        [ObservableProperty] UserViewModel? selectedUser;
        [ObservableProperty] bool isError = false;
        [ObservableProperty] bool isLoading = true;
        [ObservableProperty] string? errorMessage = "";

        public UserManagerViewModel()
        {
            WeakReferenceMessenger.Default.Register<SelectedUserChangedMessage>(this, (x, s) =>
            {
                if (LoadCommand.CanExecute(null))
                {
                    LoadCommand.Execute(null);
                }
            });
        }
        [RelayCommand]
        public async Task LoadAsync()
        {
            await Task.Run(Load);   
        }
        public void Load()
        {
            IsLoading = true;
            IsError = false;
            var mgr = Plugins.UserManager.UserManager.Current;
            Application.Current.Dispatcher.Invoke(() =>
            {
                Users.Clear();
            });
            try
            {
                foreach(var userImmediate in mgr.GetUsers())
                {
                    if(userImmediate is User user)
                    {
                        UserViewModel model = new UserViewModel(this);
                        if(user is IImmediateIcon icon)
                        {
                            model.Icon = icon.IconImmediate;
                        }
                        else
                        {
                            model.Icon = ResIndex.steve;
                        }
                        model.Name = user.NameImmediate;
                        model.TypeKey = user.UserType.Key;
                        if (user.IsSelected)
                        {
                            model.IsSelected = true;
                            SelectedUser = model;
                        }
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Users.Add(model);
                        });
                        //TODO: Support for Delayed User
                    }
                }
            }
            catch(Exception ex)
            {
                IsError = true;
                ErrorMessage = ex.Message;
                logger.error("Cannot load userinfo.", ex);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
