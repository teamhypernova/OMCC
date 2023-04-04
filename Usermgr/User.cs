using Newtonsoft.Json.Linq;
using OMCCore.Core.User;
using OMCCore.Globalization;
using OMCCore.Model.Data;
using System;
using System.Collections.Generic;

namespace OMCC.Plugins.UserManager
{
    public abstract class User : IImmediateUser, IEquatable<User?>, IAdditionalString
    {
        public abstract string NameImmediate { get; }
        public abstract string UuidImmediate { get; }
        public abstract string TokenImmediate { get; }
        public virtual bool IsCrypted { get; } = false;
        public abstract UserType Type { get; }
        public virtual Text UserType => Type.Name;
        public abstract JObject Serialize();
        public bool IsSelected 
        {
            get
            {
                string name = UniqueName;
                string confName = Configs.GetConfig<UserManagerConfig>().SelectedId;
                //TODO : 当confName = ""或null的时候的自动识别
                if (name == confName)
                    return true;
                return false;
            }
        }
        public override bool Equals(object? obj)
        {
            return Equals(obj as User);
        }

        public bool Equals(User? other)
        {
            return other is not null &&
                   UniqueName == other.UniqueName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UniqueName);
        }

        public virtual string UniqueName => UuidImmediate + Type.Id;

        public string AdditionalString => UserType.Content;

        public static bool operator ==(User? left, User? right)
        {
            return EqualityComparer<User>.Default.Equals(left, right);
        }

        public static bool operator !=(User? left, User? right)
        {
            return !(left == right);
        }
    }
}
