using System.Windows.Media;

namespace OMCCore.Model.Data
{
    public interface IDelayeIcon : IImmediateIcon
    {
        public ImageSource IconDelayed { get; }
    }
}
