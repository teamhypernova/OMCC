using OMCCore.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMCCore.Core.User
{
    public interface IImmediateUser
    {
        public string NameImmediate { get; }
        public string UuidImmediate { get; }
        public string TokenImmediate { get; }
        public Text UserType { get; }
        public UserInfo CreateUserInfo();
    }
}
