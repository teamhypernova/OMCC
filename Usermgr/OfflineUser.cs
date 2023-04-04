using Newtonsoft.Json.Linq;
using OMCC.Plugins.UserManager.Resources;
using OMCCore.Model.Data;
using System;
using System.Windows.Media;
using Vlingo.Xoom.UUID;

namespace OMCC.Plugins.UserManager
{
    public sealed class OfflineUser : User, IImmediateIcon
    {
        static NameBasedGenerator gen = new NameBasedGenerator();

        public OfflineUser(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public override string NameImmediate => Name;

        public override string UuidImmediate => Uuid;

        public override string TokenImmediate => Token;
        public override bool IsCrypted => true;
        public string Name { get; set; }
        public string Uuid => gen.GenerateGuid($"OfflinePlayer:{Name}").ToString();
        public string Token => Uuid;
        public override UserType Type => OfflineUserType.OFFLINE;

        public ImageSource IconImmediate => ResIndex.steve;

        public override JObject Serialize()
        {
            JObject obj = new JObject();
            obj["name"] = Name;
            return obj;
        }
    }
}
