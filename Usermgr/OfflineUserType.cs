using MaterialDesignThemes.Wpf;
using Newtonsoft.Json.Linq;
using OMCC.Plugins.UserManager.UI;
using OMCCore.Globalization;
using System;

namespace OMCC.Plugins.UserManager
{
    public sealed class OfflineUserType : UserType
    {
        public static OfflineUserType OFFLINE { get; } = new OfflineUserType();
        public static OfflineUserType OFFLINE2 { get; } = new OfflineUserType("offline2");

        public override Text Name => new Text("plugin.official.usermgr.offlineuser.name");
        public string id = "offline";

        public OfflineUserType(string id)
        {
            this.id = id;
        }
        public OfflineUserType()
        {
            this.id = "";
        }

        public override string Id => id;

        public override User Parse(JObject data)
        {
            var name = data["name"];
            if (name != null)
            {
                return new OfflineUser(name.ToString());
            }
            else
            {
                throw new ArgumentNullException(nameof(name));
            }
        }

        public override UserLoginPage CreatePage()
        {
            return new SimpleUserLoginPage(new OfflineUserLoginPageViewModel());
        }
        public class OfflineUserLoginPageViewModel : SimpleUserLoginPageViewModel
        {
            public OfflineUserLoginPageViewModel()
            {
                this.Title = new Text("plugin.official.usermgr.offlineuser.name");
                this.Questions.Add(USERNAME = new QuestionModel(new Text("plugin.official.usermgr.username.text"), PackIconKind.AccountOutline));
            }
            QuestionModel USERNAME;
            public override User? CreateUser()
            {
                return new OfflineUser(USERNAME.Text);
            }
            public override ValidationInfo Validate(QuestionModel model)
            {
                if(model == USERNAME)
                {
                    if (model.Text.Trim() == "")
                    {
                        return new ValidationInfo(false, new Text("plugin.official.usermgr.public.usrname_cannot_be_null"));
                    }else if(model.Text.Contains(" ") || model.Text.Contains("\""))
                    {
                        return new ValidationInfo(false, new Text("plugin.official.usermgr.public.usrname_cannot_con_spa_q"));
                    }
                    else
                    {
                        return new ValidationInfo(true);
                    }
                }
                else
                {
                    return new ValidationInfo(true);
                }
            }
        }
    }
}
