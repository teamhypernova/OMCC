using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OMCCore.UI
{
    public class OFrame : Control
    {
        public OFrame()
        {
            Pages.CollectionChanged += OnPagesChanged;
        }
        static OFrame()
        {

            DefaultStyleKeyProperty.OverrideMetadata(typeof(OFrame), new FrameworkPropertyMetadata(typeof(OFrame)));
        }
        public override void OnApplyTemplate()
        {
            WindowContent = VisualTreeHelperExtend.FindParent<OWindowContent>(this);
            if (WindowContent != null)
            {
                WindowContent.ChildOFrame = this;
                WindowContent.GoBackCommand = GoBackCommand; ;
                WindowContent.CanGoBack = CanGoBack;
            }
        }
        public OWindowContent? WindowContent { get; set; }
        private void OnPagesChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var pg = Pages.Any() ? Pages.Last() : null;
            if (pg != null)
            {
                pg.Frame = this;
            }
            if (pg != SelectedPage)
            {
                SelectedPage = pg;
                if (pg != null && WindowContent != null)
                {
                    WindowContent.Title = pg.Title;
                }
            }
            var cb = Pages.Count > 1;
            if (cb != CanGoBack)
            {
                CanGoBack = cb;
            }
        }

        public ObservableCollection<OPage> Pages { get; } = new();
        public ICommand GoBackCommand => new RelayCommand(GoBack);
        public void GoBack()
        {
            if (Pages.Count > 0)
                Pages.RemoveAt(Pages.Count - 1);
        }
        #region PROPERTY CANGOBACK

        public bool CanGoBack { get => GetCanGoBack(this); set => SetCanGoBack(this, value); }
        public static bool GetCanGoBack(DependencyObject obj)
        {
            return (bool)obj.GetValue(CanGoBackProperty);
        }

        public static void SetCanGoBack(DependencyObject obj, bool value)
        {
            obj.SetValue(CanGoBackProperty, value);
        }

        // Using a DependencyProperty as the backing store for CanGoBack.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanGoBackProperty =
            DependencyProperty.RegisterAttached("CanGoBack", typeof(bool), typeof(OFrame), new PropertyMetadata(false, (s, x) =>
            {
                var f = (OFrame)s;
                var wc = f.WindowContent;
                if (wc != null)
                {
                    wc.CanGoBack = (bool)x.NewValue;
                }
            }));


        #endregion
        #region PROPERTY SELECTEDPAGE

        public OPage? SelectedPage { get => GetSelectedPage(this); set => SetSelectedPage(this, value); }
        public static OPage GetSelectedPage(DependencyObject obj)
        {
            return (OPage)obj.GetValue(SelectedPageProperty);
        }

        public static void SetSelectedPage(DependencyObject obj, OPage value)
        {
            obj.SetValue(SelectedPageProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedPage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedPageProperty =
            DependencyProperty.RegisterAttached("SelectedPage", typeof(OPage), typeof(OFrame), new PropertyMetadata(null, (s, x) =>
            {
                var v = x.NewValue as OPage;
                var f = s as OFrame;
                if(f != null && v != null)
                {
                    if (!f.Pages.Contains(v))
                    {
                        f.Pages.Add(v);
                    }
                }
            }));


        #endregion
        public void AddPage(OPage page)
        {
            Pages.Add(page);
        }
        public void ReplacePage(OPage page)
        {
            Pages.Add(page);
            if (Pages.Count > 1)
            {
                Pages.RemoveAt(Pages.Count - 1 - 1);
            }
        }
        public void ClearBackPages()
        {
            for(int i = 0; i < Pages.Count - 1; i++)
            {
                Pages.RemoveAt(0);
            }
        }
        public void ClearPages()
        {
            Pages.Clear();
        }
    }
}
