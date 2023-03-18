using CommunityToolkit.Mvvm.Messaging;
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

namespace OMCCore.Globalization
{
    public class GlobalTextBlock : Control
    {
        static GlobalTextBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GlobalTextBlock), new FrameworkPropertyMetadata(typeof(GlobalTextBlock)));
        }
        public GlobalTextBlock()
        {
            WeakReferenceMessenger.Default.Register<SelectedLanguageChangedMessage>(this, (sender, e) =>
            {
                this.Key = Key;
            });
        }

        public string Text { get => GetText(this); set => SetText(this, value); }
        public static string GetText(DependencyObject obj)
        {
            return (string)obj.GetValue(TextProperty);
        }

        public static void SetText(DependencyObject obj, string value)
        {
            obj.SetValue(TextProperty, value);
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.RegisterAttached("Text", typeof(string), typeof(GlobalTextBlock), new PropertyMetadata(""));


        public string Key { get => GetKey(this); set => SetKey(this, value); }
        public static string GetKey(DependencyObject obj)
        {
            return (string)obj.GetValue(KeyProperty);
        }

        public static void SetKey(DependencyObject obj, string value)
        {
            obj.SetValue(KeyProperty, value);
        }

        // Using a DependencyProperty as the backing store for Key.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KeyProperty =
            DependencyProperty.RegisterAttached("Key", typeof(string), typeof(GlobalTextBlock), new PropertyMetadata("", (x, s) =>
            {
                ((GlobalTextBlock)x).Text = Globalization.GetString((string)s.NewValue);
            }));


    }
    
}
