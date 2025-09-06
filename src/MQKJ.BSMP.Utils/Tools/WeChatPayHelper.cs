using MQKJ.BSMP.Utils.WechatPay.Dtos;
using MQKJ.BSMP.Utils.WechatPay.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json.Linq;

namespace MQKJ.BSMP.Utils.Tools
{
    public class WeChatPayHelper
    {

        public static Random random = new Random();

        /// <summary>
        /// 随机生成Noncestr
        /// </summary>
        /// <returns></returns>
        public static string GetNoncestr()
        {
            return EncryptHelper.GetMD5(Guid.NewGuid().ToString(), "UTF-8");
        }

        /// <summary>
        /// 采用系统当前的硬件信息、进程信息、线程信息、系统启动时间为填充英子 耗资源
        /// </summary>
        /// <returns></returns>
        public static string GenerateRandom(int length)
        {
            var count = Convert.ToInt32(Math.Ceiling(length / 2.0));
            byte[] randomBytes = new byte[count];
            RNGCryptoServiceProvider rngServiceProvider = new RNGCryptoServiceProvider();
            rngServiceProvider.GetBytes(randomBytes);
            string result = BitConverter.ToString(randomBytes, 0);
            return result;
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateRandomData(int length)
        {
            StringBuilder builder = new StringBuilder();

            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                builder.Append(random.Next(10));
            }

            return builder.ToString();
        }


        /// <summary>
        /// 随机生成订单号
        /// </summary>
        /// <returns></returns>
        public static string GenerateOutTradeNo()
        {
            var ran = new Random();
            return $"{UnixStamp()}{ran.Next(999)}";
        }

        /// <summary>
        /// 循环获取一个实体类每个字段的XmlAttribute属性的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDictionaryByType<T>(T model)
        {
            var dict = new Dictionary<string, string>();

            var type = model.GetType(); //获取类型

            var props = typeof(T).GetProperties();
            foreach (var prop in props)
            {
                var attrs = prop.GetCustomAttributes(true);
                XmlElementAttribute attr = null;
                if (attrs.Length > 0) attr = attrs[0] as XmlElementAttribute;
                var property = type.GetProperty(prop.Name);
                var value = property.GetValue(model, null); //获取属性值
                dict.Add(attr?.ElementName ?? property.Name, value?.ToString());
            }

            return dict;
        }
        /// 不带证书的请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postdata"></param>
        /// <param name="cer"></param>
        /// <returns></returns>
        private static string HttpPost(string url, string postdata)
        {
            var httpClientHandler = new HttpClientHandler();
            using (HttpClient httpClient = new HttpClient(httpClientHandler))
            {
                //AppContext.SetSwitch("System.Net.Http.UseSocketsHttpHandler", false);
                HttpContent httpContent = new StringContent(postdata);
                httpClient.Timeout = TimeSpan.FromSeconds(60);
                httpContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                var response = httpClient.PostAsync(url, httpContent).Result;
                var body = string.Empty;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return body;
                }
                else
                {
                    body = response.Content.ReadAsStringAsync().Result;
                }

                return body;
            }
        }
        public static Output HttpPost<Output>(string url, object postData)
        {
            var postDataStr = JsonConvert.SerializeObject(postData);
            var response = HttpPost(url, postDataStr);
            var responseObj = JObject.Parse(response);
            var dataStr = responseObj.SelectToken("result.items").ToString();
            return JsonConvert.DeserializeObject<Output>(dataStr);
        }
        /// <summary>
        ///     带证书的post
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <param name="cer"></param>
        /// <param name="result"></param>
        /// <param name="serializeStrFunc"></param>
        /// <param name="inputDataType"></param>
        /// <param name="outDataType"></param>
        /// <returns></returns>
        public static T HttpPost<T>(string url, IDictionary<string,string>paramDic, X509Certificate cer, out string result,
            Func<string, string> serializeStrFunc = null, WebRequestDataTypes inputDataType = WebRequestDataTypes.XML,
            WebRequestDataTypes outDataType = WebRequestDataTypes.XML) where T : class
        {
            string postStr = null;
            switch (inputDataType)
            {
                case WebRequestDataTypes.XML:
                    postStr = XmlHelper.CreateXmlParam(paramDic);
                    break;
                default:
                    postStr = JsonConvert.SerializeObject(paramDic);
                    break;
            }

            if (serializeStrFunc != null)
                postStr = serializeStrFunc(postStr);
            //result = HttpPost(url, postStr, cer.SubjectName.Name);
            result = HttpPost(url, postStr, cer);
            //Console.WriteLine($"企业付款结果：{result}");
            switch (outDataType)
            {
                case WebRequestDataTypes.XML:
                    return XmlHelper.DeserializeObject<T>(result);
                default:
                    return JsonConvert.DeserializeObject<T>(result);
            }
        }

