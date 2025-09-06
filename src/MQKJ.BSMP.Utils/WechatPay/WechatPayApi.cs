using Microsoft.Extensions.Logging;
using MQKJ.BSMP.Utils.Tools;
using MQKJ.BSMP.Utils.Tools.HttpRequestTool;
using MQKJ.BSMP.Utils.WechatPay;
using MQKJ.BSMP.Utils.WechatPay.Dtos;
using MQKJ.BSMP.Utils.WechatPay.Helper;
using MQKJ.BSMP.Utils.WechatPay.Modes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MQKJ.BSMP.BigRisks.WeChat.WechatPay
{
    /// <summary>
    ///     微信支付接口
    /// </summary>
    public static class WeChatPayApi
    {
        private const string Fail_Xml_Tpl =
            "<xml><return_code><![CDATA[FAIL]]></return_code><return_msg><![CDATA[{0}]]></return_msg></xml>";

        /// <summary>
        ///     小程序支付
        ///     小程序统一下单接口
        ///     https://pay.weixin.qq.com/wiki/doc/api/wxa/wxa_api.php?chapter=9_1
        /// </summary>
        /// <returns></returns>
        public static MiniProgramPayOutput MiniProgramPay(MiniProgramPayInput input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            //if (input.TotalFee <= 0)
            //{
            //    throw new ArgumentException("金额不能小于0!", nameof(input.TotalFee));
            //}

            if (string.IsNullOrWhiteSpace(input.OpenId))
            {
                throw new ArgumentNullException("OpenId必须填写!", nameof(input.OpenId));
            }

            if (string.IsNullOrWhiteSpace(input.Body))
            {
                throw new ArgumentNullException("商品描述必须填写!", nameof(input.Body));
            }

            if (string.IsNullOrWhiteSpace(input.SpbillCreateIp))
            {
                throw new ArgumentNullException("终端IP必须填写!", nameof(input.SpbillCreateIp));
            }

            var url = "https://api.mch.weixin.qq.com/pay/unifiedorder";

            var model = new WechatPayRequest
            {
                AppId = input.AppId,
                MchId = input.MchId,
                Attach = input.Attach,
                Body = input.Body,
                //Detail = input.Detail,
                //DeviceInfo = input.DeviceInfo,
                //FeeType = input.FeeType,
                //GoodsTag = input.GoodsTag,
                //LimitPay = input.LimitPay,
                OpenId = input.OpenId,
                OutTradeNo = input.OutTradeNo ?? WeChatPayHelper.GenerateOutTradeNo(),
                SpbillCreateIp = input.SpbillCreateIp,
                //TimeExpire = input.TimeExpire,
                //TimeStart = input.TimeStart,
                TotalFee = ((int)(input.TotalFee * 100)).ToString(),
                NonceStr = WeChatPayHelper.GetNoncestr(),
                NotifyUrl = input.NotifyUrl
            };
            Dictionary<string, string> paramDic = new Dictionary<string, string>();
            //小程序AppId
            paramDic.Add("appid", input.AppId);
            //附加数据
            paramDic.Add("attach", input.Attach);
            //商品描述
            paramDic.Add("body", input.Body);
            //商户号
            paramDic.Add("mch_id", input.MchId);

            string nonceStr = WeChatPayHelper.GetNoncestr();
            //随机串
            paramDic.Add("nonce_str", nonceStr);
            //通知地址(异步接收支付结果通知回调地址) 必须是外网能访问且不能携带参数
            paramDic.Add("notify_url", input.NotifyUrl);
            //openid 每个用户唯一标识
            paramDic.Add("openid", input.OpenId);

            //订单号
            paramDic.Add("out_trade_no", input.OutTradeNo);
            //终端Id
            paramDic.Add("spbill_create_ip", input.SpbillCreateIp);
            //金额
            paramDic.Add("total_fee", Convert.ToDouble(input.TotalFee * 100).ToString());
            //交易类型
            paramDic.Add("trade_type", input.TradeType);
            var sign = WeChatPayHelper.GetSignInfo(paramDic, input.Key);
            //签名
            paramDic.Add("sign", sign);
            //var dictionary = WeChatPayHelper.GetDictionaryByType(model);
            //model.Sign = WeChatPayHelper.GetSignInfo(dictionary, input.Key); //生成Sign

            var result = WeChatPayHelper.PostXML<WechatPayResult>(url, paramDic);
            if (result.IsSuccess())
            {
                var data = new
                {
                    appId = result.AppId,
                    nonceStr = result.NonceStr,
                    package = "prepay_id=" + result.PrepayId,
                    signType = input.SignType,
                    timeStamp = WeChatPayHelper.GetTimestamp(),
                };
                return new MiniProgramPayOutput
                {
                    AppId = data.appId,
                    Package = data.package,
                    NonceStr = data.nonceStr,
                    PaySign = WeChatPayHelper.GetSignInfo(WeChatPayHelper.GetDictionaryByType(data), input.Key),
                    SignType = data.signType,
                    TimeStamp = data.timeStamp,
                    TradeNo = input.OutTradeNo,
                    MwebUrl = result.MwebUrl
                };
            }
            else
            {
                //Logger.Error("支付失败：" + result);
            }
            throw new WeChatPayException($"支付错误，请联系客服或重新支付！result.DetailResult:{result.DetailResult}");
        }

        /// <summary>
        ///     支付回调通知处理
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public static PayNotifyOutput PayNotifyHandler(Stream inputStream)
        {
            PayNotifyOutput result = null;
            var data = WeChatPayHelper.PostInput(inputStream);
            try
            {
                result = XmlHelper.DeserializeObject<PayNotifyOutput>(data);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        ///     支付回调通知处理二
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public static PayNotifyOutput PayNotifyHandler(string xmlStr)
        {
            PayNotifyOutput result = null;
            try
            {
                result = XmlHelper.DeserializeObject<PayNotifyOutput>(xmlStr);
            }
            catch (Exception ex)
            {
                throw new Exception($"支付回调结果处理异常:{ex}");
            }

            return result;
        }

        /// <summary>
        /// 获取公众号的openid
        /// </summary>
        /// <returns></returns>
        public static GetOfficeAccountOpenIdOutput GetOfficeAccountOpenId(GetOfficeAccountOpenIdInput input)
        {
            var url = "https://api.weixin.qq.com/sns/oauth2/access_token";
            Dictionary<string, string> paramDic = new Dictionary<string, string>();

            paramDic.Add("appid", input.AppId);
            paramDic.Add("secret", input.AppSecret);
            paramDic.Add("code", input.Code);
            paramDic.Add("grant_type", "authorization_code");
            var response = CustomHttpRequest.CreatePostHttpResponse(url, paramDic);

           //var obj =  JsonHelper.GetObject(response);

            var output = JsonConvert.DeserializeObject<GetOfficeAccountOpenIdOutput>(response);

            return output;
        }

        /// <summary>
        /// 获取公众号的openid
        /// </summary>
        /// <returns></returns>
        public static GetOfficeAccountUnionIdOutput GetOfficeAccountUnionId(GetOfficeAccountUnionIdInput input)
        {
            var url = "https://api.weixin.qq.com/sns/userinfo";
            Dictionary<string, string> paramDic = new Dictionary<string, string>();

            paramDic.Add("access_token", input.AccessToken);
            paramDic.Add("openid", input.OpenId);
            paramDic.Add("lang", input.Lang);
            var response = CustomHttpRequest.CreateHttpGetRequest(url, paramDic);

            //var obj = JsonHelper.GetObject(response);

            var output = JsonConvert.DeserializeObject<GetOfficeAccountUnionIdOutput>(response);

            return output;
        }

        /// <summary>
        /// 获取微信网站应用accesstoken
        /// </summary>
        /// <returns></returns>
        public static GetAccessTokenWithCodeOutput GetWechatWebAccessToken(GetAccessTokenWithCodeInput input)
        {
            var url = "https://api.weixin.qq.com/sns/oauth2";
            Dictionary<string, string> paramDic = new Dictionary<string, string>();

            paramDic.Add("appid", input.AppId);
            paramDic.Add("secret", input.Secret);
            paramDic.Add("code", input.Code);
            paramDic.Add("grant_type", input.GrantType);
            WebRequestHelper requestTool = new WebRequestHelper();
            var response = CustomHttpRequest.CreateHttpGetRequest(url, paramDic);

            //var obj = JsonHelper.GetObject(response);

            var output = JsonConvert.DeserializeObject<GetAccessTokenWithCodeOutput>(response);

            return output;
        }

        /// <summary>
        /// 刷新微信网站应用accesstoken
        /// </summary>
        /// <returns></returns>
        public static RefreshAccessTokenOutput RefreshAccessToken(RefreshAccessTokenInput input)
        {
            var url = "https://api.weixin.qq.com/sns/oauth2";
            Dictionary<string, string> paramDic = new Dictionary<string, string>();

            paramDic.Add("appid", input.AppId);
            paramDic.Add("grant_type", input.GrantType);
            paramDic.Add("refresh_token", input.RefreshToken);
            var response = CustomHttpRequest.CreateHttpGetRequest(url, paramDic);

            //var obj = JsonHelper.GetObject(response);

            var output = JsonConvert.DeserializeObject<RefreshAccessTokenOutput>(response);

            return output;
        }

        /// <summary>
        /// 验证微信网站应用accesstoken
        /// </summary>
        /// <returns></returns>
        public static CheckAccessTokenOutput CheckAccessToken(CheckAccessTokenInput input)
        {
            var url = "https://api.weixin.qq.com/sns/auth";
            Dictionary<string, string> paramDic = new Dictionary<string, string>();

            paramDic.Add("access_token", input.AccessToken);
            paramDic.Add("openid", input.OpenId);
            var response = CustomHttpRequest.CreateHttpGetRequest(url, paramDic);

            //var obj = JsonHelper.GetObject(response);

            var output = JsonConvert.DeserializeObject<CheckAccessTokenOutput>(response);

            return output;
        }

        /// <summary>
        /// 验证微信网站应用accesstoken
        /// </summary>
        /// <returns></returns>
        public static GetWechatWebUserInfoOutput GetWechatWebUserInfo(GetWechatWebUserInfoInput input)
        {
            var url = "https://api.weixin.qq.com/sns/userinfo";
            Dictionary<string, string> paramDic = new Dictionary<string, string>();

            paramDic.Add("access_token", input.AccessToken);
            paramDic.Add("openid", input.OpenId);
            paramDic.Add("lang", input.Lang);
            var response = CustomHttpRequest.CreateHttpGetRequest(url, paramDic);

            //var obj = JsonHelper.GetObject(response);

            var output = JsonConvert.DeserializeObject<GetWechatWebUserInfoOutput>(response);

            return output;
        }

        /// <summary>
        ///     处理支付回调参数
        /// </summary>
        /// <param name="inputStream">输入流</param>
        /// <param name="payHandlerFunc">支付处理逻辑函数</param>
        /// <returns>处理结果</returns>
        //public static Task<string> PayNotifyHandler(Stream inputStream, Action<PayNotifyOutput, string> payHandlerFunc)
        //{
        //    PayNotifyOutput result = null;
        //    var data = WeChatPayHelper.PostInput(inputStream);
        //    var outPutXml = string.Empty;
        //    var error = string.Empty;
        //    try
        //    {
        //        result = XmlHelper.DeserializeObject<PayNotifyOutput>(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        WeChatPayHelper.LoggerAction?.Invoke("Error", "解析支付回调参数出错：" + data + "  Exception:" + ex);
        //        outPutXml = string.Format(Fail_Xml_Tpl, "解析支付回调参数出错");
        //        error = ex.ToString();
        //    }

        //    if (!string.IsNullOrWhiteSpace(outPutXml))
        //    {
        //    }
        //    else if (string.IsNullOrWhiteSpace(result?.TransactionId))
        //    {
        //        error = "支付结果中微信订单号不存在";
        //        outPutXml = string.Format(Fail_Xml_Tpl, error);
        //    }
        //    else if (!result.IsSuccess())
        //    {
        //        outPutXml = string.Format(Fail_Xml_Tpl, "回调处理失败");
        //        error = $"回调处理失败：ErrCode:{result.ErrCode} \nErrCodeDes:{result.ErrCodeDes}";
        //    }
        //    //查询订单，判断订单真实性
        //    else if (!QueryOrder(result.TransactionId))
        //    {
        //        error = "订单不存在";
        //        outPutXml = string.Format(Fail_Xml_Tpl, error);
        //    }

        //    payHandlerFunc?.Invoke(result, error);

        //    return Task.FromResult(!string.IsNullOrWhiteSpace(outPutXml)
        //        ? outPutXml
        //        : "<xml><return_code><![CDATA[SUCCESS]]></return_code></xml>");
        //}

        /// <summary>
        ///     订单查询
        ///     该接口提供所有微信支付订单的查询，商户可以通过查询订单接口主动查询订单状态，完成下一步的业务逻辑。
        ///     需要调用查询接口的情况：
        ///     ◆ 当商户后台、网络、服务器等出现异常，商户系统最终未接收到支付通知；
        ///     ◆ 调用支付接口后，返回系统错误或未知交易状态情况；
        ///     ◆ 调用刷卡支付API，返回USERPAYING的状态；
        ///     ◆ 调用关单或撤销接口API之前，需确认支付状态；
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static OrderQueryOutput OrderQuery(OrderQueryInput input)
        {
            var url = "https://api.mch.weixin.qq.com/pay/orderquery";
            //检测必填参数
            if (string.IsNullOrWhiteSpace(input.TransactionId) && string.IsNullOrWhiteSpace(input.OutTradeNo))
            {
                throw new WeChatPayException("订单查询接口中，out_trade_no、transaction_id至少填一个！");
            }
            Dictionary<string, string> paramDic = new Dictionary<string, string>();
            paramDic.Add("appid", input.AppId);
            paramDic.Add("mch_id", input.MchId);
            paramDic.Add("nonce_str", input.NonceStr);
            paramDic.Add("out_trade_no", input.OutTradeNo);
            var sign = WeChatPayHelper.GetSignInfo(paramDic, input.Key);
            paramDic.Add("sign", sign);

            return WeChatPayHelper.PostXML<OrderQueryOutput>(url, paramDic);
        }

        //private static string CreateSign<T>(T model)
        //{
        //    var dictionary = WeChatPayHelper.GetDictionaryByType(model);
        //    return WeChatPayHelper.GetSignInfo(dictionary, config.TenPayKey); //生成Sign
        //}

        /// <summary>
        ///     查询订单是否存在
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        //private static bool QueryOrder(string transactionId) => OrderQuery(new OrderQueryInput
        //{
        //    TransactionId = transactionId
        //}).IsSuccess();
    }
}
