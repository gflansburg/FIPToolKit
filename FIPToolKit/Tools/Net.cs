using GMap.NET;
using Newtonsoft.Json;
using RestSharp;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FIPToolKit.Tools
{
    public static class Net
    {
        public static string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/116.0.0.0 Safari/537.36";
        public static int TimeoutMs = 60000;
        static readonly string requestAccept = "*/*";
        static readonly string responseContentType = "image";
        private static bool Win7OrLater = IsRunningOnWin7OrLater();

        public static double Double(this Models.RadioDistance radioDistance)
        {
            switch(radioDistance)
            {
                case Models.RadioDistance.NM50:
                    return 50f;
                case Models.RadioDistance.NM100:
                    return 100f;
                case Models.RadioDistance.NM250:
                    return 250f;
                case Models.RadioDistance.NM500:
                    return 500f;
                default:
                    return -1f;
            }
        }

        public static double DistanceBetween(double lat1, double lon1, double lat2, double lon2)
        {
            double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist = Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) * Math.Cos(rlat2) * Math.Cos(rtheta);

            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;
            return dist * 0.8684; // NM
        }

        static public FlightSim.IP2Location GetLocation(string ipAddress)
        {
            RestClient client = new RestClient("https://cloud.gafware.com/Home");
            RestRequest request = new RestRequest("GetLocation", Method.Get);
            request.AddParameter("ipAddress", ipAddress);
            RestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<FlightSim.IP2Location>(response.Content);
            }
            return null;
        }

        static public string GetIPAddress()
        {
            string address = "0.0.0.0";
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    address = webClient.DownloadString("https://api.ipify.org");
                }
            }
            catch (Exception)
            {
                WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                    {
                        address = stream.ReadToEnd();
                    }
                }
                int first = address.IndexOf("Address: ") + 9;
                int last = address.LastIndexOf("</body>");
                address = address.Substring(first, last - first);
            }
            return address;
        }

        public static string GetURL(string url, ref HttpStatusCode statusCode)
        {
            //CookieCollection cookies = null;
            //return GetURL(url, ref cookies, ref statusCode);
            string responseData = String.Empty;
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
                using (var httpClient = new HttpClient())
                {
                    var response = httpClient.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content;
                        // by calling .Result you are synchronously reading the result
                        responseData = responseContent.ReadAsStringAsync().Result;
                    }
                    statusCode = response.StatusCode;
                }
            }
            catch (WebException ex)
            {
                if (ex.Response.GetType() == typeof(HttpWebResponse))
                {
                    HttpWebResponse response = ex.Response as HttpWebResponse;
                    statusCode = response.StatusCode;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return responseData;
        }

        public static string GetURL(string url)
        {
            CookieCollection cookies = null;
            HttpStatusCode statusCode = HttpStatusCode.OK;
            return GetURL(url, ref cookies, ref statusCode);
            /*using (System.Windows.Forms.WebBrowser wb = new System.Windows.Forms.WebBrowser())
            {
                wb.ScrollBarsEnabled = false;
                wb.ScriptErrorsSuppressed = true;
                wb.Navigate(url);
                while (wb.ReadyState != System.Windows.Forms.WebBrowserReadyState.Complete) { System.Windows.Forms.Application.DoEvents(); }
                return wb.DocumentText;
            }*/
            /*string responseData = String.Empty;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    // by calling .Result you are synchronously reading the result
                    responseData = responseContent.ReadAsStringAsync().Result;
                }
                else if (response.StatusCode == (HttpStatusCode)429)
                {
                    foreach (var header in response.Headers)
                    {
                        if (header.Key.Equals("Retry-After", StringComparison.OrdinalIgnoreCase))
                        {
                            int retry = Convert.ToInt32(header.Value);
                            break;
                        }
                    }
                }
            }
            return responseData;*/
        }

        public static string GetURL(string url, ref CookieCollection cookies, ref HttpStatusCode statusCode)
        {
            return GetURL(url, String.Empty, 0, ref cookies, ref statusCode);
        }

        public static string GetURL(string url, string proxyServer, int proxyPort, ref CookieCollection cookies, ref HttpStatusCode statusCode)
        {
            string strRet = "";
            try
            {
                byte[] data = GetURLFile(url, proxyServer, proxyPort, ref cookies, ref statusCode);
                if (data != null && data.Length > 0)
                {
                    //strRet = System.Text.ASCIIEncoding.ASCII.GetString(data);
                    strRet = Encoding.UTF8.GetString(data);
                }
            }
            catch (WebException e)
            {
                if (e.Response.GetType() == typeof(HttpWebResponse))
                {
                    HttpWebResponse response = e.Response as HttpWebResponse;
                    statusCode = response.StatusCode;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return strRet;
        }

        public static byte[] GetURLFile(string url, string proxyServer, int proxyPort, ref CookieCollection cookies, ref HttpStatusCode statusCode)
        {
            byte[] data = null;
            try
            {
                Uri objURI = new Uri(url);
                if (objURI != null)
                {
                    System.Net.HttpWebRequest objWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(objURI);
                    if (proxyServer.Length > 0)
                    {
                        objWebRequest.Proxy = new WebProxy(proxyServer, proxyPort);
                    }
                    objWebRequest.Timeout = 100000;
                    objWebRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                    objWebRequest.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-us,en;q=0.5");
                    objWebRequest.AllowAutoRedirect = true;
                    objWebRequest.KeepAlive = true;
                    objWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.140 Safari/537.36 Edge/18.17763";
                    objWebRequest.CookieContainer = new CookieContainer();
                    if (cookies != null)
                    {
                        foreach (Cookie cookie in cookies)
                        {
                            objWebRequest.CookieContainer.Add(cookie);
                        }
                    }
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
                    System.Net.WebResponse objWebResponse = objWebRequest.GetResponse();
                    if (objWebResponse.GetType() == typeof(HttpWebResponse))
                    {
                        HttpWebResponse response = objWebResponse as HttpWebResponse;
                        cookies = response.Cookies;
                        statusCode = response.StatusCode;
                    }
                    Stream objStream = objWebResponse.GetResponseStream();
                    BinaryReader objStreamReader = new BinaryReader(objStream);
                    if (objStreamReader != null)
                    {
                        System.IO.MemoryStream ms = new MemoryStream();
                        if (ms != null)
                        {
                            byte[] buf = new byte[1024];
                            int count;
                            while ((count = objStreamReader.Read(buf, 0, 1024)) > 0)
                            {
                                ms.Write(buf, 0, count);
                            }
                            ms.Position = 0;
                            data = new byte[ms.Length];
                            ms.Read(data, 0, data.Length);
                            ms.Close();
                        }
                        objStreamReader.Close();
                    }
                    objStream.Close();
                }
            }
            catch (System.Net.WebException ex)
            {
                var objWebResponse = ex.Response;
                if (objWebResponse != null && objWebResponse.GetType() == typeof(HttpWebResponse))
                {
                    HttpWebResponse response = objWebResponse as HttpWebResponse;
                    statusCode = response.StatusCode;
                }
            }
            catch (Exception)
            {
            }
            return data;
        }

        private static object _lock = new object();
        public static int DeleteTilesOlderThan(string connectionString, DateTime date, int? type)
        {
            int affectedRows = 0;
            try
            {
                lock (_lock)
                {
                    using (var cn = new SQLiteConnection())
                    {
                        cn.ConnectionString = connectionString;
                        cn.Open();
                        using (DbTransaction tr = cn.BeginTransaction())
                        {
                            try
                            {
                                using (DbCommand com = cn.CreateCommand())
                                {
                                    com.CommandText = string.Format("DELETE FROM main.Tiles WHERE CacheTime is not NULL and CacheTime < datetime('{0}')", date.ToString("s"));
                                    if (type.HasValue)
                                    {
                                        com.CommandText += " and Type = " + type;
                                    }
                                    affectedRows = com.ExecuteNonQuery();
                                }
                                tr.Commit();
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("DeleteTilesOlderThan: " + ex.ToString());
                                tr.Rollback();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DeleteTilesOlderThan: " + ex);
            }
            return affectedRows;
        }

        public static long UnixTimeNow()
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }

        public static MemoryStream CopyStream(Stream inputStream, bool seekOriginBegin)
        {
            const int readSize = 32 * 1024;
            byte[] buffer = new byte[readSize];
            var ms = new MemoryStream();
            {
                int count;
                while ((count = inputStream.Read(buffer, 0, readSize)) > 0)
                {
                    ms.Write(buffer, 0, count);
                }
            }
            if (seekOriginBegin)
            {
                inputStream.Seek(0, SeekOrigin.Begin);
            }
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }


        public static bool IsRunningOnWin7OrLater()
        {
            var os = Environment.OSVersion;
            if (os.Platform == PlatformID.Win32NT)
            {
                var vs = os.Version;
                if (vs.Major >= 6 /* && vs.Minor > 0 */)
                {
                    return true;
                }
            }
            return false;
        }

        public static PureImage GetTileImageUsingHttp(CacheManager provider, string url)
        {
            PureImage ret = null;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
            var request = WebRequest.Create(url);
            if (request is HttpWebRequest r)
            {
                r.UserAgent = UserAgent;
                r.ReadWriteTimeout = TimeoutMs * 6;
                r.Accept = requestAccept;
                r.Timeout = TimeoutMs;
            }
            else
            {
                if (!string.IsNullOrEmpty(UserAgent))
                {
                    request.Headers.Add("User-Agent", UserAgent);
                }
                if (!string.IsNullOrEmpty(requestAccept))
                {
                    request.Headers.Add("Accept", requestAccept);
                }
            }
            provider.InitializeWebRequest2(request);
            try
            {
                using (var response = request.GetResponse())
                {
                    if (response.ContentType.Contains(responseContentType))
                    {
                        using (var responseStream = response.GetResponseStream())
                        {
                            var data = CopyStream(responseStream, false);
                            Debug.WriteLine("Response[" + data.Length + " bytes]: " + url);
                            if (data.Length > 0)
                            {
                                try
                                {
                                    System.Drawing.Image m = System.Drawing.Image.FromStream(data, true, !Win7OrLater);
                                    if (m != null)
                                    {
                                        ret = new GMap.NET.WindowsForms.GMapImage { Img = m };
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine("FromStream: " + ex);
                                }
                                if (ret != null)
                                {
                                    ret.Data = data;
                                    ret.Data.Position = 0;
                                }
                                else
                                {
                                    data.Dispose();
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.WriteLine("CheckTileImageHttpResponse[false]: " + url);
                    }
                    response.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("CheckTileImageHttpResponse[false]: " + url + ": " + ex.Message);
            }
            return ret;
        }
    }
}
