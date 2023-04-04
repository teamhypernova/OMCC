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

        void Update()
        {
            var paramsList = new List<string>();
            if (Param1 != null)
            {
                paramsList.Add(Param1);
                if (Param2 != null)
                {
                    paramsList.Add(Param2);
                    if (Param3 != null)
                    {
                        paramsList.Add(Param3);
                        if (Param4 != null)
                        {
                            paramsList.Add(Param4);
                        }
                    }
                }
            }
            if (paramsList.Any())
            {
                Text = Globalization.GetString(Key, paramsList.ToArray());
            }
            else
            {
                Text = Globalization.GetString(Key);
            }
        }
        #region PROPERTIES
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
                ((GlobalTextBlock)x).Update();
            }));

        public string Param1 { get => GetParam1(this); set => SetParam1(this, value); }
        public static string GetParam1(DependencyObject obj)
        {
            return (string)obj.GetValue(Param1Property);
        }

        public static void SetParam1(DependencyObject obj, string value)
        {
            obj.SetValue(Param1Property, value);
        }

        // Using a DependencyProperty as the backing store for Param1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Param1Property =
            DependencyProperty.RegisterAttached("Param1", typeof(string), typeof(GlobalTextBlock), new PropertyMetadata(null, (x, s) =>
            {
                ((GlobalTextBlock)x).Update();
            }));




        public string Param2 { get => GetParam2(this); set => SetParam2(this, value); }
        public static string GetParam2(DependencyObject obj)
        {
            return (string)obj.GetValue(Param2Property);
        }

        public static void SetParam2(DependencyObject obj, string value)
        {
            obj.SetValue(Param2Property, value);
        }

        // Using a DependencyProperty as the backing store for Param2.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Param2Property =
            DependencyProperty.RegisterAttached("Param2", typeof(string), typeof(GlobalTextBlock), new PropertyMetadata(null, (x, s) =>
            {
                ((GlobalTextBlock)x).Update();
            }));




        public string Param3 { get => GetParam3(this); set => SetParam3(this, value); }
        public static string GetParam3(DependencyObject obj)
        {
            return (string)obj.GetValue(Param3Property);
        }

        public static void SetParam3(DependencyObject obj, string value)
        {
            obj.SetValue(Param3Property, value);
        }

        // Using a DependencyProperty as the backing store for Param3.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Param3Property =
            DependencyProperty.RegisterAttached("Param3", typeof(string), typeof(GlobalTextBlock), new PropertyMetadata(null, (x, s) =>
            {
                ((GlobalTextBlock)x).Update();
            }));




        public string Param4 { get => GetParam4(this); set => SetParam4(this, value); }
        public static string GetParam4(DependencyObject obj)
        {
            return (string)obj.GetValue(Param4Property);
        }

        public static void SetParam4(DependencyObject obj, string value)
        {
            obj.SetValue(Param4Property, value);
        }

        // Using a DependencyProperty as the backing store for Param4.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Param4Property =
            DependencyProperty.RegisterAttached("Param4", typeof(string), typeof(GlobalTextBlock), new PropertyMetadata(null, (x, s) =>
            {
                ((GlobalTextBlock)x).Update();
            }));

        #endregion
    }

}
