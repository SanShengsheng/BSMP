using System;
using System.Collections.Generic;
using System.Text;

namespace MQKJ.BSMP.Utils.WechatPay.Dtos
{
    public class EnterprisePayForPersonInput
    {
        public string AppId { get; set; }

        public string MchId { get; set; }


        public string NonceStr { get; set; }

        public string OpenId { get; set; }

        /// <summary>
        /// 商户订单号(只能包含数字字母)
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 校验用户姓名 NO_CHECK-不校验FORCE_CHECK强校验真实姓名
        /// </summary>
        public string CheckName { get; set; }

        /// <summary>
        /// 用户真实姓名(收款人真实姓名)
        /// </summary>
        public string UserName { get; set; }


        /// <summary>
        /// 企业付款金额
        /// </summary>
        public double TotalAmount { get; set; }

        /// <summary>
        /// 企业付款备注
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public string Spbill_Create_IP { get; set; }

        public string Key { get; set; }

        public string Path { get; set; }
    }
}
