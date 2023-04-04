using Newtonsoft.Json.Linq;
using OMCCore.Globalization;
using OMCCore.Model.Data;
using System.Windows.Documents;

namespace OMCC.Plugins.UserManager
{
    public abstract class UserType : IId
    {
        public abstract Text Name { get; }
        public abstract string Id { get; }
        public abstract User Parse(JObject data);
        public abstract UserLoginPage CreatePage();

    }

}
