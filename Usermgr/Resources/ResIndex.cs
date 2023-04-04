using OMCCore.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace OMCC.Plugins.UserManager.Resources
{
    public static class ResIndex
    {
        static ImageSource tosource(Func<Bitmap> map)
        {
#pragma warning disable CS8600
#pragma warning disable CS8603
            ImageSource source = null;
            Application.Current.Dispatcher.Invoke(() =>
            {
                source = map().ToBitmapSource();
            });
            return source;
#pragma warning restore CS8603
#pragma warning restore CS8600
        }
        public static ImageSource steve { get; } = tosource(() => Images.steve);
    }
}
