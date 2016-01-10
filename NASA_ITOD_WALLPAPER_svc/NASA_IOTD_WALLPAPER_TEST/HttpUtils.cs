using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace NASA_IOTD_WALLPAPER_TEST
{
    class HttpUtils
    {

        public HttpUtils()
        {

        }

        ~HttpUtils()
        {

        }
        public bool CheckConnection(Uri uri)
        {
            return CheckConnection(uri.AbsolutePath);
        }

        public bool CheckConnection(String URL)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Timeout = 5000;
                request.Credentials = CredentialCache.DefaultNetworkCredentials;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
