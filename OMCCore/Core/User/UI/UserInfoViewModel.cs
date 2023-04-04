using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using OMCCore.Resources;
using OMCCore.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using OMCCore.UI;

namespace OMCCore.Core.User.UI
{
    public partial class UserInfoViewModel : ObservableObject
    {
        public UserInfoViewModel()
        {
            Load();
            WeakReferenceMessenger.Default.Register<SelectedUserChangedMessage>(this, (x, s) => Load());
        }
        [ObservableProperty] UserViewModel? selectedUser;

        [ObservableProperty] bool hasManagementPage = false;
        IUISupported? uimanager;

        [ObservableProperty] bool hasAddPage = false;
        IAddSupported? addpage;

        [ObservableProperty] bool noUsers = false;
        [RelayCommand(CanExecute =nameof(HasManagementPage))]
        void OpenManagementPage(IPageNavigator nv)
        {
            if (uimanager != null)
            {
                nv.AddPage(uimanager.CreatePage());
            }
        }
        [RelayCommand(CanExecute = nameof(HasAddPage))]
        void OpenAddPage(IPageNavigator nv)
        {
            if (addpage != null)
            {
                var pg = addpage.CreatePage();
                if (pg.createWindow)
                {
                    nv.AddPage(pg.page, true, true);
                }
                else
                {
                    nv.AddPage(pg.page);
                }
            }
        }
        public void Load()
        {
            var mgr = UserRegistry.Current.Selected;
            if(mgr is IUISupported ui)
            {
                HasManagementPage = true;
                uimanager = ui;
            }
            else
            {
                HasManagementPage = false;
            }
            if (mgr is IAddSupported ad)
            {
                HasAddPage = true;
                addpage = ad;
            }
            else
            {
                HasAddPage = false;
            }
            var usr = mgr?.GetSelectedUser();
            if (usr != null)
            {
                NoUsers = false;
                //TODO: Support for Delayed User
                var m = new UserViewModel();
                m.Name = usr.NameImmediate;
                if(usr is IImmediateIcon icon)
                {
                    m.Icon = icon.IconImmediate;
                }
                else
                {
                    m.Icon = ResIndex.steve;
                }
                if(usr is IAdditionalString add)
                {
                    m.HasAdditionalString = true;
                    m.AdditionalString = add.AdditionalString;
                }
                this.SelectedUser = m;
            }
            else
            {
                NoUsers = true;
            }
        }
    }
}
