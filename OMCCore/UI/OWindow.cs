using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shell;

namespace OMCCore.UI
{
    public class OWindow : Window
    {
        public OWindow()
        {
            CaptionHeight = 45;
            Resizable = true;
            WindowChrome.SetWindowChrome(this, WindowChrome);
        }

        #region PRIVATE_PROPERTIES
        WindowChrome _wc = new WindowChrome()
        {
            CaptionHeight = 45,
            ResizeBorderThickness = new Thickness(5),
            GlassFrameThickness = new Thickness(1),
        };
        WindowChrome WindowChrome { get => _wc; set
            {
                _wc = value;
                WindowChrome.SetWindowChrome(this, _wc);
            } }
        #endregion
        #region PUBLIC_PROPERTIES
        public double CaptionHeight { get => GetCaptionHeight(this); set => SetCaptionHeight(this, value); }
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
            DependencyProperty.RegisterAttached("CaptionHeight", typeof(double), typeof(OWindow), new PropertyMetadata(40d, (s, x) =>
            {
                if(s is OWindow oWindow)
                {
                    oWindow.WindowChrome.CaptionHeight = (double)x.NewValue;
                }
            }));




        public bool Resizable { get => GetResizable(this); set => SetResizable(this, value); }
        public static bool GetResizable(DependencyObject obj)
        {
            return (bool)obj.GetValue(ResizableProperty);
        }

        public static void SetResizable(DependencyObject obj, bool value)
        {
            obj.SetValue(ResizableProperty, value);
        }

        // Using a DependencyProperty as the backing store for Resizable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ResizableProperty =
            DependencyProperty.RegisterAttached("Resizable", typeof(bool), typeof(OWindow), new PropertyMetadata(false, (s, x) => 
            {
                if(s is OWindow oWindow)
                {
                    oWindow.WindowChrome.ResizeBorderThickness = (bool)x.NewValue ? new Thickness(5) : new Thickness(0);
                }
            }));


        #endregion
    }
}
