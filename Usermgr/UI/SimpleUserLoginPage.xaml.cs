using OMCCore.UI;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OMCC.Plugins.UserManager.UI
{
    /// <summary>
    /// SimpleUserLoginPage.xaml 的交互逻辑
    /// </summary>
    public partial class SimpleUserLoginPage : UserLoginPage
    {

        public SimpleUserLoginPage(SimpleUserLoginPageViewModel model)
        {
            InitializeComponent();
            model.LoginPage = this;
            this.DataContext = model;
        }

        public override event UserLoginPageEventHandler? Callback;
        public void RaiseCallbackEvent(bool succeeded,User? user)
        {
            if (Callback != null)
            {
                Callback(succeeded, user);
            }
        }
    }
#if DEBUG
    public sealed class DebugSimpleUserLoginPageViewModel : SimpleUserLoginPageViewModel
    {

        public override User? CreateUser()
        {
            throw new System.NotImplementedException();
        }

    }
#endif
}
