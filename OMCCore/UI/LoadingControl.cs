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
    public class LoadingControl : ContentControl
    {
        static LoadingControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LoadingControl), new FrameworkPropertyMetadata(typeof(LoadingControl)));
        }

        public bool IsLoading { get => GetIsLoading(this); set => SetIsLoading(this, value); }
        public bool IsError { get => GetIsError(this); set => SetIsError(this, value); }
        public string ErrorMessage { get => GetErrorMessage(this); set => SetErrorMessage(this, value); }

        public static bool GetIsLoading(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsLoadingProperty);
        }

        public static void SetIsLoading(DependencyObject obj, bool value)
        {
            obj.SetValue(IsLoadingProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsLoading.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.RegisterAttached("IsLoading", typeof(bool), typeof(LoadingControl), new PropertyMetadata(true));



        public static bool GetIsError(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsErrorProperty);
        }

        public static void SetIsError(DependencyObject obj, bool value)
        {
            obj.SetValue(IsErrorProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsError.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsErrorProperty =
            DependencyProperty.RegisterAttached("IsError", typeof(bool), typeof(LoadingControl), new PropertyMetadata(false));



        public static string GetErrorMessage(DependencyObject obj)
        {
            return (string)obj.GetValue(ErrorMessageProperty);
        }

        public static void SetErrorMessage(DependencyObject obj, string value)
        {
            obj.SetValue(ErrorMessageProperty, value);
        }

        // Using a DependencyProperty as the backing store for ErrorMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ErrorMessageProperty =
            DependencyProperty.RegisterAttached("ErrorMessage", typeof(string), typeof(LoadingControl), new PropertyMetadata(""));


    }
}
