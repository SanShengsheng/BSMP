using Castle.Core.Logging;
using MQKJ.BSMP.Utils.Message.Dtos;
using MQKJ.BSMP.Utils.Message.Wechat.Dtos;
using MQKJ.BSMP.Utils.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MQKJ.BSMP.Utils.Message
{
    public class TemplateMessage
    {
        private static string _appId = "";
        private static Dictionary<string, WeChatAccessToken> _accessTokenDic = new Dictionary<string, WeChatAccessToken>();
        private static string _requestAccessTokenUrl = "";
        private static string _sendMiniProgramTemplateMessage = "";
        private static int _expiredErrorCount = 0;
        /// <summary>
        /// 日志
        /// </summary>
        public ILogger Logger { get; set; }
        public TemplateMessage(string appId, string secret, ILogger logger)
        {
            _appId = appId;
            _requestAccessTokenUrl = $"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={appId}&secret={secret}";
            _sendMiniProgramTemplateMessage = $"https://api.weixin.qq.com/cgi-bin/message/wxopen/template/send?access_token=";
            Logger = logger;
        }
        /// <summary>
        /// 发送小程序模板消息
        /// </summary>
        /// <param name="openId">接收人openId</param>
        /// <returns></returns>
        public async Task<string> SendMiniProgramTemplateMessage(WechatTemplateMessageDataDto input)
        {
            //object error = null;
            var result = "";
            //获取accessToken
            var accessToken = GetAccessToken();
            Logger.Debug("accessToken为：" + accessToken);
            if (!string.IsNullOrEmpty(accessToken))
            {
                var requestUrl = _sendMiniProgramTemplateMessage + accessToken;
                var postData = JsonHelper.GetJson(input);
                result = SendQYMessage(requestUrl, postData);
                if (result.Contains("42001") && _expiredErrorCount <= 2)
                {
                    Logger.Error("accessToken过期了，失败次数" + _expiredErrorCount);

                    //access token过期了，重新获取
                    _accessTokenDic[_appId] = null;
                    ++_expiredErrorCount;
                    await SendMiniProgramTemplateMessage(input);
                }
            }
            else
            {
                Logger.Error("accessToken没有获取到");
            }

            return result;
        }

        public string GetAccessToken(int errorCount = 0, bool isExpired = false)
        {
            var accessToken = _accessTokenDic.ContainsKey(_appId) ? _accessTokenDic[_appId] : null;
            //判断凭据是否有效
            if (accessToken != null && accessToken.CreateDateTime.AddSeconds(accessToken.Expires_in + 5000) < DateTime.Now)
            {
                return accessToken.Access_token;
            }
            using (var client = new HttpClient())
            {
                try
                {
                    var response = client.GetAsync(_requestAccessTokenUrl).Result;
                    Logger.Debug("请求accessToken");
                    if (response != null)
                    {
                        if (response.IsSuccessStatusCode == true)
                        {
                            //

                            String strResult = response.Content.ReadAsStringAsync().Result;
                            Logger.Debug("请求accessToken返回" + strResult);

                            var result = JsonConvert.DeserializeObject<WeChatAccessToken>(strResult, new JsonSerializerSettings { MetadataPropertyHandling = MetadataPropertyHandling.Ignore });
                            Logger.Debug("accessToken反序列化完毕");

                            if (result != null && !string.IsNullOrEmpty(result.Access_token))
                            {
                                _accessTokenDic = _accessTokenDic ?? new Dictionary<string, WeChatAccessToken>();
                                if (!_accessTokenDic.ContainsKey(_appId))
                                {
                                    accessToken = new WeChatAccessToken() { Access_token = result.Access_token, Expires_in = result.Expires_in, CreateDateTime = DateTime.Now };
                                    //添加
                                    _accessTokenDic.Add(_appId, accessToken);
                                }
                                else
                                {
                                    //更新
                                    if (_accessTokenDic == null)
                                    {
                                        Logger.Warn("_accessTokenDic为null了");
                                        _accessTokenDic = new Dictionary<string, WeChatAccessToken>();
                                    }
                                    if (accessToken == null)
                                    {
                                        Logger.Warn("_accessToken为null了");
                                        accessToken = new WeChatAccessToken();
                                    }
                                    _accessTokenDic[_appId] = null;
                                    accessToken.Access_token = result.Access_token;
                                    accessToken.Expires_in = result.Expires_in;
                                    accessToken.CreateDateTime = DateTime.Now;
                                    _accessTokenDic[_appId] = accessToken;
                                }
                                Logger.Debug("accessToken存储完毕");
                            }
                            else
                            {
                                Logger.Error("获取accessToken时返回值为null");
                            }
                        }
                        else
                        {
                            return "请求接口错误，错误码：" + response.StatusCode;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"获取accessToken出错啦，appid:{_appId},message：" + ex.Message + "；/r/n source：" + ex.Source + "；/r/n source：" + ex.StackTrace);
                }

                if (accessToken == null && errorCount < 2)
                {
                    GetAccessToken(++errorCount);
                }
            }
            return accessToken == null ? null : accessToken.Access_token;
        }
        private string SendQYMessage(string postUrl, string paramData)
        {
            return PostWebRequest(postUrl, paramData, Encoding.UTF8);
        }

        private TemplateList GetTemplateList(string access_token, int offset, int count)
        {
            var accessToken = GetAccessToken();
            var paras = JsonHelper.GetJson(new { offset = 0, count = 20 });
            var response = PostWebRequest("https://api.weixin.qq.com/cgi-bin/wxopen/template/list?access_token=" + accessToken, paras, Encoding.UTF8);
            var result = response != null ? (TemplateList)JsonHelper.GetObject(response) : null;
            return result;
        }
        /// <summary>
        /// Post数据接口
        /// </summary>
        /// <param name="postUrl">接口地址</param>
        /// <param name="paramData">提交json数据</param>
        /// <param name="dataEncode">编码方式</param>
        /// <returns></returns>
        public string PostWebRequest(string postUrl, string paramData, Encoding dataEncode)
        {
            string ret = string.Empty;
            try
            {
                byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return ret;
        }
    }
}
