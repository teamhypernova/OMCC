using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using OMCCore.Model.Data;
using OMCCore.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace OMCCore.Core.Game.UI
{
    public partial class GameInfoViewModel : ObservableObject
    {
        public GameInfoViewModel()
        {
            Load();
            WeakReferenceMessenger.Default.Register<SelectedVersionChangedMessage>(this, (x, s) =>
            {
                Load();
            });
        }
        [ObservableProperty] GameViewModel? selectedGame;
        [ObservableProperty] bool hasVersions = true;
        [ObservableProperty] bool hasManagementPage = false;
        IUISupported? ui;

        [RelayCommand(CanExecute = nameof(HasManagementPage))]
        public void OpenManagementPage(IPageNavigator navigator)
        {
            if (ui != null)
            {
                navigator.AddPage(ui.CreatePage());
            }
        }
        [RelayCommand(CanExecute = nameof(HasVersions))]
        public void StartSelectedGame()
        {
            if (SelectedGame != null)
            {
                StartGame(SelectedGame);
            }
        }
        [RelayCommand(CanExecute =nameof(HasVersions))]
        public void StartGame(GameViewModel vm)
        {
            try
            {
                var l = vm.Version.GetLauncher();
                l.Start();
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                //TODO:exception handler
            }
        }
        public void Load()
        {
            HasVersions = true;
            HasManagementPage = false;
            var mgr = GameRegistry.Current.Selected;
            if (mgr != null)
            {
                var v = mgr.GetSelectedVersion();
                if (v == null)
                {
                    HasVersions = false;
                }
                else
                {
                    SelectedGame = new GameViewModel(v);
                }
                if(mgr is IUISupported sp)
                {
                    HasManagementPage = true;
                    ui = sp;
                }
            }
            else
            {
                HasVersions = false;
            }
        }
    }
}
