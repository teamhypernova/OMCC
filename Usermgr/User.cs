using Newtonsoft.Json.Linq;
using OMCCore.Core.User;
using OMCCore.Globalization;
using System;
using System.Collections.Generic;

namespace OMCC.Plugins.UserManager
{
    public abstract class User : IImmediateUser, IEquatable<User?>
    {
        public abstract string NameImmediate { get; }
        public abstract string UuidImmediate { get; }
        public abstract string TokenImmediate { get; }
        public virtual bool IsCrypted { get; } = false;
        public abstract UserType Type { get; }
        public virtual Text UserType => Type.Name;
        public abstract JObject Serialize();

        public override bool Equals(object? obj)
        {
            return Equals(obj as User);
        }

        public bool Equals(User? other)
        {
            return other is not null &&
                   GetUName == other.GetUName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(GetUName);
        }

        public virtual string GetUName => UuidImmediate + Type.Id;

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
