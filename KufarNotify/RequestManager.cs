using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KufarNotify
{
    public class RequestManager
    {
        private WebRequest CreateRequest()
        {
            WebRequest req = WebRequest.Create("https://re.kufar.by/l/minsk/snyat/kvartiru-dolgosrochno/bez-posrednikov?cur=BYR&fkn=v.and%3A1&flb=v.and%3A3&fli=v.and%3A6&prc=r%3A0%2C70000&size=30&sort=lst.d");
            req.Headers.Add("content-type", "text/html; charset=utf-8");
            req.Headers.Add("vary", " Accept-Encoding");
            req.Headers.Add("cache-control", "private, no-cache, no-store, max-age=0, must-revalidate");
            req.Headers.Add("content-security-policy", "frame-ancestors 'self' *.kufar.by");
            req.Headers.Add("etag", "wwnicmbuk5jw8m");
            req.Headers.Add("set-cookie", "lang=ru; Max-Age=31536000; Domain=.kufar.by; Path=/");
            req.Headers.Add("strict-transport-security", "max-age=600; includeSubDomains");
            req.Headers.Add("x-content-type-options", "nosniff");
            req.Headers.Add("x-dns-prefetch-control", "off");
            req.Headers.Add("x-download-options", "noopen");
            req.Headers.Add("x-frame-options", "SAMEORIGIN");
            req.Headers.Add("x-powered-by", "Next.js");
            req.Headers.Add("x-xss-protection", "1; mode=block");

            CultureInfo culture = new CultureInfo("en-US", false);
            string currentTime = DateTime.Now.AddHours(-3).ToString("ddd, dd MMM yyy HH:mm:ss", culture) + " GMT";

            req.Headers.Add("date", currentTime);
            req.Headers.Add("server", "openresty");
            return req;
        }

        public string GetResponseString()
        {
            try
            {
                var webrequest = CreateRequest();
                WebResponse resp = webrequest.GetResponse();
                Stream stream = resp.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                string response = sr.ReadToEnd();
                return response.ToString();
            }
            catch
            {
                return null;
            }
            
        }
    }
}
