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
    public class OCard : ContentControl
    {
        static OCard()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(OCard), new FrameworkPropertyMetadata(typeof(OCard)));
        }
        public object Title { get => GetTitle(this); set => SetTitle(this, value); }

        public static object GetTitle(DependencyObject obj)
        {
            return (object)obj.GetValue(TitleProperty);
        }

        public static void SetTitle(DependencyObject obj, object value)
        {
            obj.SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.RegisterAttached("Title", typeof(object), typeof(OCard), new PropertyMetadata(null));

    }
}
