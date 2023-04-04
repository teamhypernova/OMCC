using System;
using System.Windows;

namespace OMCCore.Model.Data
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]

    public class SizeRecommended : System.Attribute
    {
        public SizeRecommended(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}
