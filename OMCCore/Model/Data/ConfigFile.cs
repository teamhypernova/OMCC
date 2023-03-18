using Newtonsoft.Json;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OMCCore.Model.Data
{
    public abstract class ConfigFile : IStringSerializable
    {
        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
