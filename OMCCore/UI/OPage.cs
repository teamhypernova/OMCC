using OMCCore.Model.Data;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace OMCCore.UI
{
    public class OPage : Grid, IPageNavigator
    {
        public OPage()
        {
            this.DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is BasePageViewModel p)
            {
                p.Page = this;
            }
        }

        public string Title { get => GetTitle(this); set => SetTitle(this, value); }
        public OFrame? Frame { get; set; }
        public void GoBack()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Frame?.GoBack();

            });
        }
        public void AddPage(OPage page) => AddPage(page, false, false);
        public void AddPage(OPage page, bool createFrame = false, bool dialog = false)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (!createFrame)
                {
                    Frame?.AddPage(page);
                }
                else
                {
                    var nv = new NavigationWindow();
                    nv.Frame.SelectedPage = page;
                    if (dialog)
                    {
                        var type = page.GetType();
                        var att = type.GetCustomAttribute<SizeRecommended>();
                        if (att != null)
                        {
                            nv.Width = att.Width;
                            nv.Height = att.Height;
                            nv.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        }
                        nv.ShowDialog();
                    }
                    else
                    {
                        nv.Show();
                    }
                }
            });
        }


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
            DependencyProperty.RegisterAttached("Title", typeof(string), typeof(OPage), new PropertyMetadata("", (x, s) =>
            {
                var pg = (OPage)x;
                pg.Frame?.SetTitle(s.NewValue.ToString() ?? "");
            }));


    }
}
