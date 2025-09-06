using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MQKJ.BSMP.WeChatPay.Models
{
    [XmlRoot("xml")]
    public abstract class WechatPayResponseBase
    {
        /// <summary>
        /// 此字段是通信标识，非交易标识，交易是否成功需要查看trade_state来判断
        /// </summary>
        [XmlElement("return_code")]
        public string ReturnCode { get; set; }
        /// <summary>
        /// 当return_code为FAIL时返回信息为错误原因 ，例如
        /// 签名失败
        ///参数格式校验错误
        /// </summary>
        [XmlElement("return_msg")]
        public string ReturnMsg { get; set; }

        /// <summary>
        /// 业务结果 SUCCESS/FAIL
        /// </summary>
        [XmlElement("result_code")]
        public string ResultCode { get; set; }
        /// <summary>
        /// 错误代码	
        /// </summary>
        [XmlElement("err_code")]
        public string ErrCode { get; set; }
        /// <summary>
        /// 错误返回的信息描述
        /// </summary>
        [XmlElement("err_code_des")]
        public string ErrCodeDes { get; set; }

        /// <summary>
        /// 调用接口提交的公众账号ID
        /// </summary>
        [XmlElement("appid")]
        public string AppId { get; set; }
        /// <summary>
        /// 调用接口提交的商户号
        /// </summary>
        [XmlElement("mch_id")]
        public string MchId { get; set; }
        /// <summary>
        /// 调用接口提交的终端设备号
        /// </summary>
        [XmlElement("device_info")]
        public string DeviceInfo { get; set; }
        /// <summary>
        /// 微信返回的随机字符串
        /// </summary>
        [XmlElement("nonce_str")]
        public string NonceStr { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        [XmlElement("sign")]
        public string Sign { get; set; }

        /// <summary>
        /// 返回是否成功
        /// </summary>
        [XmlIgnore]
        public bool IsReturnSuccess
        {
            get
            {
                return ReturnCode.Equals("SUCCESS", StringComparison.OrdinalIgnoreCase);
            }
        }

        /// <summary>
        /// 业务是否成功
        /// </summary>
        [XmlIgnore]
        public bool IsResultSuccess
        {
            get
            {
                return ResultCode.Equals("SUCCESS", StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}
