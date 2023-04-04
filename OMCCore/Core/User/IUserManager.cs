using OMCCore.Globalization;
using OMCCore.Model.Data;
using System.Windows;

namespace OMCCore.Core.User
{
    public interface IUserManager : IId
    {
        public Text Name { get; }
        public IImmediateUser[] GetUsers();
        public IImmediateUser? GetSelectedUser();
    }
}
