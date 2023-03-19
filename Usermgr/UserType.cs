using Newtonsoft.Json.Linq;
using OMCCore.Globalization;

namespace OMCC.Plugins.UserManager
{
    public abstract class UserType
    {
        public abstract Text Name { get; }
        public abstract string Id { get; }
        public abstract User Parse(JObject data);
    }
}
