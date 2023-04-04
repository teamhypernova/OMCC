namespace OMCCore.UI
{
    public interface IPageNavigator
    {
        public void AddPage(OPage page, bool createFrame = false, bool dialog = false);
        public void GoBack();
    }
}
