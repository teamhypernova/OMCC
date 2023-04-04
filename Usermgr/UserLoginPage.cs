using OMCC.Plugins.UserManager;
using OMCCore.UI;

namespace OMCC.Plugins.UserManager
{
    public abstract class UserLoginPage : OPage
    {

        public UserLoginPage()
        {

        }
        public abstract event UserLoginPageEventHandler? Callback;
    }

    public delegate void UserLoginPageEventHandler(bool succeeded, User? user);

}
