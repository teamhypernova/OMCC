using System.IO;
using System.Text;

namespace EDGW.Logging
{
    internal class WhiteWriter : TextWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
        public override void Write(string? value)
        {

        }
    }
}