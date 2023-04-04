using System.Windows;
using System.Windows.Media;

namespace OMCCore.UI
{
    public static class VisualTreeHelperExtend
    {
        public static T? FindParent<T>(FrameworkElement obj)
            where T : FrameworkElement
        {
            var p = obj.Parent as FrameworkElement;
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
