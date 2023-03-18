using System.Windows;
using System.Windows.Media;

namespace OMCCore.UI
{
    public static class VisualTreeHelperExtend
    {
        public static T? FindParent<T>(DependencyObject obj)
            where T : DependencyObject
        {
            var p = VisualTreeHelper.GetParent(obj);
            if (p != null)
            {
                if (p is T t) return t;
                return FindParent<T>(p);
            }
            else
            {
                return null;
            }
        }
    }
}
