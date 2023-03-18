using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMCCore.Globalization
{
    public sealed class Text
    {
        public Text(string key)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
        }

        public string Key { get; set; }
        public string Content => Globalization.GetString(Key);
        public override string ToString() => Content;
    }
}
