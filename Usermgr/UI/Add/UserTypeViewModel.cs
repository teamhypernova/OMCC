using System.Windows.Input;

namespace OMCC.Plugins.UserManager.UI.Add
{
    public class UserTypeViewModel
    {
        public UserTypeViewModel(UserType userType)
        {
            UserType = userType;
        }

        public UserType UserType { get; set; }
        public string UserTypeKey => UserType.Name.Key;
        public ICommand? AddPageCommand { get; set; }
    }
}
