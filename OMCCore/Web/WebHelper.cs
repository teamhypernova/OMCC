using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OMCCore.Web
{
    public static class WebHelper
    {
        public static string GetData(string url)
        {
            return GetData(url, new NameValueCollection());
        }
        public static string GetData(string url, NameValueCollection namevalue)
        {
            string responseData = "";
            if (namevalue.Count > 0)
            {
                string para = "";
                for (int i = 0; i < namevalue.Count; i++)
                {
                    para += string.Format("&{0}={1}", namevalue.GetKey(i), namevalue.Get(i));
                }
                para = "?" + para.TrimStart('&');
                url += para;
                //get请求需要把 url?para1=11&para2=22 补充上
            }

            using (var client = new WebClient())
            {
                byte[] data = client.DownloadData(url);
                responseData = Encoding.UTF8.GetString(data);
            }
            return responseData;
        }

        public static string PostData(string url, NameValueCollection namevalue)
        {
            string responseData = "";
            using (var client = new WebClient())
            {
                byte[] bytes = client.UploadValues(url, namevalue);
                responseData = Encoding.UTF8.GetString(bytes);
            }
            return responseData;
        }

    }
}
