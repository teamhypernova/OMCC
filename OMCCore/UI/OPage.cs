using System.Windows.Controls;

namespace OMCCore.UI
{
    public class OPage : Grid
    {
        public string Title { get; set; } = "";
        public OFrame? Frame { get; set; }
        public void GoBack()
        {
            Frame?.GoBack();
        }
        public void AddPage(OPage page) => AddPage(page, false, false);
        public void AddPage(OPage page, bool createFrame = false, bool dialog = false)
        {
            if (!createFrame)
            {
                Frame?.AddPage(page);
            }
            else
            {
                var nv = new NavigationWindow();
                nv.Frame.SelectedPage = page;
                if (dialog)
                {
                    nv.ShowDialog();
                }
                else
                {
                    nv.Show();
                }
            }
        }
    }
}
