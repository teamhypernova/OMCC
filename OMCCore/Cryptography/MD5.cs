using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace OMCCore.Cryptography
{
    public static class MD5
    {
        public static string GetSHA1(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] retval = sha1.ComputeHash(file);
            file.Close();

            StringBuilder sc = new StringBuilder();
            for (int i = 0; i < retval.Length; i++)
            {
                sc.Append(retval[i].ToString("x2"));
            }
            return sc.ToString();
        }
        static string GetMD5(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] retval = md5.ComputeHash(file);
            file.Close();

            StringBuilder sc = new StringBuilder();
            for (int i = 0; i < retval.Length; i++)
            {
                sc.Append(retval[i].ToString("x2"));
            }
            return sc.ToString();
        }
    }

}
