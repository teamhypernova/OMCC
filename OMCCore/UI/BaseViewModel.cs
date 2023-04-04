using CommunityToolkit.Mvvm.ComponentModel;
using OMCCore.Core.Game.UI;
using OMCCore.Core.User.UI;

namespace OMCCore.UI
{
    public abstract class BaseViewModel : ObservableObject
    {
        static UserInfoViewModel _u = new UserInfoViewModel();
        static GameInfoViewModel _g = new GameInfoViewModel();
        public UserInfoViewModel UserInfo => _u;
        public GameInfoViewModel GameInfo => _g;
    }
}
