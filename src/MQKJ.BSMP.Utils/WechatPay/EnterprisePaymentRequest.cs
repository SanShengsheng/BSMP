using MQKJ.BSMP.Utils.Tools;
using MQKJ.BSMP.Utils.WechatPay.Dtos;
using MQKJ.BSMP.Utils.WechatPay.Modes;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace MQKJ.BSMP.Utils.WechatPay
{

    public class EnterprisePaymentRequest
    {

        public const string CER_PASSWORD = "1622628629";

        private const string enterprisePayUrl = "https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/transfers";

        /// <summary>
        /// 企业付款给个人
        /// </summary>
        /// <param name="Bill_No"></param>
        /// <param name="toOpenid"></param>
        /// <param name="Charge_Amt"></param>
        /// <param name="userName"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static WechatEnterprisePayResult EnterprisePayForPerson(EnterprisePayForPersonInput input)
        {
            try
            {
                //公众账号appid mch_appid 是 wx8888888888888888 String 微信分配的公众账号ID（企业号corpid即为此appId） 
                //商户号 mchid 是 1900000109 String(32) 微信支付分配的商户号 
                //设备号 device_info 否 013467007045764 String(32) 微信支付分配的终端设备号 
                //随机字符串 nonce_str 是 5K8264ILTKCH16CQ2502SI8ZNMTM67VS String(32) 随机字符串，不长于32位 
                //签名 sign 是 C380BEC2BFD727A4B6845133519F3AD6 String(32) 签名，详见签名算法 
                //商户订单号 partner_trade_no 是 10000098201411111234567890 String 商户订单号，需保持唯一性 
                //用户openid openid 是 oxTWIuGaIt6gTKsQRLau2M0yL16E String 商户appid下，某用户的openid 
                //校验用户姓名选项 check_name 是 OPTION_CHECK String NO_CHECK：不校验真实姓名 
                //FORCE_CHECK：强校验真实姓名（未实名认证的用户会校验失败，无法转账） 
                //OPTION_CHECK：针对已实名认证的用户才校验真实姓名（未实名认证用户不校验，可以转账成功） 
                //收款用户姓名 re_user_name 可选 马花花 String 收款用户真实姓名。 
                // 如果check_name设置为FORCE_CHECK或OPTION_CHECK，则必填用户真实姓名 
                //金额 amount 是 10099 int 企业付款金额，单位为分 
                //企业付款描述信息 desc 是 理赔 String 企业付款操作说明信息。必填。 
                //Ip地址 spbill_create_ip 是 192.168.0.1 String(32) 调用接口的机器Ip地址 

                //Bill_No = string.Format("{0}{1}{2}", PayInfo.MchId, DateTime.Now.ToString("yyyyMMddHHmmss"), WeChatPayHelper.BuildRandomStr(6));  //订单号组成 商户号 + 随机时间串 + 记录ID
                SortedDictionary<string, string> paramDic = new SortedDictionary<string, string>();
                //小程序AppId
                paramDic.Add("mch_appid", input.AppId);
                //商户号
                paramDic.Add("mchid", input.MchId);
                //随机串
                paramDic.Add("nonce_str", input.NonceStr);
                //订单号
                paramDic.Add("partner_trade_no", input.OutTradeNo);
                //openid 每个用户唯一标识
                paramDic.Add("openid", input.OpenId);
                //校验用户姓名选项	
                paramDic.Add("check_name", input.CheckName);
                //用户名	
                paramDic.Add("re_user_name", input.UserName);
                //金额	
                paramDic.Add("amount", (input.TotalAmount).ToString());
                //描述	
                paramDic.Add("desc", input.Description);
                //终端Id
                paramDic.Add("spbill_create_ip", input.Spbill_Create_IP);
                var sign = WeChatPayHelper.GetSignInfo(paramDic, input.Key);
                //签名
                paramDic.Add("sign", sign);

                //将上面的改成
                //X509Certificate2 cert = new X509Certificate2(input.Path, CER_PASSWORD, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);//线上发布需要添加

                //X509Certificate cert = new X509Certificate(input.Path, CER_PASSWORD, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);

                X509Certificate2 certificate = new X509Certificate2(input.Path, CER_PASSWORD);

                X509Store store1 = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                store1.Open(OpenFlags.ReadWrite);
                store1.Add(certificate);
                if (string.IsNullOrWhiteSpace(certificate.SubjectName.Name) || !certificate.SubjectName.Name.Contains("CN"))
                {
                    throw new Exception($"企业证书无效：{certificate.SubjectName.Name},WxRefundDomain,Refund");
                }
                Regex rg = new Regex("(?<=(CN=))[.\\s\\S]*?(?=(,))", RegexOptions.Multiline | RegexOptions.Singleline);
                var subjectName = rg.Match(certificate.SubjectName.Name).Value;
                store1.Close();


                X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection certs = store.Certificates.Find(X509FindType.FindBySubjectName, subjectName,
                false);
                if (certs.Count > 0)
                {
                    X509Certificate2 cert = certs[0];

                    string result = string.Empty;

                    var response = WeChatPayHelper.HttpPost<WechatEnterprisePayResult>(enterprisePayUrl, paramDic, cert, out result);

                    return response;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

    }
}