        /// <summary>
        ///     带证书的post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postdata"></param>
        /// <param name="cer"></param>
        /// <returns></returns>
        //private static string HttpPost(string url, string postdata, string subjectName)
        private static string HttpPost(string url, string postdata, X509Certificate cer)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                //var cert = GetMyX509Certificate(subjectName);
                handler.ClientCertificates.Add(cer);
                using (HttpClient httpClient = new HttpClient(handler))
                {
                    HttpContent httpContent = new StringContent(postdata);

                    var response = httpClient.PostAsync(url, httpContent);

                    var body = response.Result.Content.ReadAsStringAsync().Result;

                    return body;
                }
            }
            //var request = CreateWebRequest(url);
            //request.ClientCertificates.Add(cer);
            //request.Method = "POST";
            //if (!string.IsNullOrWhiteSpace(postdata))
            //{
            //    var bytesToPost = Encoding.UTF8.GetBytes(postdata);
            //    //request.ContentLength = bytesToPost.Length;
            //    request.KeepAlive = false;
            //    request.ProtocolVersion = HttpVersion.Version10;
            //    request.Proxy = null;
            //    using (var requestStream = request.GetRequestStream())
            //    {
            //        requestStream.Write(bytesToPost, 0, bytesToPost.Length);
            //        requestStream.Close();
            //    }
            //}

            //using (var response = (HttpWebResponse)request.GetResponse())
            //{
            //    using (var sr = new StreamReader(response.GetResponseStream()))
            //    {
            //        var result = sr.ReadToEnd();
            //        return result;
            //    }
            //}
        }

        internal static X509Certificate2 GetMyX509Certificate(string certName)
        {
            X509Store store = new X509Store(StoreName.CertificateAuthority, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection x509Certificates = store.Certificates.Find(X509FindType.FindBySubjectName, certName, false);

            X509Certificate2 x509Certificate2 = new X509Certificate2();

            foreach (var item in store.Certificates)
            {
                if (item.SubjectName.Name == certName)
                {
                    Console.WriteLine("成功");
                    x509Certificate2 = item;
                    break;
                }
            }
            return x509Certificate2;
        }

        protected static HttpWebRequest CreateWebRequest(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.UseDefaultCredentials = true;
            SetCertificatePolicy();
            return request;
        }

        //注册证书验证回调事件，在请求之前注册
        private static void SetCertificatePolicy()
        {
            ServicePointManager.ServerCertificateValidationCallback
                += RemoteCertificateValidate;
        }
        /// <summary>
        ///     远程证书验证，固定返回true
        /// </summary>
        private static bool RemoteCertificateValidate(object sender, X509Certificate cert,
            X509Chain chain, SslPolicyErrors error)
        {
            return true;
        }

        /// <summary>
        /// post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <param name="serializeStrFunc"></param>
        /// <returns></returns>
        public static T PostXML<T>(string url, object obj, Func<string, string> serializeStrFunc = null)
            where T : WechatPayOutputBase
        {
            var wr = new WeChatApiWebRequestHelper();
            string resultStr = null;
            var result = wr.HttpPost<T>(url, obj, out resultStr, serializeStrFunc,
                WebRequestDataTypes.XML, WebRequestDataTypes.XML);
            if (result != null)
                result.DetailResult = resultStr;
            return result;
        }

        /// <summary>
        ///     接收post数据
        /// </summary>
        /// <returns></returns>
        public static string PostInput(Stream stream)
        {
            var count = 0;
            var buffer = new byte[1024];
            var builder = new StringBuilder();
            while ((count = stream.Read(buffer, 0, 1024)) > 0)
                builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
            return builder.ToString();
        }

        /// <summary>
        /// 获取微信时间格式
        /// </summary>
        /// <returns></returns>
        public static string GetTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// 对字符串进行URL编码
        /// </summary>
        /// <param name="instr"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string UrlEncode(string instr, string charset)
        {
            //return instr;
            if (instr == null || instr.Trim() == "")
                return "";
            else
            {
                //string res;

                try
                {
#if NET35 || NET40 || NET45 || NET461
                    return System.Web.HttpUtility.UrlEncode(instr, Encoding.GetEncoding(charset));
#else
                    return WebUtility.UrlEncode(instr);
#endif
                }
                catch (Exception)
                {
#if NET35 || NET40 || NET45 || NET461
                    return System.Web.HttpUtility.UrlEncode(instr, Encoding.GetEncoding("GB2312"));
#else
                    return WebUtility.UrlEncode(instr);
#endif
                }

                //return res;
            }
        }

        /// <summary>
        /// 对字符串进行URL解码
        /// </summary>
        /// <param name="instr"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string UrlDecode(string instr, string charset)
        {
            if (instr == null || instr.Trim() == "")
                return "";
            else
            {
                //string res;

                try
                {
#if NET35 || NET40 || NET45 || NET461
                    return System.Web.HttpUtility.UrlDecode(instr, Encoding.GetEncoding(charset));
#else
                    return WebUtility.UrlDecode(instr);
#endif
                }
                catch (Exception)
                {
#if NET35 || NET40 || NET45 || NET461
                    return System.Web.HttpUtility.UrlDecode(instr, Encoding.GetEncoding("GB2312"));
#else
                    return WebUtility.UrlDecode(instr);
#endif
                }
                //return res;

            }
        }


        /// <summary>
        /// 取时间戳生成随即数,替换交易单号中的后10位流水号
        /// </summary>
        /// <returns></returns>
        public static UInt32 UnixStamp()
        {
#if NET35 || NET40 || NET45 || NET461
            TimeSpan ts = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
#else
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1);
#endif
            return Convert.ToUInt32(ts.TotalSeconds);
        }

        /// <summary>
        /// 取随机数
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string BuildRandomStr(int length)
        {
            int num;

            lock (random)
            {
                num = random.Next();
            }

            string str = num.ToString();

            if (str.Length > length)
            {
                str = str.Substring(0, length);
            }
            else if (str.Length < length)
            {
                int n = length - str.Length;
                while (n > 0)
                {
                    str = str.Insert(0, "0");
                    n--;
                }
            }

            return str;
        }

        /// <summary>
        /// 创建当天内不会重复的数字
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string BuildDailyRandomStr(int length)
        {
            var stringFormat = DateTime.Now.ToString("HHmmss0000");//共10位

            return stringFormat;
        }

        /// <summary>
        /// 生成签名 获取签名数据
        /// </summary>
        /// <param name="strParam"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetSignInfo(IDictionary<string, string> strParam, string key)
        {
            int i = 0;
            string sign = string.Empty;
            StringBuilder sb = new StringBuilder();
            try
            {
                foreach (KeyValuePair<string, string> temp in strParam)
                {
                    if (temp.Value == "" || temp.Value == null || temp.Key.ToLower() == "sign")
                    {
                        continue;
                    }
                    i++;
                    sb.Append(temp.Key.Trim() + "=" + temp.Value.Trim() + "&");
                }
                sb.Append("key=" + key.Trim() + "");

                sign = EncryptHelper.EncryptWithMD5(sb.ToString()).ToUpper();
            }
            catch (Exception)
            {
                //Utility.AddLog("PayHelper", "GetSignInfo", ex.Message, ex);
            }
            return sign;
        }

        /// <summary>
        /// 返回通知 XML
        /// </summary>
        /// <param name="returnCode"></param>
        /// <param name="returnMsg"></param>
        /// <returns></returns>
        public static string GetReturnXml(string returnCode, string returnMsg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<xml>");
            sb.Append("<return_code><![CDATA[" + returnCode + "]]></return_code>");
            sb.Append("<return_msg><![CDATA[" + returnMsg + "]]></return_msg>");
            sb.Append("</xml>");
            return sb.ToString();
        }

    }
}
