using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMCCore.Cryptography
{
    public static class Base64
    {
        public static string EncodeBase64(string code)
        {
            string encode = "";
            byte[] bytes = Encoding.UTF8.GetBytes(code);
            return Convert.ToBase64String(bytes);
        }
        ///解码
        public static string DecodeBase64(string code)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);
            return Encoding.UTF8.GetString(bytes);
        }
    }

}
