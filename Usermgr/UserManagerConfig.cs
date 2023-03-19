using Newtonsoft.Json.Linq;
using OMCCore.Model.Data;
using System;
using System.Collections.Generic;

namespace OMCC.Plugins.UserManager
{
    public class UserManagerConfig : ConfigFile
    {
        public JObject UserObjects { get; set; } = new JObject();
        public Guid CryptoKey { get; set; } = Guid.NewGuid();
        public 
    }
}
