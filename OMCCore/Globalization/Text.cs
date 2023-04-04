using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMCCore.Globalization
{
    public sealed class Text
    {
        public Text(string key, params string[] parameters)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Params = new List<string>(parameters);
        }

        public string Key { get; set; }
        public List<string> Params { get; } = new List<string>();
        public string Content => Globalization.GetString(Key, Params.ToArray());
        public override string ToString() => Content;
    }
}
