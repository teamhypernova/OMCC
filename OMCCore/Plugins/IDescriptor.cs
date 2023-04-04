using OMCCore.Globalization;
using OMCCore.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMCCore.Plugins
{
    public interface IDescriptor : IId
    {
        public Text Name { get; }
        public Text Description { get; }
        public Plugin Plugin { get; }
    }
}
