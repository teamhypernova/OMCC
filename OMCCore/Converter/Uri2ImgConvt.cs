using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace OMCCore.Converter
{
    public class Uri2ImgConvt : IValueConverter
    {
        static Dictionary<Uri, BitmapImage> imgs = new Dictionary<Uri, BitmapImage>();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (value is Uri u)
            {
                if (imgs.ContainsKey(u)) return imgs[u];
                var i = new BitmapImage(u);
                imgs[u] = i;
                return i;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
