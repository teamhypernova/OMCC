using OMCCore.Model.Data;
using System.Collections.Generic;

namespace OMCCore.Core
{
    public class SelectorConfig : ConfigFile
    {
        public Dictionary<string, string> Selections { get; set; } = new Dictionary<string, string>();
    }
}
