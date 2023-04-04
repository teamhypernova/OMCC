using MaterialDesignThemes.Wpf;
using System.Windows;

namespace OMCCore.UI
{
    public class AniCard : Card
    {
        static AniCard()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AniCard), new FrameworkPropertyMetadata(typeof(AniCard)));
        }
    }
}
