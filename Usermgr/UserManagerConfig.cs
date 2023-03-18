using Newtonsoft.Json.Linq;
using OMCCore.Model.Data;
using System;
using System.Collections.Generic;

namespace OMCC.Plugins.UserManager
{
    public class UserManagerConfig : ConfigFile
    {
        public JArray UserObjects { get; set; } = new JArray();
        public Guid CryptoKey { get; set; } = Guid.NewGuid();
    }
}
