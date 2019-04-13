using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _55rCheckerWF
{
    public static class RequsetHelper
    {
        private static HttpWebRequest request = WebRequest.Create("https://eatj8.com") as HttpWebRequest;
        private static WebResponse response;

        public static string GetHtml(string email, string key, string uid, string expire_in)
        {
            try
            {
                CookieContainer cookie = new CookieContainer();
                cookie.Add(new Cookie("email", email, "/", "eatj8.com"));
                cookie.Add(new Cookie("key", key, "/", "eatj8.com"));
                cookie.Add(new Cookie("uid", uid, "/", "eatj8.com"));
                cookie.Add(new Cookie("expire_in", expire_in, "/", "eatj8.com"));
                request.CookieContainer = cookie;
                response = request.GetResponse();
                string result = string.Empty;
                using (Stream dataStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(dataStream, Encoding.UTF8))
                    {
                        result = reader.ReadToEnd() ?? "failed";
                    }
                }
                return result;
            }
            catch
            {
                return "failed";
            }
        }
    }
}
