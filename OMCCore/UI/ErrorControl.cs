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
    public class ErrorControl : ContentControl
    {
        static ErrorControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ErrorControl), new FrameworkPropertyMetadata(typeof(ErrorControl)));
        }

        public ICommand RefreshCommand { get => GetRefreshCommand(this); set => SetRefreshCommand(this, value); }
        public static ICommand GetRefreshCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(RefreshCommandProperty);
        }

        public static void SetRefreshCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(RefreshCommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for RefreshCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RefreshCommandProperty =
            DependencyProperty.RegisterAttached("RefreshCommand", typeof(ICommand), typeof(ErrorControl), new PropertyMetadata(null));


    }

}
