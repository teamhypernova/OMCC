using OMCCore.UI;

namespace OMCCore.Model.Data
{
    public interface IAddSupported
    {
        public (OPage page, bool createWindow) CreatePage();
    }
}
