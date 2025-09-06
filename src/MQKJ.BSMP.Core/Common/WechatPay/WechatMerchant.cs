using Abp.Domain.Entities;
using MQKJ.BSMP.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MQKJ.BSMP.Common.WechatPay
{
    [Table("WechatMerchants")]
    public class WechatMerchant:Entity<int>,ISoftDelete
    {
        /// <summary>
        /// 商户号Id
        /// </summary>
        public string MchId { get; set; }

        /// <summary>
        /// 商户密钥
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 回调地址
        /// </summary>
        public string NotifyUrl { get; set; }

        //public int TeantId { get; set; }

        public MerchantState MerchantState { get; set; }

        public bool IsDeleted { get; set; }

        public PaymentType PaymentType { get; set; }
    }

    public enum MerchantState
    {
        /// <summary>
        /// 已启用
        /// </summary>
        Activar = 1,

        /// <summary>
        /// 已停止
        /// </summary>
        Disconnected = 2
    }
}
