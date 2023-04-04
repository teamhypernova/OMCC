using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace OMCCore.UI
{
    public abstract partial class BasePageViewModel : BaseViewModel, IPageNavigator
    {
        [ObservableProperty] string title = "";
        public OPage? Page { get; set; }
        public void AddPage(OPage page, bool createFrame = false, bool dialog = false)
        {
            if (Page != null)
            {
                Page.AddPage(page, createFrame, dialog);
            }
            
        }
        public void GoBack()
        {
            if (Page != null)
            {
                Page.GoBack();
            }
        }
        public void invk(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }
    }
}
