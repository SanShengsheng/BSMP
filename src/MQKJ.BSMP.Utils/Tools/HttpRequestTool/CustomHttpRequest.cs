using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MQKJ.BSMP.Utils.Tools.HttpRequestTool
{
    public static class CustomHttpRequest
    {
        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受
        }

        public static string CreatePostHttpResponse(string url,IDictionary<string,string> parameters)
        {
            HttpWebRequest request = null;

            string result = string.Empty;

            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

            request = WebRequest.Create(url) as HttpWebRequest;

            request.ProtocolVersion = HttpVersion.Version10;

            request.Method = "POST";

            request.Timeout = 15000;

            request.ContentType = "application/x-www-form-urlencoded";

            request.UserAgent = DefaultUserAgent;

            request.Proxy = null;//默认会使用代理 关掉

            if (parameters != null || parameters.Count != 0)
            {
                StringBuilder buffer = new StringBuilder();

                int i = 0;

                foreach (var key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}",key,parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    }
                    i++;
                }

                byte[] data = Encoding.UTF8.GetBytes(buffer.ToString());

                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }

            return GetResponseString(request.GetResponse() as HttpWebResponse);
        }

        /// <summary>  
        /// 模拟POST提交  
        /// </summary>  
        /// <param name="url">请求地址</param>  
        /// <param name="xmlParam">xml参数</param>  
        /// <returns>返回结果</returns>  
        public static string PostHttpResponse(string url, string parmas)
        {
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            myHttpWebRequest.Method = "POST";
            myHttpWebRequest.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

            // Encode the data  
            byte[] encodedBytes = Encoding.UTF8.GetBytes(parmas);
            myHttpWebRequest.ContentLength = encodedBytes.Length;

            // Write encoded data into request stream  
            Stream requestStream = myHttpWebRequest.GetRequestStream();
            requestStream.Write(encodedBytes, 0, encodedBytes.Length);
            requestStream.Close();

            HttpWebResponse result;

            try
            {
                result = (HttpWebResponse)myHttpWebRequest.GetResponse();
            }
            catch
            {
                return string.Empty;
            }

            if (result.StatusCode == HttpStatusCode.OK)
            {
                using (Stream mystream = result.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(mystream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            return null;
        }

        public static string GetResponseString(HttpWebResponse response)
        {
            using (Stream s = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(s, Encoding.UTF8);

                return reader.ReadToEnd();
            }
        }

        public static string CreateHttpGetRequest(string url,IDictionary<string,string> parameters)
        {
            HttpWebRequest request = null;

            string result = string.Empty;

            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

            if (parameters != null || parameters.Count != 0)
            {
                StringBuilder buffer = new StringBuilder();

                buffer.Append(url);

                int i = 0;

                foreach (var key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("?{0}={1}", key, parameters[key]);
                    }
                    i++;
                }

                request = (HttpWebRequest)WebRequest.Create(buffer.ToString());

                request.Method = "GET";

                request.ContentType = "text/html;charset=UTF-8";

                HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

                Stream stream = resp.GetResponseStream();

                try
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        result = reader.ReadToEnd();
                    }
                }
                finally
                {
                    stream.Close();
                }
            }

            return result;
        }

        //public string CreateHttpGetRequestAsync()
        //{
        //    using (var client = new HttpClient)
        //    {
        //        client.GetStringAsync()
        //    }
        //}
    }
}
