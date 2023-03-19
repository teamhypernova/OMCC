using OMCCore.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace OMCC.Plugins.UserManager.Resources
{
    public static class ResIndex
    {
        public static ImageSource steve { get; } = ImageHelper.ToBitmapSource(Images.steve);
    }
}
