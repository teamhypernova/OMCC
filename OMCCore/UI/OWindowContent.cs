using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OMCCore.UI
{
    public class OWindowContent : ContentControl
    {
        static OWindowContent()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(OWindowContent), new FrameworkPropertyMetadata(typeof(OWindowContent)));
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var p = VisualTreeHelperExtend.FindParent<OWindow>(this);
            if(p !=null)
            {
                this.SetBinding(TitleProperty, new Binding("Title") { Source = p, Mode = BindingMode.TwoWay });
                this.SetBinding(CaptionHeightProperty, new Binding("CaptionHeight") { Source = p, Mode = BindingMode.TwoWay });
                MinimizeCommand = new RelayCommand(() =>
                {
                    p.WindowState = WindowState.Minimized;
                });
                CloseCommand = new RelayCommand(() =>
                {
                    p.Close();
                });
            }
        }
        public OFrame? ChildOFrame { get => GetChildOFrame(this); set => SetChildOFrame(this, value); }
        public object? LeftTitleContent { get => GetLeftTitleContent(this); set => SetLeftTitleContent(this, value); }
        public object? RightTitleContent { get => GetRightTitleContent(this); set => SetRightTitleContent(this, value); }
        public double CaptionHeight { get => GetCaptionHeight(this); set => SetCaptionHeight(this, value); }
        public string Title { get => GetTitle(this); set => SetTitle(this, value); }
        public bool CanMinimize { get => GetCanMinimize(this); set => SetCanMinimize(this, value); }
        public ICommand MinimizeCommand { get => GetMinimizeCommand(this); set => SetMinimizeCommand(this, value); }
        public ICommand CloseCommand { get => GetCloseCommand(this); set => SetCloseCommand(this, value); }
        public ICommand GoBackCommand { get => GetGoBackCommand(this); set => SetGoBackCommand(this, value); }
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
            DependencyProperty.RegisterAttached("CanGoBack", typeof(bool), typeof(OWindowContent), new PropertyMetadata(null));






        public static ICommand GetGoBackCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(GoBackCommandProperty);
        }

        public static void SetGoBackCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(GoBackCommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for GoBackCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GoBackCommandProperty =
            DependencyProperty.RegisterAttached("GoBackCommand", typeof(ICommand), typeof(OWindowContent), new PropertyMetadata(null));


        public static OFrame GetChildOFrame(DependencyObject obj)
        {
            return (OFrame)obj.GetValue(ChildOFrameProperty);
        }

        public static void SetChildOFrame(DependencyObject obj, OFrame value)
        {
            obj.SetValue(ChildOFrameProperty, value);
        }

        // Using a DependencyProperty as the backing store for ChildOFrame.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChildOFrameProperty =
            DependencyProperty.RegisterAttached("ChildOFrame", typeof(OFrame), typeof(OWindowContent), new PropertyMetadata(null));



        public static ICommand GetCloseCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CloseCommandProperty);
        }

        public static void SetCloseCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CloseCommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for CloseCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseCommandProperty =
            DependencyProperty.RegisterAttached("CloseCommand", typeof(ICommand), typeof(OWindowContent), new PropertyMetadata(null));


        public static ICommand GetMinimizeCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(MinimizeCommandProperty);
        }

        public static void SetMinimizeCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(MinimizeCommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for MinimizeCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinimizeCommandProperty =
            DependencyProperty.RegisterAttached("MinimizeCommand", typeof(ICommand), typeof(OWindowContent), new PropertyMetadata(null));


        public static bool GetCanMinimize(DependencyObject obj)
        {
            return (bool)obj.GetValue(CanMinimizeProperty);
        }

        public static void SetCanMinimize(DependencyObject obj, bool value)
        {
            obj.SetValue(CanMinimizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for CanMinimize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanMinimizeProperty =
            DependencyProperty.RegisterAttached("CanMinimize", typeof(bool), typeof(OWindowContent), new PropertyMetadata(true));




        public static string GetTitle(DependencyObject obj)
        {
            return (string)obj.GetValue(TitleProperty);
        }

        public static void SetTitle(DependencyObject obj, string value)
        {
            obj.SetValue(TitleProperty, value);
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.RegisterAttached("Title", typeof(string), typeof(OWindowContent), new PropertyMetadata(""));



        public static double GetCaptionHeight(DependencyObject obj)
        {
            return (double)obj.GetValue(CaptionHeightProperty);
        }

        public static void SetCaptionHeight(DependencyObject obj, double value)
        {
            obj.SetValue(CaptionHeightProperty, value);
        }

        // Using a DependencyProperty as the backing store for CaptionHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CaptionHeightProperty =
            DependencyProperty.RegisterAttached("CaptionHeight", typeof(double), typeof(OWindowContent), new PropertyMetadata(45d));



        public static object GetLeftTitleContent(DependencyObject obj)
        {
            return (object)obj.GetValue(LeftTitleContentProperty);
        }

        public static void SetLeftTitleContent(DependencyObject obj, object value)
        {
            obj.SetValue(LeftTitleContentProperty, value);
        }

        // Using a DependencyProperty as the backing store for LeftTitleContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftTitleContentProperty =
            DependencyProperty.RegisterAttached("LeftTitleContent", typeof(object), typeof(OWindowContent), new PropertyMetadata(null));




        public static object GetRightTitleContent(DependencyObject obj)
        {
            return (object)obj.GetValue(RightTitleContentProperty);
        }

        public static void SetRightTitleContent(DependencyObject obj, object value)
        {
            obj.SetValue(RightTitleContentProperty, value);
        }

        // Using a DependencyProperty as the backing store for RightTitleContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightTitleContentProperty =
            DependencyProperty.RegisterAttached("RightTitleContent", typeof(object), typeof(OWindowContent), new PropertyMetadata(null));


    }
}
