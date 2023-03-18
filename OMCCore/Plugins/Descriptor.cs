using OMCCore.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMCCore.Plugins
{
    public interface Descriptor
    {
        public Text Name { get; }
        public string Id { get; }
        public Text Description { get; }
        public Plugin Plugin { get; }
    }
}
